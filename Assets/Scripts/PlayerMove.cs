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

    private void Move()
    {
        transform.Translate(new Vector3(playerInput.xInput, 0, playerInput.zInput) * moveSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

}
