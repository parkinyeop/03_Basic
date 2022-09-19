using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180.0f;
    public float jumpPower = 5f;

    float moveDir = default;
    float rotateDir = default;

    public bool isJump = false;
    public bool isMove = false;

    Vector3 usePosition = Vector3.zero;
    float useRadius = 1.0f;

    PlayerInputAction inputAction;              // PlayerInputAction타입의 변수 선언
    Rigidbody rigid;
    Animator anim;
    Platform plat;

    private void Awake()
    {
        inputAction = new PlayerInputAction();  //인스턴스 생성
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }

    private void Start()
    {
        GameObject platform = GameObject.Find("Platform");
        if (platform != null)
            plat = platform.GetComponent<Platform>();
    }
    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Move.performed += OnMoveInput;
        inputAction.Player.Move.canceled += OnMoveInput;
        inputAction.Player.Jump.performed += OnJumpInput;
        inputAction.Player.PlatformMove.performed += OnPlatformMove;
        inputAction.Player.PlatformMove.canceled += OnPlatformMove;
    }

    private void OnPlatformMove(InputAction.CallbackContext _)
    {
        plat.isPlatformMoveOn = true;
        anim.SetTrigger("Use");
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
        inputAction.Player.Move.performed -= OnMoveInput;
        inputAction.Player.Move.canceled -= OnMoveInput;
        inputAction.Player.Jump.performed -= OnJumpInput;
        inputAction.Player.PlatformMove.performed -= OnPlatformMove;
        inputAction.Player.PlatformMove.canceled -= OnPlatformMove;
    }
    private void OnMoveInput(InputAction.CallbackContext context)
    {

        Vector2 input = context.ReadValue<Vector2>();

        moveDir = input.y;
        rotateDir = input.x;

        anim.SetBool("isWalk", !context.canceled); //이동키의 입력 상태를 체크
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
        if (collision.gameObject.tag == "Ground" || transform.position.y <= 0)
            isJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + usePosition, useRadius);
    }
}
