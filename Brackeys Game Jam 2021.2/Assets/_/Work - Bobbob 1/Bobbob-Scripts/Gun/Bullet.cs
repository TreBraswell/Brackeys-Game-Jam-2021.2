using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public virtual void OnCollisionEnter(Collision collision)
    {  if(this && this.gameObject)
        {
            Destroy(this.gameObject);
        }
        
        Debug.Log("test");
    }
}
