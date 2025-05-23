using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class BallJoint : MonoBehaviour
{
    public Rigidbody rb;
    public SpringJoint Joint;
    public Vector3 BallConnectedAnchor;
    public Rigidbody Hand;
    public bool isConnected;
    public float ThrowForce;

    
    public float RbMassConnected,RbMassDisconnected, RbLinearDampConnected, RbLinearDampDisconected, RbAngularDampConnected, RbAngularDampDisconected;
    public float JointSpring, JointDamper, JointMinDis, JointMaxDis;


    private void Awake()
    {
        if (Joint != null)
        {
            BallConnectedAnchor = Joint.connectedAnchor;
            JointSpring = Joint.spring;
            JointDamper = Joint.damper;
            JointMinDis = Joint.minDistance;
            JointMaxDis = Joint.maxDistance;
        }
    }


    private void Update()
    {
        if(!isConnected) Joint.connectedAnchor = transform.position;
    }


    [Button]
    public void ThrowBall()
    {
        //DropBall();
        
        rb.AddForce((Vector3.forward+(Vector3.up/2))*ThrowForce);
    }

    [Button]
    public void DropBall()
    {
        //Destroy(Joint);
        Joint.connectedBody = null;
        Joint.connectedAnchor = transform.position;
        rb.linearDamping = RbLinearDampDisconected;
        rb.angularDamping = RbAngularDampDisconected;
        rb.mass = RbMassDisconnected;
        isConnected = false;
        
    }

    [Button]
    public void GrabBall()
    {
        /*Joint = gameObject.AddComponent<SpringJoint>();
        Joint.autoConfigureConnectedAnchor = false;
        Joint.spring = JointSpring;
        Joint.damper = JointDamper;
        Joint.minDistance = JointMinDis;
        Joint.maxDistance = JointMaxDis;*/
        isConnected = true;
        Joint.connectedBody = Hand;
        Joint.connectedAnchor = BallConnectedAnchor;
        rb.linearDamping = RbLinearDampConnected;
        rb.angularDamping = RbAngularDampConnected;
        rb.mass = RbMassConnected;

    }
}
