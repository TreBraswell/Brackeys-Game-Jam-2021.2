using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override void Fire()
    {
        Rigidbody instantiatedProjectile = Instantiate(projectile.GetComponent<Rigidbody>(), transform.position,transform.rotation)as Rigidbody;

        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, 10));

        Debug.Log("bang bang");
    }

}
