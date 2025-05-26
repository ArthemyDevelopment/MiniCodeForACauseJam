using System;
using System.Collections;
using ArthemyDev.ScriptsTools;
using Sirenix.OdinInspector;
using UnityEngine;

public class BallJoint : MonoBehaviour
{
    
    [FoldoutGroup("Components")][SerializeField] private Rigidbody rb;
    [FoldoutGroup("Components")][SerializeField] private SpringJoint Joint;
    [FoldoutGroup("Components")][SerializeField] private Rigidbody Hand;
    
    [FoldoutGroup("States values")]public BallState curState = BallState.Grab;
    [FoldoutGroup("States values")][SerializeField] private Vector3 BallConnectedAnchor;
    [FoldoutGroup("States values")][SerializeField] private bool isConnected;
    [FoldoutGroup("States values")][SerializeField] private float ThrowForce;
    [FoldoutGroup("States values")][SerializeField] private float layerChangeDelay= 0.5f;
    [FoldoutGroup("States values")][SerializeField] private Vector3 ThrowDirection;

    [FoldoutGroup("Rb states values")] [SerializeField] private float RbMassConnected,RbMassDisconnected, RbLinearDampConnected, RbLinearDampDisconected, RbAngularDampConnected, RbAngularDampDisconected;
    

    [FoldoutGroup("SpringJoint states values")]public float JointSpring,JointSpringOnReturn, JointDamper, JointMinDis, JointMaxDis, JointMaxDisOnReturn;


    

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
        
        DropBall();
    }


    private void Update()
    {
        //if(!isConnected) Joint.connectedAnchor = transform.position;
    }


    [Button]
    public void ThrowBall(Vector3 direction)
    {
        ThrowDirection = direction - transform.position;
        ThrowDirection.Normalize();
        rb.AddForce((ThrowDirection+(Vector3.up/2))*ThrowForce);
        curState = BallState.Throw;
    }

    [Button]
    public void DropBall()
    {
        Destroy(Joint);
        curState = BallState.Throw;
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
        curState = BallState.Returning;
        Joint = gameObject.AddComponent<SpringJoint>();
        Joint.autoConfigureConnectedAnchor = false;
        Joint.spring = JointSpringOnReturn;
        Joint.damper = JointDamper;
        Joint.minDistance = JointMinDis;
        Joint.maxDistance = JointMaxDisOnReturn;

        gameObject.layer = 12;
        isConnected = true;
        Joint.connectedBody = Hand;
        Joint.connectedAnchor = BallConnectedAnchor;
        rb.linearDamping = RbLinearDampConnected;
        rb.angularDamping = RbAngularDampConnected;
        rb.mass = RbMassConnected;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerTriggers")&& curState== BallState.Returning)
        {
            gameObject.layer = 7;
            Joint.maxDistance = JointMaxDis;
            Joint.spring = JointSpring;
            curState = BallState.Grab;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            HitboxRecognitionSystem.ApplyDamage(other, 1);
        }
    }
}
