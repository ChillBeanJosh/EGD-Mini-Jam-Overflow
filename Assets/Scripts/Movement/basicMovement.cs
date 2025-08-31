using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;


    [Header("Ice Control")]
    private Coroutine iceCoroutine;
    private float defaultGroundDrag;
    public float iceSlipTime;
    public overflowBar meter;
    private Vector3 lockedMoveDirection;
    private bool isSliding = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        defaultGroundDrag = groundDrag;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        if (isSliding) return;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput * Time.deltaTime;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ice"))
        {
            if (iceCoroutine != null)
            {
                StopCoroutine(iceCoroutine);
            }

            iceCoroutine = StartCoroutine(SetIceDragForSeconds(iceSlipTime)); 
        }
    }

    private IEnumerator SetIceDragForSeconds(float duration)
    {
        //lock direction:
        lockedMoveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        isSliding = true;

        //slidey movement:
        meter.currentValue = meter.currentValue + 10;
        groundDrag = 1f;

        yield return new WaitForSeconds(duration);

        //return to default:
        groundDrag = defaultGroundDrag;
        isSliding = false;
        iceCoroutine = null;
    }

}
