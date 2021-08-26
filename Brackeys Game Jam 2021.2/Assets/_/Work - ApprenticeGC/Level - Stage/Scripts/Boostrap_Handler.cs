namespace BGJ20212.Game.ApprenticeGC.Stage
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Bolt;
    using UnityEngine;

    public partial class Boostrap : MonoBehaviour
    {
        private void HandleBeingKilled()
        {
            CustomEvent.Trigger(this.gameObject, "Player - Dead");
        }
    }
}
