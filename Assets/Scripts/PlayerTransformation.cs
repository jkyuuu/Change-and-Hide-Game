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

    public State playerState { get; private set; } // 현재 플레이어 상태
    private void Awake()
    {
        subCamParent = GameObject.Find("Sub Cam Parent").transform;
        subCamera = subCamParent.GetChild(0);
        if (subCamera == null)
            Debug.Log("서브 카메라 없음");
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
                Debug.Log("3인칭으로!");
                
                Transformation();
                Debug.Log("변신 완료!");
            }
            else
            {
                Debug.Log("마우스 안누름");
                return;
            }
        }


        if (playerInput.returnPlayer)
        {
            if(this.playerState == State.Transformation)
            {
                returnPlayer();
                Debug.Log("플레이어로 복귀");
            }
            else
            {
                Debug.Log("변신 중 아님");
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
                Debug.Log("오브젝트 가져옴");
            }
            else
            {
                Debug.Log("레디 아님");
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
