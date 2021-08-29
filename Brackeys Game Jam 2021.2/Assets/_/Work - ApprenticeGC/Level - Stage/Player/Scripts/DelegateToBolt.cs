namespace BGJ20212.Game.ApprenticeGC.Player
{
    using System.Collections;
    using System.Collections.Generic;
    using Bolt;
    using UnityEngine;

    public class DelegateToBolt : MonoBehaviour
    {
        public void HandleHpPercentageChange(float percentage)
        {
            CustomEvent.Trigger(gameObject, "Hp Percentage Changed", percentage);
        }

        public void HandleDie()
        {
            CustomEvent.Trigger(gameObject, "Die");
        }
    }
}
