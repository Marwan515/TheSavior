using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float playerSpeed = 1.0f;
    [SerializeField] float sprintMultiplier = 2.0f;
    private bool sprinting = false;
    private Vector2 moveInput;
    // the 3 variables below are used to check if player object is colliding with ground
    [Header("Grounded Check")]
    //[SerializeField] Vector3 boxSize;
    [SerializeField] float groundCheckDistance = 0.2f;
    //[SerializeField] float maxDistance;
    [SerializeField] float gravityScale = 2.0f;
    [SerializeField] LayerMask layerMask;
    // keep count of jumps
    [Header("Jump Parameters")]
    [SerializeField] float jumpForce = 200;
    private bool doubleJumpAvailable = false;
    private bool hasJumped = false;
    // Crouch Variable
    [Header("Crouch Parameters")]
    [SerializeField] float crouchSpeed;
    [SerializeField] float crouchYScale;
    private bool isCrouched;
    private float startYScale;
    
    // Dash parameters
    [Header("Dashing")]
    public float dashSpeed;
    public float cooldownSec;
    float lastDashed;

    private bool isGrounded = true;
    private Rigidbody rb;
    
    private Animator anim;
    [Header("Input Player")]
    private PlayerInputActions playerInputs;
    
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        startYScale = transform.localScale.y;
        playerInputs = new PlayerInputActions();
        isCrouched = false;
    }

    private void OnEnable() {
        playerInputs.PlayerActions.Enable();
        playerInputs.PlayerActions.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInputs.PlayerActions.Movement.canceled += _ => HandleMovement(Vector2.zero);
        playerInputs.PlayerActions.Sprint.started += _ => sprinting = true;
        playerInputs.PlayerActions.Sprint.canceled += _ => sprinting = false;
        playerInputs.PlayerActions.Jump.performed += _ => hasJumped = true;
        playerInputs.PlayerActions.Crouch.performed += _ => HandleCrouch();
        playerInputs.PlayerActions.Dodge.performed += _ => HandleDashing();
    }

    private void OnDisable() {
        playerInputs.PlayerActions.Disable();
    }


    private void FixedUpdate() {
        isGrounded = GroundCheck();
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        }
        HandleMovement(moveInput);
        HandleJump();
    }


    void HandleMovement(Vector2 direction) {
        Vector3 currentMovementStatus = new Vector3(direction.x, 0, direction.y);
        float speed = sprinting ? sprintMultiplier : playerSpeed;
        anim.SetFloat("X Axis", direction.x * speed);
        anim.SetFloat("Z Axis", direction.y * speed);
        if (direction == Vector2.zero) {
            rb.velocity = Vector3.zero;
            moveInput = direction;
        } else {
            float maxSpeed = isCrouched ? crouchSpeed : speed;
            Debug.Log(maxSpeed);
            rb.velocity = maxSpeed * currentMovementStatus;
        }
        

    }
    public void HandleJump(){
        if (hasJumped) {
            Debug.Log(isGrounded);
            if (isGrounded)
            {
                doubleJumpAvailable = false;
            }

            if (isGrounded || doubleJumpAvailable) {
                if (isCrouched) {
                    isCrouched = false;
                }

                if (!doubleJumpAvailable)
                {
                    anim.SetTrigger("Jump");
                } else {
                    anim.SetTrigger("DoubleJump");
                }
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJumpAvailable = !doubleJumpAvailable;
                hasJumped = false;
            }
        }
    }

    private void HandleCrouch() {
        isCrouched = !isCrouched;
        if (isCrouched && isGrounded) {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            anim.SetBool("Crouched", true);
        } else {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            anim.SetBool("Crouched", false);
        }
    }

    private void HandleDashing() 
    {
        if (isGrounded) {
            if (Time.time - lastDashed < cooldownSec)
            {
                return;
            }
            lastDashed = Time.time;
            if (moveInput.x > 0) {
                rb.AddForce(Vector3.right * dashSpeed, ForceMode.Impulse);
                anim.SetTrigger("DodgeRight");   
            } else if (moveInput.x < 0) {
                rb.AddForce(Vector3.left * dashSpeed, ForceMode.Impulse);
                anim.SetTrigger("DodgeLeft");
            }
        }
    }
    
    /*void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }*/

    public bool GroundCheck() 
    {
       /* if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, layerMask))
        {
            return true;
        } 
        else 
        {
            return false;
        }*/
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, layerMask);

    }
}
