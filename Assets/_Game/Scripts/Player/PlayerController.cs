using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseMoveSpeed, runSpeed, currentSpeed, timeBetweenFootsteps;

    [SerializeField]
    private Light2D[] candleLights;

    [SerializeField]
    private string footstepsSfx;

    private bool isRunning, isActivatePressed, isPaused, disableMovement, isCandleOut;
    private PlayerInput playerInput;
    private Vector2 movePosition;
    private Animator anim;
    private Vector2 lastMove = new Vector2(0, -1);

    private CandleController candleController;
    private Coroutine footStepCoroutine;
    public bool IsRunning { get => isRunning; }
    public bool IsActivatePressed { get => isActivatePressed; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public bool DisableMovement { get => disableMovement; set => disableMovement = value; }
    public CandleController CandleController { get => candleController; set => candleController = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public bool IsRunning1 { get => isRunning; set => isRunning = value; }
    public bool IsActivatePressed1 { get => isActivatePressed; set => isActivatePressed = value; }
    public bool IsPaused1 { get => isPaused; set => isPaused = value; }
    public bool DisableMovement1 { get => disableMovement; set => disableMovement = value; }
    public bool IsCandleOut { get => isCandleOut; set => isCandleOut = value; }

    private void OnEnable()
    {
        EventManager.onCandleColorChanged += EventManager_onCandleColorChanged;
        EventManager.onCandleOut += EventManager_onCandleOut;
        EventManager.onCandleReset += EventManager_onCandleReset;
        if(playerInput != null)
        {
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].canceled += OnMove;
            playerInput.actions["Run"].performed += OnRun;
            playerInput.actions["Run"].canceled += OnRun;
            playerInput.actions["Activate"].performed += OnActivate;
            playerInput.actions["Activate"].canceled += OnActivate;
            playerInput.actions["Pause"].performed += OnPause;
        }
    }



    private void OnDisable()
    {
        EventManager.onCandleColorChanged -= EventManager_onCandleColorChanged;
        EventManager.onCandleOut -= EventManager_onCandleOut;
        EventManager.onCandleReset -= EventManager_onCandleReset;

        if (playerInput != null)
        {
            playerInput.actions["Move"].performed -= OnMove;
            playerInput.actions["Move"].canceled -= OnMove;
            playerInput.actions["Run"].performed -= OnRun;
            playerInput.actions["Run"].canceled -= OnRun;
            playerInput.actions["Activate"].performed -= OnActivate;
            playerInput.actions["Activate"].canceled -= OnActivate;
            playerInput.actions["Pause"].performed -= OnPause;
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        candleController = GameObject.FindGameObjectWithTag("Candle").GetComponent<CandleController>();
        anim.SetBool("Yellow", true);
        candleLights = GetComponentsInChildren<Light2D>();
    }

    private void Update()
    {
        if (!disableMovement)
        {
            currentSpeed = isRunning ? runSpeed : baseMoveSpeed;
            transform.position += (Vector3)movePosition * Time.deltaTime * currentSpeed;
        }
    }

    private void OnMove(InputAction.CallbackContext input)
    {
        if (!disableMovement)
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
                if (footStepCoroutine != null)
                {
                    StopCoroutine(footStepCoroutine);
                }
                footStepCoroutine = StartCoroutine(FootStep(timeBetweenFootsteps));
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }

    private void OnRun(InputAction.CallbackContext input)
    {
        if (!disableMovement)
        {
            isRunning = input.ReadValueAsButton();
        }
    }

    private void OnActivate(InputAction.CallbackContext input)
    {
        isActivatePressed = input.ReadValueAsButton();
    }

    private void OnPause(InputAction.CallbackContext input)
    {
        isPaused = !isPaused;
    }

    private IEnumerator FootStep(float timeBetwenStep)
    {
        while (movePosition != Vector2.zero && !disableMovement)
        {
            if (!string.IsNullOrWhiteSpace(footstepsSfx))
            {
                AudioManager.instance.PlaySound(footstepsSfx);
                yield return new WaitForSeconds(timeBetwenStep);
            }
        }
    }
    private void EventManager_onCandleColorChanged(CandleColor color)
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
    private void EventManager_onCandleOut()
    {
        isCandleOut = true;
        for (int i = 0; i < candleLights.Length; i++)
        {
            if (candleLights[i] != null)
            {
                candleLights[i].enabled = false;
            }
        }
    }

    private void EventManager_onCandleReset()
    {
        isCandleOut = false;
        for (int i = 0; i < candleLights.Length; i++)
        {
            if (candleLights[i] != null)
            {
                candleLights[i].enabled = true;
            }
        }
    }
}