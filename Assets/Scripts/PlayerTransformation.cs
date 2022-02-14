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

    private Camera subCamera;
        
    public enum State
    {
        Ready,
        Transformation
    }

    public State state { get; private set; } // 현재 플레이어 상태

    private void OnEnable()
    {
        state = State.Ready;
        
    }
    
    private void Start()
    {        
        playerInput = GetComponent<PlayerInput>();
        camController = GetComponent<CamController>();
        subCamera = camController.subCam;
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
        //objectRigid.constraints = RigidbodyConstraints.None;
        objectRigid.constraints = RigidbodyConstraints.FreezeRotationX;
        //objectRigid.constraints = RigidbodyConstraints.FreezeRotationZ;

        hitObject.AddComponent<TransformedObject>();
        //TransformedObject transformed = hitObject.GetComponent<TransformedObject>();
        //transformed.enabled = true;
        hitObject.AddComponent<PlayerInput>();
        hitObject.AddComponent<PlayerTransformation>();
        hitObject.AddComponent<PlayerMove>();
        hitObject.AddComponent<CamController>();

    }

    public void returnPlayer()
    {
        if (playerInput.returnPlayer)
        {
            playerObject.SetActive(true);
            hitObject.SetActive(false);

            state = State.Ready;

            Debug.Log("플레이어로 복귀");
        }
    }
}
