using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField]
    private ObjectAwareness objectAwareness;
    private PlayerInput playerInput;
    private GameObject playerObject;

    public GameObject hitObject;
    private Transform transformPoint;

    private Rigidbody objectRigid;
    private CamController camController;

    private Transform subCamParent;
    private Transform subCamera;
    public enum State
    {
        Ready,
        Transformation
    }

    public State state { get; private set; } // 현재 플레이어 상태
    private void Awake()
    {
        subCamParent = GameObject.Find("Sub Cam Parent").transform;
        subCamera = subCamParent.GetChild(0);
        if (subCamera == null)
            Debug.Log("서브 카메라 없음");
    }
    private void OnEnable()
    {
        state = State.Ready;
        
    }
    
    private void Start()
    {        
        playerInput = GetComponent<PlayerInput>();
        camController = GetComponent<CamController>();
        
    }

    private void Update()
    {
        if(objectAwareness != null)
        {
            objectAwareness.Awareness();

            Transformation();
            
        }

        returnPlayer();
    }

    public void Transformation()
    {
        if (objectAwareness.rayHit)
        {
            if(playerInput.selectObject)
            {
                playerObject = this.gameObject;

                CreateObject();
                Debug.Log("오브젝트 가져옴");

                playerObject.SetActive(false);
                state = State.Transformation;
                Debug.Log("변신 완료!");
            }
        }
    }

    public void CreateObject()
    {
        transformPoint = playerObject.transform;
        hitObject = Instantiate(objectAwareness.hitGameobject, transformPoint.position, transformPoint.rotation);
        
        objectRigid = hitObject.GetComponent<Rigidbody>();
        objectRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        hitObject.AddComponent<TransformedObject>();
        //TransformedObject transformed = hitObject.GetComponent<TransformedObject>();
        //transformed.enabled = true;
        hitObject.AddComponent<PlayerInput>();
        hitObject.AddComponent<PlayerTransformation>();
        hitObject.AddComponent<PlayerMove>();
        hitObject.AddComponent<CamController>();

        subCamera.gameObject.SetActive(true);
        subCamera.transform.SetParent(hitObject.transform);
        subCamera.transform.localPosition = hitObject.transform.position + new Vector3(0, 4, -4);
        //objectAwareness = subCamera;
    }

    public void returnPlayer()
    {
        if (playerInput.returnPlayer)
        {
            //if (playerObject == null)
            //    Debug.Log("기존 플레이어 비활성");

            //playerObject.SetActive(true);
            //subCamera.gameObject.SetActive(false);
            Destroy(hitObject);
            state = State.Ready;

            Debug.Log("플레이어로 복귀");
            return;
        }
    }
}
