﻿
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerInput.IGameplayActions
{
    //Gameplay
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCanceledEvent = delegate { };
    public event UnityAction AttackEvent = delegate { };
    public event UnityAction InteractEvent = delegate { };
    public event UnityAction OpenInventoryEvent = delegate { }; 
    public event UnityAction PauseEvent = delegate { };
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<Vector2> CameraMoveEvent = delegate { };
    public event UnityAction EnableMouseControlCameraEvent = delegate { };
    public event UnityAction DisableMouseControlCameraEvent = delegate { };
    public event UnityAction StartedRunning = delegate { };
    public event UnityAction StoppedRunning = delegate { };

    public event UnityAction ActivateSkill1 = delegate { };
    public event UnityAction ActivateSkill2 = delegate { };
    public event UnityAction ActivateSkill3 = delegate { };
    public event UnityAction ActivateSprintSkill = delegate { };

    private PlayerInput _playerInput;
    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.Gameplay.SetCallbacks(this);
        }
        EnablePlayerInput();
    }
    private void OnDisable()
    {
        DisableAllInput();
    }
    
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            AttackEvent.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }
    

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            JumpEvent.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            AttackEvent.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            InteractEvent.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("No method for pause");
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        CameraMoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
    }
    
    public void OnRun(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                StartedRunning.Invoke();
                break;
            case InputActionPhase.Canceled:
                StoppedRunning.Invoke();
                break;
        }
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ActivateSkill1.Invoke();
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ActivateSkill2.Invoke();
    }

    public void OnSkill3(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ActivateSkill3.Invoke();
            
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ActivateSprintSkill.Invoke();
    }

    public void DisableAllInput()
    {
        _playerInput.Gameplay.Disable();
        //_playerInput.Menus.Disable();
    }
    public void EnablePlayerInput()
    {
        //Disable other input methods
        _playerInput.Gameplay.Enable();
        //_playerInput.Menus.Disable();
    }

}
