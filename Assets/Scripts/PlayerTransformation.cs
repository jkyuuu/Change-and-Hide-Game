using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField]
    private ObjectAwareness objectAwareness;
    private PlayerInput playerInput;
    private GameObject playerObject;

    
    public enum State
    {
        Ready,
        Transformation
    }

    public State state { get; private set; } // ���� �÷��̾� ����

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

                GameObject hitObject = FindObjectOfType<HitObject>().gameObject;

                playerObject = hitObject;

                state = State.Transformation;

                Debug.Log("���� �Ϸ�!");
            }
            
        }        
    }

    public void returnPlayer()
    {
        if (playerInput.returnPlayer)
        {
            

            state = State.Ready;

            Debug.Log("�÷��̾�� ����");
        }
    }
}
