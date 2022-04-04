using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField]
    private ObjectAwareness objectAwareness;
    private PlayerInput playerInput;
    //private ThirdPersonCam thirdPersonCam;

    public Transform playerObject;
    private Transform parentPlayer;

    public GameObject hitObject;
    private Transform transformPoint;

    private Rigidbody objectRigid;

    private Transform subCamParent;
    public Transform subCamera;
    public enum State
    {
        Ready,
        Transformation
    }

    public State playerState { get; private set; } // ���� �÷��̾� ����
    private void Awake()
    {
        subCamParent = GameObject.Find("Sub Cam Parent").transform;
        subCamera = subCamParent.GetChild(0);
        if (subCamera == null)
            Debug.Log("���� ī�޶� ����");
    }
    //private void OnEnable()
    //{

    //}

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        //thirdPersonCam = GetComponent<ThirdPersonCam>();

        parentPlayer = GameObject.Find("Parent Player").transform;
        playerObject = parentPlayer.GetChild(0);

        if (GameObject.FindWithTag("Player"))
        {
            this.playerState = State.Ready;
        }
        else if (GameObject.FindWithTag("Object"))
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
                Debug.Log("3��Ī����!");
                
                Transformation();
                Debug.Log("���� �Ϸ�!");
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
                returnPlayer();
                Debug.Log("�÷��̾�� ����");
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
        transformPoint = playerObject;
        hitObject = Instantiate(objectAwareness.hitGameobject, transformPoint.position, transformPoint.rotation);
        
        objectRigid = hitObject.GetComponent<Rigidbody>();
        objectRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        hitObject.AddComponent<PlayerInput>();
        hitObject.AddComponent<PlayerTransformation>();
        hitObject.AddComponent<PlayerMove>();
        hitObject.tag = "Transformed";

        playerObject.gameObject.SetActive(false);

        subCamera.gameObject.SetActive(true);
        subCamera.gameObject.AddComponent<ThirdPersonCam>();
        //subCamera.transform.SetParent(hitObject.transform);
        //subCamera.transform.position = hitObject.transform.localPosition + new Vector3(0, 4, -4);

        //thirdPersonCam.thirdPlayer = hitObject.gameObject;
    }

    public void returnPlayer()
    {
        subCamera.transform.SetParent(subCamParent);
        subCamera.gameObject.SetActive(false);
        Destroy(hitObject);

        playerObject.gameObject.SetActive(true);
        playerObject.position = hitObject.transform.localPosition;

        var direction = subCamera.forward;
        direction.y = 0;
        playerObject.transform.LookAt(playerObject.transform.position + direction);
    }
}
