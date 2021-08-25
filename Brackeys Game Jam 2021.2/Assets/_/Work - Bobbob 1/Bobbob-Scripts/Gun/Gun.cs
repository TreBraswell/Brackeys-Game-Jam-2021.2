namespace BGJ20212.Game.Bobbob
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : MonoBehaviour
    {
        public GameObject projectile;
        public double fireRate;
        public double projectileVelocity;
        private double nextFire = 0.0;

        public virtual void Fire()
        {
            Debug.Log("bang");
        }


        // Update is called once per frame
        public virtual void Update()
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Fire();
            }
        }

    }
}
