using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivationPopupUI : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private PlayerInput playerInput;

    private void OnEnable()
    {
        if(playerInput != null)
        {
            playerInput.onControlsChanged += PlayerInput_onControlsChanged;

            HandleInput(playerInput);
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.onControlsChanged -= PlayerInput_onControlsChanged;
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void PlayerInput_onControlsChanged(PlayerInput input)
    {
        HandleInput(input);
    }

    public void HandleInput(PlayerInput input)
    {
        if (input.currentControlScheme == "Keyboard&Mouse") { ChangeUIPrompt("KEYBOARD"); }
        if (input.currentControlScheme == "Gamepad")
        {
            if (Gamepad.current.device.name.Contains("XInput")) { ChangeUIPrompt("XINPUT"); }
            else if (Gamepad.current.device.name.Contains("DualShock")) { ChangeUIPrompt("DUALSHOCK"); }
            else if (Gamepad.current.device.name.Contains("Switch")) { ChangeUIPrompt("SWITCH"); }
        }
    }

    public void ChangeUIPrompt(string deviceName)
    {

        switch (deviceName.ToUpper())
        {

            case "XINPUT":
                anim.SetBool("isKeyboard", false);
                anim.SetBool("isGamepadXbox", true);
                anim.SetBool("isGamepadPlaystation", false);
                anim.SetBool("isGamepadSwitch", false);
                break;
            case "SWITCH":
                anim.SetBool("isKeyboard", false);
                anim.SetBool("isGamepadXbox", false);
                anim.SetBool("isGamepadPlaystation", false);
                anim.SetBool("isGamepadSwitch", true);
                break;
            case "DUALSHOCK":
                anim.SetBool("isKeyboard", false);
                anim.SetBool("isGamepadXbox", false);
                anim.SetBool("isGamepadPlaystation", true);
                anim.SetBool("isGamepadSwitch", false);
                break;
            case "KEYBOARD":
            default:
                anim.SetBool("isKeyboard", true);
                anim.SetBool("isGamepadXbox", false);
                anim.SetBool("isGamepadPlaystation", false);
                anim.SetBool("isGamepadSwitch", false);
                break;
        }
    }
}