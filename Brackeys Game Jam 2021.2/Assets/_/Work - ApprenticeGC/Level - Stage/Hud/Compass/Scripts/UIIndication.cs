namespace BGJ20212.Game.ApprenticeGC.Hud
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UIIndication : MonoBehaviour
    {
        public Sprite icon;

        public Vector2 MappedPos => new Vector2(transform.position.x, transform.position.z);
    }
}
