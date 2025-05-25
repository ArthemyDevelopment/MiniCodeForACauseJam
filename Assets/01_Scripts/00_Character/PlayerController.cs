using System;
using GAP_LaserSystem;
using Sirenix.OdinInspector;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [FoldoutGroup("Components")][SerializeField] private BallJoint ballJoint;
    [FoldoutGroup("Components")][SerializeField] private LaserScript ballLaser;
    [FoldoutGroup("Components")][SerializeField] private Rigidbody rb;
    [FoldoutGroup("Components")][SerializeField] private PlayerAnimationsController anims;
    [FoldoutGroup("Move Stats")][SerializeField] private Vector3 InputDirection;
    [FoldoutGroup("Move Stats")][SerializeField] private Vector3 MoveMagnitude;
    [FoldoutGroup("Move Stats")][SerializeField] private float velocity;
    [FoldoutGroup("Move Stats")][SerializeField] private float RollForce;
    [FoldoutGroup("Move Stats")][SerializeField]private bool IsRoll;
    [SerializeField]private Vector3 MovePos;
    [SerializeField]private Vector3 MoveRot;
    [FoldoutGroup("Raycast Stats")] [SerializeField] private LayerMask layersToHit;
    [FoldoutGroup("Raycast Stats")] [SerializeField] private Vector3 hitPosition;
    private Ray ray;
    private RaycastHit hitPoint;
    


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

        if (InputDirection.magnitude == 0)
        {
            MoveMagnitude = Vector3.zero;
            anims.TriggerAnim(PlayerAnims.Idle);
        }
        else
        {
            MoveRot.y = Mathf.Atan2 (InputDirection.x, InputDirection.z)* Mathf.Rad2Deg;
            MoveMagnitude = InputDirection * velocity;
            anims.TriggerAnim(PlayerAnims.Run);
        }


        if (Input.GetButtonDown("Jump"))
        {
            
            anims.TriggerAnim(PlayerAnims.Roll);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitPoint, 100, layersToHit))
            {
                hitPosition = hitPoint.point;
            }
            if(ballJoint.curState== BallState.Grab)ThrowBall();
            anims.TriggerAnim(PlayerAnims.Throw);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(ballJoint.curState== BallState.Throw)GrabBall();
            if(ballJoint.curState==BallState.Grab)DropBall();
        }


    }

    private void FixedUpdate()
    {
        MovePos = transform.position + MoveMagnitude;

        if (!IsRoll)
        {
            rb.MovePosition(MovePos);
            rb.MoveRotation(Quaternion.Euler(MoveRot));
        }
    }

    public void SetRollState()
    {
        IsRoll = true;
        Debug.Log("Roll");
        rb.AddForce(transform.forward*RollForce, ForceMode.Impulse);
        gameObject.layer = 14;
    }

    public void RemoveRollState()
    {
        IsRoll = false;
        gameObject.layer = 6;
    }


    [Button]
    public void ThrowBall()
    {
        DropBall();
        ballJoint.ThrowBall(hitPosition);
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
