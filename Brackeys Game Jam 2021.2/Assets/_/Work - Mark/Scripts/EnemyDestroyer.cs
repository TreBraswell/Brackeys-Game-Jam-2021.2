namespace BGJ20212.Game.Mark
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine; //using unity engine methods

    public class EnemyDestroyer : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
