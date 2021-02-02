using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerInput.IGameplayActions
{
    //Gameplay
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction jumpCanceledEvent = delegate { };
    public event UnityAction attackEvent = delegate { };
    public event UnityAction interactEvent = delegate { };
    public event UnityAction openInventoryEvent = delegate { }; 
    public event UnityAction pauseEvent = delegate { };
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction<Vector2, bool> cameraMoveEvent = delegate { };
    public event UnityAction enableMouseControlCameraEvent = delegate { };
    public event UnityAction disableMouseControlCameraEvent = delegate { };
    public event UnityAction startedRunning = delegate { };
    public event UnityAction stoppedRunning = delegate { };

    public event UnityAction activateSkill1 = delegate { };
    public event UnityAction activateSkill2 = delegate { };
    public event UnityAction activateSkill3 = delegate { };
    public event UnityAction activateSprintSkill = delegate { };

    private PlayerInput _playerInput;
    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.Gameplay.SetCallbacks(this);
            //UI
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
            attackEvent.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            jumpEvent.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            attackEvent.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            interactEvent.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        cameraMoveEvent.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            enableMouseControlCameraEvent.Invoke();

        if (context.phase == InputActionPhase.Canceled)
            disableMouseControlCameraEvent.Invoke();
    }
    
    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
    
    public void OnRun(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                startedRunning.Invoke();
                break;
            case InputActionPhase.Canceled:
                stoppedRunning.Invoke();
                break;
        }
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            activateSkill1.Invoke();
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            activateSkill2.Invoke();
    }

    public void OnSkill3(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            activateSkill3.Invoke();
            
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            activateSprintSkill.Invoke();
    }

    public void DisableAllInput()
    {
        _playerInput.Gameplay.Disable();
        _playerInput.Menus.Disable();
    }
    public void EnablePlayerInput()
    {
        //Disable other input methods
        _playerInput.Gameplay.Enable();
        _playerInput.Menus.Disable();
    }

}
