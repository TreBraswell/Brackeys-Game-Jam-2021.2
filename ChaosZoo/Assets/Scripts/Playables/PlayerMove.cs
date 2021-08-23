using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Refs
    [Header("Refrences")]
    private PlayerAnimator animator;
    private Rigidbody rb;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Collider collider;
    //Stats
    [Header("Stats")]
    public float move_Speed = 250f;
    public float run_Speed = 400f;
    public float speed;
    public float jumpHeight;

    //For raycast, can be deleted later
    [SerializeField]
    private float playerHeight;


    //private stuff for physisc
    private Vector3 moveDirection;
    private bool isGrounded;
    [Header("Drag")]
    public float gorundDrag = 6f;
    public float airDrag = 0f;
    private float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        playerHeight = collider.bounds.size.y;
        animator = GetComponent<PlayerAnimator>();
        speed = move_Speed;
    
    }

    // Update is called once per frame
    void FixedUpdate()

    {
        DragControl();
        Move();
    }


    private void Move()
    {
        if (!IsGrounded()) return;
       Vector2 inputAxis = new Vector2(Input.GetAxis(Axis.HORIZONTAL), Input.GetAxis(Axis.VERTICAL));

        moveDirection = new Vector3(inputAxis.x, 0, inputAxis.y).normalized;
        print(moveDirection.magnitude);
        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg+Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            rb.AddForce(moveDirection * speed*Time.deltaTime, ForceMode.Acceleration);
        }
     

        //Max speed
        float speedPercent = Mathf.Sqrt((run_Speed) / rb.drag) / Time.fixedDeltaTime;
        print(speedPercent);
        animator.SetFloat("moveSpeed", speedPercent);

        Jump();
       
        
    }

    void DragControl()
    {
        if (IsGrounded()) {

            rb.drag = gorundDrag;

        }
        else
        {
            rb.drag = airDrag;
        }

    }

    private void Jump()
    {
        if(IsGrounded() && Input.GetButton(Axis.JUMP))
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
      
        isGrounded = Physics.Raycast(transform.position, Vector3.down,playerHeight/2f+0.1f, groundLayer);
        return isGrounded;
    }

}
