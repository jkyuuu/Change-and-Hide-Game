using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField]
    private ObjectAwareness objectAwareness;
    private PlayerInput playerInput;
    public CamView camView;
    public ThirdPersonCam thirdPersonCam;
    public Player player;

    public GameObject playerObject;
    public GameObject thirdPlayerObject;
    public GameObject hitObject;
    private Transform transformPoint;

    private Rigidbody objectRigid;

    //private Transform subCamParent;
    //public Transform subCamera;

    public Transform mainCamParent;
    public Transform mainCamera;

    //public event Action create;

    public enum State
    {
        Ready,
        Transformation
    }

    public State playerState { get; private set; } // ���� �÷��̾� ����
    //public void Awake()
    //{
    //    subCamParent = GameObject.Find("Sub Cam Parent").transform;
    //    subCamera = subCamParent.GetChild(0);
    //    if (subCamera == null)
    //        Debug.Log("���� ī�޶� ����");
    //}
    private void Start()
    {
        mainCamParent = GameObject.Find("Main Cam Parent").transform;
        mainCamera = mainCamParent.GetChild(0);
        if (mainCamera == null)
            Debug.Log("���� ī�޶� ����");

        //playerParent = GameObject.Find("Player").transform;
        //playerObject = playerParent.GetChild(0).gameObject;

        playerInput = GetComponent<PlayerInput>();
        camView = GameObject.Find("Main Camera").GetComponent<CamView>();
        thirdPersonCam = mainCamera.GetComponent<ThirdPersonCam>();
        player = FindObjectOfType<Player>();

        playerObject = GameObject.FindWithTag("Player");
        thirdPlayerObject = GameObject.FindWithTag("Transformed");
        
        if(playerObject)
        {
            objectAwareness = GameObject.Find("Main Camera").GetComponent<ObjectAwareness>();
            this.playerState = State.Ready;
            mainCamera.SetParent(playerObject.transform);
        }
        else if (thirdPlayerObject)
        {
            this.playerState = State.Transformation;
            hitObject = this.gameObject;
        }
    }

    private void Update()
    {
        if (objectAwareness != null)
        {
            objectAwareness.Awareness();

            if (playerInput.selectObject)
            {
                if (objectAwareness.hitGameobject != null)
                {
                    Debug.Log("3��Ī����!");

                    Transformation();
                    Debug.Log("���� �Ϸ�!");

                    camView.toggleView = 4 - camView.toggleView;
                    Debug.Log("3��Ī ��ۺ�");

                    var thirdDirection = mainCamera.forward;
                    thirdDirection.y = 0;
                    hitObject.transform.LookAt(hitObject.transform.position + thirdDirection);
                }
                else
                    Debug.Log("��� ����");
            }
            else
            {
                Debug.Log("���콺 �ȴ���");
                return;
            }
        }


        if (playerInput.returnPlayer)
        {
            if(this.playerState == State.Transformation)
            { 
                thirdPersonCam.enabled = false;
                ReturnFirstPlayer();
                Debug.Log("�÷��̾�� ����");

                camView.toggleView = 4 - camView.toggleView;
                Debug.Log("1��Ī ��ۺ�");
            }
            else
            {
                Debug.Log("���� �� �ƴ�");
            }
        }
    }

    public void Transformation()
    { 
        if (objectAwareness.rayHit)
        {                 
            if (this.playerState == State.Ready)
            {
                CreateObject();
                Debug.Log("������Ʈ ������");

            }
            else
            {
                Debug.Log("���� �ƴ�");
            }                    
        }
    }

    public void CreateObject()
    {
        transformPoint = playerObject.transform;
        hitObject = Instantiate(objectAwareness.hitGameobject, transformPoint.position, transformPoint.rotation);
        
        objectRigid = hitObject.GetComponent<Rigidbody>();
        objectRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        hitObject.AddComponent<PlayerInput>();
        hitObject.AddComponent<PlayerTransformation>();
        hitObject.AddComponent<PlayerMove>();
        thirdPersonCam.thirdCamPlayer = hitObject;
        thirdPersonCam.enabled = true;

        hitObject.tag = "Transformed";
        mainCamera.SetParent(mainCamParent);
        //playerObject.gameObject.SetActive(false);

        PlayerPool.ReturnPool(player);
        //subCamera.gameObject.SetActive(true);


        //thirdPersonCam.transform.LookAt(thirdPersonCam.transform.position + playerObject.transform.forward);
        //subCamera.transform.SetParent(hitObject.transform);
        //subCamera.transform.position = hitObject.transform.localPosition + new Vector3(0, 4, -4);

        //thirdPersonCam.thirdPlayer = hitObject.gameObject;
    }

    public void ReturnFirstPlayer()
    {
        //subCamera.transform.SetParent(subCamParent);
        //subCamera.gameObject.SetActive(false);

        //playerObject.SetActive(true);
        var objectReturn = PlayerPool.GetPlayer();
        objectReturn.transform.position = hitObject.transform.position;
        mainCamera.SetParent(objectReturn.transform);
        Destroy(hitObject);

        var fisrstDirection = mainCamera.forward;
        fisrstDirection.y = 0;
        objectReturn.transform.LookAt(objectReturn.transform.position + fisrstDirection);
    }
}
