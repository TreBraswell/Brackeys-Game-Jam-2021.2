using UnityEngine;

namespace AbhiTechGames
{    
    public class ThirdPersonController : MonoBehaviour
    {
        public float speed = 10f;

        public CharacterController characterController;
        float turnSmoothVelocity;
        public float turnSmoothTime = .1f;

        float horizontal;
        float vertical;

        // Update is called once per frame
        void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
            if(dir.magnitude >= 0.1f)
            {
                float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                float targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                characterController.Move(dir * speed * Time.deltaTime);
            }

        }
    }
}

