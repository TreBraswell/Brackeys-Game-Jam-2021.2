namespace BGJ20212.Game.Naron
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerMove : MonoBehaviour
    {
        //Refs
        [Header("Refrences")] [SerializeField] private PlayerAnimator animator;

        [SerializeField] private CharacterController characterController;

        public float gravity = -9.81f;
        Vector3 velocity;

        private Transform cameraTransform;

        public Transform GroundCheckPos;
        public float radius;
        public LayerMask whatIsGround;

        //Stats
        [Header("Stats")] public float move_Speed = 250f;
        public float speed;
        public float jumpHeight;

        private bool isGrounded;
        private bool isAttacking;
        private bool canAttack;

        private bool isStanding;

        //private stuff for physisc
        private Vector3 direction;

        private float turnSmoothTime = 0.2f;
        private float turnSmoothVelocity;

        // Start is called before the first frame update
        void Start()
        {
            cameraTransform = Camera.main.transform;
            characterController = GetComponent<CharacterController>();

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
            Move();
            GroundCheck();
            if(isGrounded && Input.GetButtonDown(Axis.JUMP))
            {
                Jumping();
            }
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
                velocity.y = -2f;
            }

            Vector2 inputAxis = new Vector2(Input.GetAxis(Axis.HORIZONTAL), Mathf.Clamp(Input.GetAxis(Axis.VERTICAL), 0f, 1f));

            direction = new Vector3(inputAxis.x, 0f, inputAxis.y).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(new Vector3(moveDirection.x * speed, 0f, moveDirection.z * speed));
                animator.SetFloat("moveSpeed", 40f);

            }

            else
            {
                animator.SetFloat("moveSpeed", 0f);
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

        #endregion

        private void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

        void Jumping()
        {
            animator.SetBool("Jump", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(GroundCheckPos.position, radius, whatIsGround);
        }

        #region attack

        private void Attack()
        {
            print("here");
            if (isGrounded && canAttack && !isStanding)
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
