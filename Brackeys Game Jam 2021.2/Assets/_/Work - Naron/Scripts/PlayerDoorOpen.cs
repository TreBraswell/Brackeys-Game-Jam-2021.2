using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGJ20212.Game.Naron
{
    public class PlayerDoorOpen : MonoBehaviour
    {
        [SerializeField] private float rayLength = 5f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private string excluseLayeName = null;
        GameObject raycastObject;
        private void Update()
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            int mask = 1 << LayerMask.NameToLayer(excluseLayeName) | layerMask.value;

            if(Physics.Raycast(transform.position,fwd,out hit, rayLength, mask))
            {
               
                raycastObject = hit.collider.gameObject;
                if (raycastObject.CompareTag("Door"))
                {
                    if(Input.GetKeyDown(KeyCode.T))
                        raycastObject.GetComponentInParent<GateOpen>().toggleGate();
                }
            }
        }
    }

}