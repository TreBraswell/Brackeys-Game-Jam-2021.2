namespace BGJ20212.Game.ApprenticeGC.Stage
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class Boostrap : MonoBehaviour
    {
        // public GameObject managerPrefab;
        //
        // private GameObject _manager;

        private void Start()
        {
            // _manager = GameObject.Instantiate(managerPrefab);
        }

        public void NotifyBeingKilled()
        {
            HandleBeingKilled();
        }
    }
}

