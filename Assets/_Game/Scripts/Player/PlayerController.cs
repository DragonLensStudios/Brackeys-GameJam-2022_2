using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseMoveSpeed, runSpeed, currentSpeed;

    private bool isRunning, isActivatePressed;
    private PlayerInput playerInput;
    private Vector2 movePosition;
    private Animator anim;
    private Vector2 lastMove = new Vector2(0, -1);

    private CandleController candleController;

    public bool IsActivatePressed { get => isActivatePressed; set => isActivatePressed = value; }
    public CandleController CandleController { get => candleController; set => candleController = value; }
    public Animator Anim { get => anim; set => anim = value; }

    private void OnEnable()
    {
        EventManager.onCandleColorChanged += EventManager_onCandleColorChanged;
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
        EventManager.onCandleColorChanged -= EventManager_onCandleColorChanged;

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
        candleController = GameObject.FindGameObjectWithTag("Candle").GetComponent<CandleController>();
        anim.SetBool("Yellow", true);
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

    private void EventManager_onCandleColorChanged(CandleColor color, float timeToLast)
    {
        switch (color)
        {
            case CandleColor.Yellow:
                anim.SetBool("Yellow", true);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", false);
                break;
            case CandleColor.Red:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", true);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", false);
                break;
            case CandleColor.Purple:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", true);
                anim.SetBool("Blue", false);
                break;
            case CandleColor.Blue:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", true);
                break;
        }
    }
}