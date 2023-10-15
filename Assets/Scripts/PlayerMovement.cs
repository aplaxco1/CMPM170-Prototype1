using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public GameObject playerObj;
    public float moveSpeed;
    public float groundDrag;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    float distToGround;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public KeyCode jumpKey = KeyCode.Space;
    
    bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        //grounded = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isOnGround()) { rb.drag = groundDrag; }
        PlayerInput();
        MovePlayer();
        SpeedControl();
    }


    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && canJump && isOnGround()) {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isOnGround()){
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);   
        }
        else {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);   

        }
    }

    private void SpeedControl() 
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed) {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private bool isOnGround()
    {
        distToGround = playerObj.GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    private void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() 
    {
        canJump = true;
    }
}
