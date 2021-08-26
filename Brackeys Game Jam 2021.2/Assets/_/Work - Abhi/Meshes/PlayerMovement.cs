using BGJ20212.Game.Naron;

namespace AbhiTechGames
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

        private Vector3 direction;

        private float turnSmoothTime = 0.2f;
        private float turnSmoothVelocity;

        Vector3 moveDirection;

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
                velocity.y = -2f;
            }

            Vector2 inputAxis = new Vector2(Input.GetAxis(Axis.HORIZONTAL), Mathf.Clamp(Input.GetAxis(Axis.VERTICAL), 0f, 1f));

            direction = new Vector3(inputAxis.x, 0f, inputAxis.y).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }
            else
            {
                characterController.Move(new Vector3(moveDirection.x * speed, 0f, moveDirection.z * speed));
                animator.SetFloat("moveSpeed", 40f);
            }
        }
        private void CheckInput()
        {
            if (Input.GetButtonDown(Axis.JUMP))
            {
                Jumping();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

        void Jumping()
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(GroundCheckPos.position, radius, whatIsGround);
        }
        private void Attack()
        {
            print("here");
            if (isGrounded && canAttack)
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
    }
}