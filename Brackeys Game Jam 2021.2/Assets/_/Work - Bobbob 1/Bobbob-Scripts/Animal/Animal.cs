using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public double Health;
    
    
    //this will be used for damage
    public virtual void OnCollisionEnter(Collision collision)
    {
        //probaly minus health
        Debug.Log(collision.gameObject.name);
    }
    //this will be used to follow the player after being freed
    public virtual void MoveToPlayer()
    {

    }
}
