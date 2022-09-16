using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180.0f;
    public float jumpPower = 5f;

    float moveDir = default;
    float rotateDir = default;

    bool isJump = false;


    PlayerInputAction inputAction;              // PlayerInputAction타입의 변수 선언
    Rigidbody rigid;

    private void Awake()
    {
        inputAction = new PlayerInputAction();  //인스턴스 생성
        rigid = GetComponent<Rigidbody>();

    }
    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Move.performed += OnMoveInput;
        inputAction.Player.Move.canceled += OnMoveInput;
        inputAction.Player.Jump.performed += OnJumpInput;

    }



    private void OnDisable()
    {
        inputAction.Player.Disable();
        inputAction.Player.Move.performed -= OnMoveInput;
        inputAction.Player.Move.canceled -= OnMoveInput;
        inputAction.Player.Jump.performed -= OnJumpInput;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveDir = input.y;
        rotateDir = input.x;
    }

    private void OnJumpInput(InputAction.CallbackContext _)
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        rigid.MovePosition(rigid.position + moveSpeed * Time.fixedDeltaTime * moveDir * transform.forward);
    }
    void Rotate()
    {

        rigid.MoveRotation(rigid.rotation * Quaternion.AngleAxis(rotateSpeed * rotateDir * Time.fixedDeltaTime, transform.up));
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(0,rotateDir*rotateSpeed*Time.deltaTime,0));

    }
    void Jump()
    {
        if (!isJump)
        {
            rigid.AddForce(jumpPower * transform.up, ForceMode.Impulse);
        }
        isJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;
    }
}
