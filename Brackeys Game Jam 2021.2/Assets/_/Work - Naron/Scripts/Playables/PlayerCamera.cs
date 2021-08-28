
namespace BGJ20212.Game.Naron
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [RequireComponent(typeof(PlayerInput))]
    public class PlayerCamera : MonoBehaviour
    {
       

        
        public float sensitivity = 300f;
        public bool invert;


        private Vector2 current_Mouse_look, look_Angles;
       
      Vector2 _move;
      Vector2 _look;
        float _Aim;
        [SerializeField]
        private Vector3 nextPosition;
        private Quaternion nextRotation;
  [SerializeField]
        private float rotationPower = 3f;
        [SerializeField]
        private float rotationLerp = 0.5f;
        [SerializeField]
        private GameObject followTransform;
        [SerializeField]
        private float speed = 1f;

        private Camera cam;

        CharacterController controller;
        //New input system stuff IDK
        public void OnLook(InputValue value)
        {
            _look = value.Get<Vector2>();
            controller= GetComponent<CharacterController>();
        }


        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
          
        }
        public void OnAim(InputValue value)
        {
            _Aim = value.Get<float>();
        }



        // Update is called once per frame
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if(cam == null)
            {
                cam = Camera.main;
            }
        }


        // Update is called once per frame
        void Update()
        {
            LockAndUnluckCursor();

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                LookAround();

            }
        }




        //Cursor visible/hidden
        void LockAndUnluckCursor()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

        } //Cursor lock

        void LookAround()
        {
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.transform.localEulerAngles.x;

            //Clamp the Up/Down rotation
            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }


            followTransform.transform.localEulerAngles = angles;
            if (_move.x == 0 && _move.y == 0)
            {
                nextPosition = transform.position;
                if (Input.GetMouseButton(1))
                {
                    //Set the player rotation based on the look transform
                    transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                    //reset the y rotation of the look transform
                    followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                }
                return;
            }
            float moveSpeed = speed / 100f;
            Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
            nextPosition = transform.position + position;


           // if(controller.velocity.sqrMagnitude > 0.1)
            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
            //reset the y rotation of the look transform
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        } //loo
        }
}
