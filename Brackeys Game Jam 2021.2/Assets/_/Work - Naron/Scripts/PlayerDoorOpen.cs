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

    }

}