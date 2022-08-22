using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseMoveSpeed, runSpeed, currentSpeed;

    [SerializeField]
    private CandleColor currentCandleColor;

    private bool isRunning, isActivatePressed;
    private PlayerInput playerInput;
    private Vector2 movePosition;
    private Animator anim;
    private Vector2 lastMove = new Vector2(0, -1);

    public bool IsActivatePressed { get => isActivatePressed; set => isActivatePressed = value; }
    public CandleColor CurrentCandleColor { get => currentCandleColor; set => currentCandleColor = value; }

    private void OnEnable()
    {
        if(playerInput != null)
        {
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].canceled += OnMove;
            playerInput.actions["Run"].performed += OnRun;
            playerInput.actions["Run"].canceled += OnRun;
            playerInput.actions["Activate"].performed += OnActivate;
            playerInput.actions["Activate"].canceled += OnActivate;
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
            playerInput.actions["Activate"].performed -= OnActivate;
            playerInput.actions["Activate"].canceled -= OnActivate;
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

    private void OnActivate(InputAction.CallbackContext input)
    {
        isActivatePressed = input.ReadValueAsButton();
    }

    public IEnumerator ChangeCandleColor(CandleColor color, float timeToLast)
    {
        currentCandleColor = color;
        yield return new WaitForSeconds(timeToLast);
        currentCandleColor = CandleColor.Yellow;
    }
}
