namespace BGJ20212.Game.ApprenticeGC.Hud
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIIndication :
        MonoBehaviour//,
        //MyPooler.IPooledObject
    {
        public Sprite icon;
        public Image image;

        public Vector2 MappedPos => new Vector2(transform.position.x, transform.position.z);

        // public void OnRequestedFromPool()
        // {
        //     //
        //     icon = null;
        //     image = null;
        // }
        //
        // public void DiscardToPool()
        // {
        //     MyPooler.ObjectPooler.Instance.ReturnToPool("Indication", this.gameObject);
        // }
    }
}
