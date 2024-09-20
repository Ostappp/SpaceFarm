using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 0.1f;
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask _groundMask;


    //private Player _player;
    private CharacterController _chController;
    private Vector2 _moveInput;

    private Vector3 _velocity;
    private bool _isGrounded;

    private void Start()
    {
        _chController = GetComponent<CharacterController>(); 
    }

    private void Update()
    {
        GroundCheck();
        Move();
        ApplyGravity();
    }
    //Set move direction
    public void OnMove(InputValue inputValue)
    {        
        _moveInput = inputValue.Get<Vector2>();
        
    }

    //Move and rotate player 
    private void Move()
    {
        Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y);
        if(movement!= Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), _rotationSpeed);
            _chController.Move(movement * _playerSpeed * Time.deltaTime);
        }

    }
    // Check if player is grounded
    private void GroundCheck()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Small value to keep player grounded
        }
    }

    // Apply gravity to player
    private void ApplyGravity()
    {
        if (!_isGrounded)
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
            _chController.Move(_velocity * Time.deltaTime);
        }
    }

}
