using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BGJ20212.Game.Naron
{
    public class GateOpen : MonoBehaviour
    {

        [SerializeField]
        private float openTime;

        private Quaternion startPos;
        [SerializeField]
        private float openAmount = 140f;
        Vector3 endPos;

        float timeLerping = 0;
        float lerpDuration = 0.5f;
        bool open, close;
        bool isopened;

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.rotation;


        }


        public void toggleGate()
        {
            if (isopened)
            {
                Close();

            }
            else
            {
                Open();

            }
        }
        private void Open()
        {
            StopAllCoroutines();
            open = true;
            close = false;
            timeLerping = 0;
            openAmount = Mathf.Abs(openAmount);
            endPos = new Vector3(0, openAmount, 0);
            StartCoroutine(RotateDoor());
            isopened = true;
        }
        private void Close()
        {
            StopAllCoroutines();
            open = false;
            close = true;
            timeLerping = 0f;
            openAmount = Mathf.Abs(openAmount) * -1;
            endPos = startPos.eulerAngles;
            StartCoroutine(RotateDoor());
            isopened = false;
        }

        IEnumerator RotateDoor()
        {

            float timeElapsed = 0;
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, openAmount, 0);

            while (timeElapsed < lerpDuration)
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = targetRotation;

        }

        private void OnTriggerEnter(Collider collision)
        {
           
            if (collision.transform.CompareTag("Player") && Input.GetKeyDown(KeyCode.T ))
            {
                
                toggleGate();
            }
        }

    }
    



}