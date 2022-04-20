using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private PlayerTransformation playerTransformation;
    private CamView camView;

    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    private bool isJumping;

    private float rotateSpeed = 15f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransformation = GetComponent<PlayerTransformation>();
        camView = GetComponent<CamView>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();

        if (playerTransformation.hitObject)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                CancelInvoke("thirdMove");
                Debug.Log("thirMove �Լ� �ߴ�");
            }
            else
            {
                ThirdMove();
            }
        }
    }

    private void Move()
    {
        transform.Translate(new Vector3(playerInput.xInput, 0, playerInput.zInput) * moveSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                //Debug.Log("����");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            isJumping = false;
            //Debug.Log("�ٴڿ� ����"); 
        }
    }
   
    private void ThirdMove()
    {
        //(���1)
        Vector3 playerToCam = playerTransformation.mainCamera.forward;
        playerToCam.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(playerToCam);
        playerRigidbody.rotation = Quaternion.Lerp(playerRigidbody.rotation, newRotation, Time.deltaTime * rotateSpeed);
        //playerRigidbody.MoveRotation(newRotation);

        ////(���2)
        //var offset = playerTransformation.subCamera.transform.forward;
        //offset.y = 0f;
        //transform.LookAt(this.gameObject.transform.position + offset * Time.deltaTime);
    }
}
