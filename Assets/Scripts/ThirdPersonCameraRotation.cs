using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraRotation : MonoBehaviour
{

    [Header ("Player Reference")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    [Header ("Rotation Speed")]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate player orientation
        Vector3 playerDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = playerDir.normalized;

        // rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero) {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        
    }
}
