using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Start()
    {
       
        StartCoroutine(disapear());
        
    }
    
   IEnumerator disapear()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shootable"))
        {
            //Damage Part
            Destroy(other.transform.gameObject);
            Destroy(this.gameObject);

        }
    }
}
