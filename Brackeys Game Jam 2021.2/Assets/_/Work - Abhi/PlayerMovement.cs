using BGJ20212.Game.Naron;

namespace  BGJ20212.Game.AbhiTechGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Animations.Rigging;

    public class PlayerMovement : MonoBehaviour
    {

        //Refs
        [Header("Refrences")] [SerializeField] private PlayerAnimator animator;

        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerShoot playerShoot;
        public float gravity = -9.81f;

        private float verticalVelocity;
        Vector3 moveDirection;


        public Transform cameraTransform;

        public Transform GroundCheckPos;
        public float radius;
        public LayerMask whatIsGround;

        //Stats
        [Header("Stats")] public float move_Speed = 250f;
        public float speed;
        public float jumpHeight;

  
        private bool isAttacking;
        private bool canAttack;
        private bool isJumping;
        private bool isShooting;
        private bool isStanding;



        //Weapon stuff

        [Header("Weapons")]
        [SerializeField] GameObject weaponHolder;
        [SerializeField] TwoBoneIKConstraint rig;



        void Start()
        {
            //cameraTransform = Camera.main.transform;
            characterController = GetComponent<CharacterController>();

            speed = move_Speed;

            canAttack = true;
            disableWeaponAndRig();
        }

        private void Update()
        {
            CheckInput();
            Move();
        }

    
        #region Movement

        private void Move()
        {
      
            
            Vector2 inputAxis = new Vector2(Input.GetAxis(Naron.Axis.HORIZONTAL), Input.GetAxis(Naron.Axis.VERTICAL));

            moveDirection = new Vector3(inputAxis.x, 0f, inputAxis.y);
            Vector3 direction = moveDirection.normalized;
            moveDirection = transform.TransformDirection(moveDirection);
            //Gravity

            if (moveDirection.magnitude >= 0.1f)
            {
                /*
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
                */
                animator.SetFloat("moveSpeed", 1f);
            }
            else
            {
                animator.SetFloat("moveSpeed", 0f);
            }

       
            ApplyGravity();
            if(verticalVelocity < gravity*2 || isJumping)
            {
                animator.SetFloat("moveSpeed", 0f);
            }
       
            characterController.Move(moveDirection*speed * Time.deltaTime);
        }
        #endregion


        private void CheckInput()
        {
      

            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
            if (Input.GetMouseButton(1))
            {
                TryShoot();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                ToggleStand();
            }
        }
        void TryShoot()
        {
            if (isStanding)
            {

            }
            
            if (!isAttacking && playerShoot.canShoot)
            {
                if (!isStanding)
                {
                    
                    playerShoot.canShoot = false;
                    animator.SetTrigger("Throw", playerShoot.Shoot);
                }
                else
                {
                    playerShoot.Shoot();
                }
                
            }
        }
#region Jump
        void ApplyGravity()
        {
            if (!characterController.isGrounded)
            {
                verticalVelocity += gravity * Time.deltaTime;
                animator.SetBool("Move", false);
            }
            else
            {
                verticalVelocity = -0.1f;
                isJumping = false;
            }

            PlayerJump();
            moveDirection.y = verticalVelocity;

        }


        private void PlayerJump()
        {
            
            if (characterController.isGrounded && Input.GetButton(Naron.Axis.JUMP))
            {
                verticalVelocity = jumpHeight;
                isJumping = true;
            }
            
        }
        #endregion
        #region Attack
        private void Attack()
        {
            
            if (characterController.isGrounded && canAttack && !isShooting && !isStanding)
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




        private void ToggleStand()
        {
            if (!characterController.isGrounded || isAttacking || isShooting) return;
          
                isStanding = !isStanding;
            animator.SetBool("Stand", isStanding);

            if (isStanding)
            {
                Invoke("enableWeapon", 0.5f);
            }
            else
            {
                Invoke("disableWeaponAndRig", 0.5f);
                playerShoot.weaponIndex = 0;
            }

            
        }

        private void disableWeaponAndRig()
        {
            rig.weight = 0;
            weaponHolder.SetActive(false);
            playerShoot.weaponIndex = 1;
        }

        private void enableWeapon()
        {
            
            weaponHolder.SetActive(true);


            rig.weight = 1;
            
            playerShoot.weaponIndex = 1;
        }
    }



    
}
