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
        [SerializeField]
        private Collider HitCollider;
        private float verticalVelocity;
        Vector3 moveDirection;

        [SerializeField] private AudioSource gorillaMove;


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
        private bool Extra;


        AudioManager audioManager;

        PlayerDoorOpen playerDoorOpen;

        //Weapon stuff

        [Header("Weapons")]
        [SerializeField] GameObject weaponHolder;
        [SerializeField] TwoBoneIKConstraint rig;



        void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
            cameraTransform = Camera.main.transform;
            characterController = GetComponent<CharacterController>();

            playerDoorOpen = FindObjectOfType<PlayerDoorOpen>();

            speed = move_Speed;

            canAttack = true;
            disableWeaponAndRig();

            Cursor.visible = false;
        }

        private void Update()
        {
            if (!Extra)
            {
                CheckInput();
                Move();
            }
        }

        void DoneExtra()
        {
            Extra = false;
        }

        void ExtraAnimation()
        {

            if (!isStanding && !isAttacking && canAttack && characterController.isGrounded && !isJumping)
            {
                Extra = true;
                System.Action callback = DoneExtra;
                animator.SetTrigger("Extra", callback);
            }

        }



        #region Movement

        private void Move()
        {
            if (Extra) return;
            Vector2 inputAxis = new Vector2(Input.GetAxis(Naron.Axis.HORIZONTAL), Input.GetAxis(Naron.Axis.VERTICAL));

            moveDirection = new Vector3(inputAxis.x, 0f, inputAxis.y);
            Vector3 direction = moveDirection.normalized;
            moveDirection = transform.TransformDirection(moveDirection);
            //Gravity

            if (moveDirection.magnitude >= 0.1f)
            {
                animator.SetFloat("moveSpeed", 1f);
                gorillaMove.volume = 1f;
            }
            else
            {
                animator.SetFloat("moveSpeed", 0f);
                gorillaMove.volume = 0f;
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
                audioManager.Play("Player Attack");
                Invoke("GrowlSound", .2f);
            }
            if (Input.GetMouseButton(1))
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                ToggleStand();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ExtraAnimation();
                audioManager.Play("Player Growl");
            }
        }

        void GrowlSound()
        {
            audioManager.Play("Player Growl 2");
        }
        
        void Shoot()
        {
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
            // print(characterController.isGrounded);
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
                audioManager.Play("Player Jump");
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
                playerDoorOpen.CheckGate();
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
            playerShoot.weaponIndex = 0;
        }

        private void enableWeapon()
        {
            weaponHolder.SetActive(true);
            rig.weight = 1;
            playerShoot.weaponIndex = 1;
        }
    }




}
