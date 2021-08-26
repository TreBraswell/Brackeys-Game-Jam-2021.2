using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGJ20212.Game.Naron {
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform weaponPlace;
        [SerializeField] private float WeaponRefreshTime;
        bool canShoot;


        [SerializeField] private AbhiTechGame.PlayerMovement player;

        private void Start()
        {
            canShoot = true;
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                Shoot();
            }
    }
        void Shoot()
        {
            //dontShoot
            if (!canShoot) return;

            //Initiate Bullet
            GameObject bullet = (GameObject)Instantiate(projectile, weaponPlace.position, weaponPlace.rotation);
            canShoot = false;
            StartCoroutine(RefreshAttack());
            

        }

        private IEnumerator RefreshAttack()
        {
            yield return new WaitForSeconds(WeaponRefreshTime);
            canShoot = true;
        }

    }
}