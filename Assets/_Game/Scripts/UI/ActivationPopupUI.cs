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

    private const string xboxDeviceNameContains = "xinput";
    private const string playstationDeviceNameContains = "dualshock";
    private const string switchDeviceNameContains = "switch";

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
        if (input.currentControlScheme == "Keyboard&Mouse") { ChangeUIPrompt(DeviceType.Keyboard); }
        if (input.currentControlScheme == "Gamepad")
        {
            if (Gamepad.current.device.name.ToLower().Contains(xboxDeviceNameContains)) { ChangeUIPrompt(DeviceType.GamepadXbox); }
            else if (Gamepad.current.device.name.ToLower().Contains(playstationDeviceNameContains)) { ChangeUIPrompt(DeviceType.GamepadPlaystation); }
            else if (Gamepad.current.device.name.ToLower().Contains(switchDeviceNameContains)) { ChangeUIPrompt(DeviceType.GamepadSwitch); }
        }
    }

    public void ChangeUIPrompt(DeviceType deviceType)
    {
        anim.SetBool("isKeyboard", deviceType == DeviceType.Keyboard);
        anim.SetBool("isGamepadXbox", deviceType == DeviceType.GamepadXbox);
        anim.SetBool("isGamepadPlaystation", deviceType == DeviceType.GamepadPlaystation);
        anim.SetBool("isGamepadSwitch", deviceType == DeviceType.GamepadSwitch);
    }
}