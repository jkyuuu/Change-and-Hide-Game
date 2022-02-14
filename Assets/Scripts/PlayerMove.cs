using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    private bool isJumping;

    private float xMouseSensitivity = 5f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Update()
    {
        XMouseRotate();
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
                //Debug.Log("점프");
            }
        }
        else
        {
            return;
            //Debug.Log("점프 못함");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            //Debug.Log("바닥에 닿음"); 
        }
    }
    private void XMouseRotate()
    {
        float xMouseRotate = Input.GetAxis("Mouse X") * xMouseSensitivity;

        transform.Rotate(0f, xMouseRotate, 0f);
        Debug.Log("Y축 기준 X축 이동 감지");
    }

}
