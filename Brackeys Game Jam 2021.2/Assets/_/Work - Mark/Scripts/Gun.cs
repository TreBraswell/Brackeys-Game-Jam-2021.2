namespace BGJ20212.Game.Mark
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Gun : MonoBehaviour
    {
        public float damage = 10f; // Weapon Damage
        public float range = 10000f; //weapon Range
        public Color ParticleOB;

        public ParticleSystem muz;
        //public GameObject impact;

        private float minHealth = 0f; //minimum of life till an enemye dies
        public float health = 100f; //Enemyhealth
        public float hitIntensity = 500f; //how hard the enemy rigidbody gets hit
        public GameObject fCam; //direction of bullets
         void Start()
        {
            fCam = this.gameObject;

        }
        //var clone : GameObject;
        void Update()
        {
            /* if (Input.GetButtonDown("Fire1")) //left mouse to shoot
             {
                 Shoot(); //line 20
             }
            */

            if (Input.GetMouseButton(0)) //left mouse to shoot
            {
                ParticleOB = new Color(1, 1, 1, 1);
                Shoot(); //line 20
            }
            else
            {
                //muz.Pause();
                //ParticleOB = new Color(1, 1, 1, 0);
            }

        }
        public GameObject CheckForEnemy(bool enemy)
        {
            RaycastHit hit; //marks the target if hit
            if (Physics.Raycast(fCam.transform.position, fCam.transform.forward, out hit, range))
            {
                Rigidbody body = hit.collider.GetComponent<Rigidbody>();
                if (body != null &&(( body.gameObject.GetComponent<Animal>() && body.gameObject.GetComponent<Animal>().isEnemy == !enemy)||(body.gameObject.GetComponent <Cage>())))
                {
                    return body.gameObject;
                }
            }
            return null;
        }
        public void Shoot() //shooting and bullet behaviour
        {
            //muz.Play(); // for Shoot animation
            RaycastHit hit; //marks the target if hit
            if (Physics.Raycast(fCam.transform.position, fCam.transform.forward, out hit, range))
            {
               
                Rigidbody body = hit.collider.GetComponent<Rigidbody>();
                if (body != null)
                {
                    
                    body.AddExplosionForce(hitIntensity, fCam.transform.forward,
                        5); //Raycast hit explosion to add hit force to enemy
                    if(body.gameObject.GetComponent<Animal>())
                    {
                        body.gameObject.GetComponent<Animal>().GetHit(damage,this.gameObject);
                    }
                    else if(body.gameObject.GetComponent<Cage>())
                    {
                        body.gameObject.GetComponent<Cage>().GetHit(damage, this.gameObject);
                    }
                }
            }
            //Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal)); //for object hit animation !!!
        }

    }
/* Only requiered for rockets etc.
 *
 *  public float speed = 1000f; //Projectile speed
 *  public Rigidbody projectile;
 clone = Instantiate(muz, transform.position, transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.forward * speed);
 */


}
