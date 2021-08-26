namespace BGJ20212.Game.Mark
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Weapon : MonoBehaviour
    {
        private ParticleSystem ps;

        void Start()
        {
            ps = GetComponent<ParticleSystem>();
            ps.Stop();

            var main = ps.main;
            main.loop = false;
            main.duration = 1.0f;
            main.stopAction = ParticleSystemStopAction.Destroy;

            ps.Play();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
