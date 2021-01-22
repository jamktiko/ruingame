using UnityEngine;

public class PlayerInput : InputManager
{
    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        _directionInput = new Vector3(horizontal, 0f, vertical).normalized;
            
        sprintInput = Input.GetKey("left shift");
            
        dodgeInput = Input.GetKey(KeyCode.Space);
            
        attackInput = Input.GetMouseButtonDown(0);
    }

    public override void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}