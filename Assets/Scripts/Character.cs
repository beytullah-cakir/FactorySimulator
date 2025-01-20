using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
   public float speed = 5.0f;
    public float rotationSpeed = 700.0f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector2 moveInput;
    private PlayerInputActions inputActions;

    private Animator anm;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions();
        anm=GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        //karakterin hareketi
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        // karakterin dönüşü ve ileri gitmesi
        if (direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

           
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        // yerçekimi
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        anm.SetBool("Run", direction.magnitude >= 0.1f);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
}