using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDist = 0.5f;
    [SerializeField] float minJumpHeight = 1.5f;
    [SerializeField] float wallRunGrav;
    [SerializeField] float wallJumpForce;
    [SerializeField] LayerMask runnableWall;

    [Header("Camera")]
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt { get; private set; }

    bool wallLeft = false;
    bool wallRight = false;

    private Rigidbody rig;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    float startGravity;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        startGravity = transform.GetComponent<Movment>().gravity;
    }

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
    }
    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDist, runnableWall);
        wallRight = Physics.Raycast(transform.position, orientation.right,out rightWallHit, wallDist, runnableWall);
    }

    private void Update()
    {
        CheckWall();
        if (CanWallRun())
        {
            if (wallLeft)
                StartWallRun();
            else if (wallRight)
                StartWallRun();
            else
                StopWallRun();
        }
        else
            StopWallRun();
    }

    void StartWallRun()
    {
        rig.useGravity = false;
        rig.AddForce(Vector3.down * wallRunGrav, ForceMode.Force);
        transform.GetComponent<Movment>().gravity = 0;

        if (wallLeft)
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if(wallRight)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(wallLeft)
            {
                Vector3 WRJumpDir = transform.up + leftWallHit.normal;
                rig.velocity = new Vector3(rig.velocity.x, 0, rig.velocity.z);
                rig.AddForce(WRJumpDir * wallJumpForce * 100, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 WRJumpDir = transform.up + rightWallHit.normal;
                rig.velocity = new Vector3(rig.velocity.x, 0, rig.velocity.z);
                rig.AddForce(WRJumpDir * wallJumpForce * 100, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        rig.useGravity = true;
        transform.GetComponent<Movment>().gravity = startGravity;
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}
