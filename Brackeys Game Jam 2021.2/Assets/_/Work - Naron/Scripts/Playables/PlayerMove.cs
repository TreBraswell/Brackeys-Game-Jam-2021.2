namespace BGJ20212.Game.Naron
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerMove : MonoBehaviour
    {

        //Refs
        [Header("Refrences")] [SerializeField] private PlayerAnimator animator;
        private Rigidbody rb;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Collider collider;

        private Transform cameraTransform;

        //Stats
        [Header("Stats")] public float move_Speed = 250f;
        public float run_Speed = 400f;
        public float speed;
        public float jumpHeight;

        private bool isGrounded;
        private bool isJumping;
        private bool isAttacking;
        private bool canAttack;

        private bool isStanding;
        private bool isSpriting;




        //For raycast, can be deleted later
        [SerializeField] private float playerHeight;




        //private stuff for physisc
        private Vector3 direction;
        [Header("Drag")] public float gorundDrag = 6f;
        public float airDrag = 2f;
        private float turnSmoothTime = 0.2f;
        private float turnSmoothVelocity;



        // Start is called before the first frame update
        void Start()
        {
            cameraTransform = Camera.main.transform;
            rb = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            playerHeight = collider.bounds.size.y;

            speed = move_Speed;


            canAttack = true;

        }

        private void Update()
        {
            CheckInput();
        }

        // Update is called once per frame
        void FixedUpdate()

        {
            DragControl();
            Move();
            GroundCheck();
        }

        #region Movement

        private void Move()
        {
            if (!isGrounded)
            {
                animator.SetBool("Move", false);
            }
            else
            {
                animator.SetBool("Move", true);
            }

            Vector2 inputAxis = new Vector2(Input.GetAxis(Axis.HORIZONTAL), Input.GetAxis(Axis.VERTICAL));

            direction = new Vector3(inputAxis.x, 0f, inputAxis.y).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                    cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
            }


            //Max speed
            float speedPercent = rb.velocity.sqrMagnitude / run_Speed / run_Speed;
            
            animator.SetFloat("moveSpeed", speedPercent);



        }

        void DragControl()
        {
            if (isGrounded)
            {

                rb.drag = gorundDrag;

            }
            else
            {
                rb.drag = airDrag;
            }

        }

        private void WalkAndSprint()
        {
            if (isSpriting)
            {
                isSpriting = false;
                speed = move_Speed;
            }
            else
            {

                isSpriting = true;
                speed = run_Speed;

            }
        }

        #endregion

        private void CheckInput()
        {
            if (Input.GetButtonDown(Axis.JUMP))
            {
                TryJump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                WalkAndSprint();
            }

            if (Input.GetMouseButtonDown(0))
            {

                Attack();
            }

            //Stand
            if (Input.GetKeyDown(KeyCode.O))
            {
                ToggleStand();
            }

        }

        private void GroundCheck()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2f + 0.1f, groundLayer);
          
        }

        private void TryJump()
        {


           
            if (isGrounded && !isAttacking && !isStanding)
            {
                print("kk");
                if (isJumping)
                {
                    isJumping = false;
                }

                //Animation
                animator.SetBool("Jump", true);
                
                Invoke("Jump", 0.3f);
                //Add delay



            }


        }

        private void Jump()
        {

           

            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isJumping = true;

        }

        #region attack

        private void Attack()
        {
            
            if ( canAttack && !isStanding )
            {
                animator.SetTrigger("Attack");
                isAttacking = true;
                canAttack = false;
                StartCoroutine(RefreshAttack());
            }
        }
    
        private IEnumerator RefreshAttack()
        {
            isAttacking = false;
            yield return new WaitForSeconds(1f);
            canAttack = true;
            yield break;
        }

        #endregion




        void ToggleStand()
        {
            isStanding = !isStanding;
            animator.SetBool("Stand", isStanding);
        }

  
   
    }
}
