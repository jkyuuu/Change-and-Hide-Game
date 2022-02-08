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
                //GameObject hitObject = FindObjectOfType<HitObject>().gameObject;
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
    }

    public void returnPlayer()
    {
        if (playerInput.returnPlayer)
        {
            

            state = State.Ready;

            Debug.Log("플레이어로 복귀");
        }
    }
}
