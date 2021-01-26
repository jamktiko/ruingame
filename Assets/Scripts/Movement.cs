using UnityEngine;


[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    protected CharacterController _characterController;
    protected InputManager _inputManager;
    
    //Get from entity stats
    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private float sprintSpeed = 15f;
    
    
    [SerializeField]
    protected float turnSmoothing = 0.2f;
    [SerializeField]
    protected float sprintSmoothing = 0.1f;

    protected float _currentMovementSpeed;
    protected float _currentSmoothing;
    protected float _turnSmoothVelocity;
    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _characterController = GetComponent<CharacterController>();
        _currentMovementSpeed = movementSpeed;
        _currentSmoothing = turnSmoothing;
    }
    private void FixedUpdate()
    {
        if (_inputManager._directionInput.magnitude >= 0.1f)
        {
            Move();
        }

        if (_inputManager.sprintInput)
        {
            Sprint();
        }
        else
        {
            _currentMovementSpeed = movementSpeed;
            turnSmoothing = _currentSmoothing;
        }
    }
    protected virtual void Sprint()
    {
        _currentMovementSpeed = sprintSpeed;
        turnSmoothing = sprintSmoothing;
    }

    protected virtual void Move()
    {

    }
}