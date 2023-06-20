using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [Header("Movment")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMulti = 0.4f;
    [SerializeField] float moveMulti = 10f;
    float horMov;
    float verMov;

    [Header("Sprinting")]
    [SerializeField] float walkSped = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] public float gravity = 25f;
    [SerializeField] Transform groundCheck;
    bool isGrounded;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Misc")]
    [SerializeField] Transform orientation;
    [SerializeField] LayerMask layer;


    Vector3 moveDirection = Vector3.zero;
    Vector3 slopeDirection = Vector3.zero;
    Rigidbody rig;
    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 2.0f))
        {
            if (slopeHit.normal != Vector3.up)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private void Start()
    {
        Physics.IgnoreLayerCollision(11,12,true);
        rig = GetComponent<Rigidbody>();
        rig.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, layer);

        MyInput();
        CalculateDrag();
        CalculateSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horMov = Input.GetAxisRaw("Horizontal");
        verMov = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verMov + orientation.right * horMov;
    }

    void CalculateSpeed()
    {
        if(Input.GetKey(sprintKey) && isGrounded)
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        else
            moveSpeed = Mathf.Lerp(moveSpeed, walkSped, acceleration * Time.deltaTime);
    }

    void CalculateDrag()
    {
        if (isGrounded)
            rig.drag = 6;
        else
            rig.drag = 0;
    }

    void Jump()
    {
        if(isGrounded)
        {
            rig.velocity = new Vector3(rig.velocity.x, 0, rig.velocity.z);
            rig.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    private void FixedUpdate()
    {
        MoveOnInput();

        rig.AddForce(-transform.up * gravity, ForceMode.Impulse);
    }

    void MoveOnInput()
    {
        if(isGrounded && !OnSlope())
            rig.AddForce(moveDirection.normalized * moveSpeed * moveMulti, ForceMode.Force);
        else if (isGrounded && OnSlope())
            rig.AddForce(slopeDirection.normalized * moveSpeed * moveMulti, ForceMode.Force);
        else if(!isGrounded)
            rig.AddForce(moveDirection.normalized * moveSpeed * airMulti, ForceMode.Force);
    }
}
