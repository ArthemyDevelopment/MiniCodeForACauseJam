using System;
using GAP_LaserSystem;
using Sirenix.OdinInspector;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 InputDirection;
    public Vector3 MoveMagnitude;
    public Rigidbody rb;
    public LaserScript ballLaser;
    public float velocity;
    public BallJoint ballJoint;


    private void Awake()
    {
        if(rb==null)rb = GetComponent<Rigidbody>();

    }


    void Start()
    {
        ballLaser.EnableLaser();
        ballLaser.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        InputDirection.x = Input.GetAxis("Horizontal");
        InputDirection.z = Input.GetAxis("Vertical");
        ballLaser.UpdateLaser();
        InputDirection.Normalize();
        MoveMagnitude = InputDirection * velocity;

        
        if(Input.GetMouseButtonDown(0)) ThrowBall();
        if(Input.GetMouseButtonDown(1)) GrabBall();


    }

    private void FixedUpdate()
    {
        rb.Move(transform.position+MoveMagnitude, Quaternion.identity);
    }


    [Button]
    public void ThrowBall()
    {
        DropBall();
        ballJoint.ThrowBall();
    }
    
    [Button]
    public void DropBall()
    {
        ballJoint.DropBall();
        ballLaser.DisableLaserCaller(0);
        
    }

    [Button]
    public void GrabBall()
    {
        ballJoint.GrabBall();
        ballLaser.EnableLaser();
        
    }
}
