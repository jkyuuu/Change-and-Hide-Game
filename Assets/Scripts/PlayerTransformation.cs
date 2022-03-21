using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField]
    private ObjectAwareness objectAwareness;
    private PlayerInput playerInput;
    private PlayerMove playerMove;

    public Transform playerObject;
    private Transform parentPlayer;

    public GameObject hitObject;
    private Transform transformPoint;

    private Rigidbody objectRigid;

    private Transform subCamParent;
    private Transform subCamera;
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
        playerMove = GetComponent<PlayerMove>();

        parentPlayer = GameObject.Find("Player").transform;
        playerObject = parentPlayer.GetChild(0);

        if (GameObject.FindWithTag("Player"))
        {
            this.playerState = State.Ready;
        }
        else
        {
            this.playerState = State.Transformation;
            hitObject = this.gameObject;
        }
    }

    private void Update()
    {        
        Transformation();
        Debug.Log("���� �Ϸ�!");

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
        if (objectAwareness != null)
        {
            objectAwareness.Awareness();

            if (objectAwareness.rayHit)
            {
                if (playerInput.selectObject)
                {
                    Debug.Log("���콺 ����");

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
                else
                {
                    Debug.Log("���콺 �ȴ���");
                    return;
                }
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

        playerObject.gameObject.SetActive(false);

        subCamera.gameObject.SetActive(true);
        subCamera.transform.SetParent(hitObject.transform);
        subCamera.transform.position = hitObject.transform.position + new Vector3(0, 4, -4);
    }

    public void returnPlayer()
    {
        subCamera.transform.SetParent(subCamParent);
        subCamera.gameObject.SetActive(false);
        Destroy(hitObject);

        playerObject.gameObject.SetActive(true);
        playerObject.position = hitObject.transform.localPosition;

    }
}
