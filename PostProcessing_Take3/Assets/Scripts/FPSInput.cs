using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Scripts/FPS Input")]

public class FPSInput : MonoBehaviour
{
    [Header("Movement Attributes")]

    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _gravity = -9.8f;

    [SerializeField] float _jumpSpeed = 15.0f;
	
	public static bool AllowMovement;
	
	
	
    float _verticalVelocity;

    CharacterController _controller;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
		AllowMovement = true;

    }

    
    void Update()
    {
        if (AllowMovement == true)
		{
	        if(Input.GetKeyDown(KeyCode.LeftShift))
	        {
	            _speed += 5.0f;
	        }
	        if (Input.GetKeyUp(KeyCode.LeftShift))
	        {
	            _speed -= 5.0f;
	        }

	        // Gather input info
	        float deltaX = Input.GetAxis("Horizontal") * _speed;
	        float deltaZ = Input.GetAxis("Vertical") * _speed;

	        // Gather movement vector
	        Vector3 movement = new(deltaX, 0.0f, deltaZ);

	        // Clamp diagonal movement
	        movement = Vector3.ClampMagnitude(movement, _speed);

	        if (_controller.isGrounded)
	        {
	            _verticalVelocity = _gravity;

	            if (Input.GetButtonDown("Jump"))
	            {
	                _verticalVelocity += _jumpSpeed;
	            }
	        }
	        else
	        {
	            _verticalVelocity +=_gravity * 3.0f * Time.deltaTime;
	        }

	        Debug.Log(_verticalVelocity);


	        // Apply gravity after X and Z have been clamped
	        movement.y = _verticalVelocity;

	        // Consider frame rate
	        movement *= Time.deltaTime;

	        // Convert movement vector to the rotation of the player
	        movement = transform.TransformDirection(movement);

	        // Move!
	        _controller.Move(movement);
			
		}
        
    }
}
