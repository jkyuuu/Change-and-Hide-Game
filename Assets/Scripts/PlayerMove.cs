using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private PlayerTransformation playerTransformation;
    private ThirdPersonCam thirdPersonCam;

    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    private bool isJumping;

    private float xMouseSensitivity = 5f;
    private float rotateSpeed = 15f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransformation = GetComponent<PlayerTransformation>();
        thirdPersonCam = GetComponent<ThirdPersonCam>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();

        if (playerTransformation.hitObject)
        {
            thirdMove();
        }
    }
    private void Update()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            XMouseRotate();
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            //Debug.Log("�ٴڿ� ����"); 
        }
    }
    private void XMouseRotate()
    {        
        float xMouseRotate = Input.GetAxis("Mouse X") * xMouseSensitivity;
        
        transform.Rotate(0f, xMouseRotate, 0f);
        Debug.Log("Y�� ���� X�� �̵� ����");        
    }
    private void thirdMove()
    {
        //(���1)
        Vector3 playerToSubcam = playerTransformation.subCamera.transform.forward;
        playerToSubcam.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(playerToSubcam);
        playerRigidbody.rotation = Quaternion.Lerp(playerRigidbody.rotation, newRotation, Time.deltaTime * rotateSpeed);
        
        //playerRigidbody.MoveRotation(newRotation);

        ////(���2)
        //var offset = playerTransformation.subCamera.transform.forward;
        //offset.y = 0f;
        //transform.LookAt(this.gameObject.transform.position + offset * Time.deltaTime);
    }
}
