using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseMoveSpeed, runSpeed, currentSpeed;

    private bool isRunning;
    private PlayerInput playerInput;
    private Vector2 movePosition;
    private Animator anim;
    private Vector2 directionFacing;
    private Vector2 lastMove = new Vector2(0, -1);
    private Vector2 lastInput = Vector2.zero;


    private void OnEnable()
    {
        if(playerInput != null)
        {
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].canceled += OnMove;
            playerInput.actions["Run"].performed += OnRun;
            playerInput.actions["Run"].canceled += OnRun;
        }
    }



    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.actions["Move"].performed -= OnMove;
            playerInput.actions["Move"].canceled -= OnMove;
            playerInput.actions["Run"].performed -= OnRun;
            playerInput.actions["Run"].canceled -= OnRun;
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        currentSpeed = isRunning ? runSpeed : baseMoveSpeed;
        transform.position += (Vector3)movePosition * Time.deltaTime * currentSpeed;
    }

    private void OnMove(InputAction.CallbackContext input)
    {
        movePosition = input.ReadValue<Vector2>();
        if (lastInput != movePosition &&  movePosition != Vector2.zero)
        {
            lastInput = movePosition;
            directionFacing = movePosition;
        }
        anim.SetFloat("MoveX", movePosition.x);
        anim.SetFloat("MoveY", movePosition.y);

        if (movePosition != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
            lastMove = movePosition;
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

    }

    private void OnRun(InputAction.CallbackContext input)
    {
        isRunning = input.ReadValueAsButton();
    }
}
