using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance { get; private set; }

    public event Action OnAttackInput = delegate { };
    public event Action OnJumpInput = delegate { };
    public event Action OnSlideInput = delegate { };
    public event Action OnAnyInteract = delegate { };

    private PlayerControls input;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        input = new PlayerControls();
    }

    private void OnEnable()
    {
        input.Player.Enable();

        //input.Player.Attack.performed += HandleAttackInput;
        input.Player.Jump.performed += HandleJumpInput;
        //input.Player.Slide.performed += HandleSlideInput;

        input.UI.Submit.performed += ctx => OnAnyInteract.Invoke();
    }

    private void OnDisable()
    {
        input.Player.Disable();

        //input.Player.Attack.performed -= HandleAttackInput;
        //input.Player.Jump.performed -= HandleJumpInput;
        //input.Player.Slide.performed -= HandleSlideInput;

        input.UI.Submit.performed -= ctx => OnAnyInteract.Invoke();
    }

    private void HandleAttackInput(InputAction.CallbackContext obj) => OnAttackInput.Invoke();
    private void HandleJumpInput(InputAction.CallbackContext obj) => OnAttackInput.Invoke();
    private void HandleSlideInput(InputAction.CallbackContext obj) => OnAttackInput.Invoke();

    public void EnableUIInputs(bool enable)
    {
        if (enable)
        {
            input.UI.Enable();
        }
        else
        {
            input.UI.Disable();
        }
    }

    public void EnablePlayerInputs(bool enable)
    {
        if (enable)
        {
            input.Player.Enable();
        }
        else
        {
            input.Player.Disable();
        }
    }
}


