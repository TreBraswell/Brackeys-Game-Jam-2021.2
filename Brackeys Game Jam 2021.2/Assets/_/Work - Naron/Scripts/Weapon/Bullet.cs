using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

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
        if (other.transform.CompareTag("Shootable"))
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GetComponent<Collider>().isTrigger = false;
        }
        }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Shootable"))
        {
            //Damage Part
            Animal anim = other.transform.GetComponent<Animal>() ;
          
            anim.GetHit(damage,Player.instance.attacker,transform.position);
            Destroy(this.gameObject);

        }
    }

}
