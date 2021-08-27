using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGJ20212.Game.Naron {
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject[] projectiles;


        public int weaponIndex;

        //weapons
        [SerializeField] private Transform[] weaponPlace;
        [SerializeField] private float[] WeaponRefreshTime;
        
        [SerializeField] private float bulletSpeed = 20f;
        [SerializeField] private float heightCalibrate = 40f;
       public bool canShoot;
        Vector3 destination;

        [SerializeField] private AbhiTechGame.PlayerMovement player;

        private void Start()
        {
            canShoot = true;
        }

        private void Update()
        {
       
    }
        public void Shoot()
        {
           
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                destination = hit.point;
            else destination = ray.GetPoint(1000);
            //dontShoot

            destination += transform.forward * heightCalibrate;
            InstantiateBullet();


        }
        void InstantiateBullet()
        {
            //Initiate Bullet
            GameObject bullet = (GameObject)Instantiate(projectiles[weaponIndex], weaponPlace[weaponIndex].position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = (destination - weaponPlace[weaponIndex].position).normalized * bulletSpeed;
            
            canShoot = false;
            StartCoroutine(RefreshAttack());
        }
        private IEnumerator RefreshAttack()
        {
            yield return new WaitForSeconds(WeaponRefreshTime[weaponIndex]);
            canShoot = true;
        }

    }
}