using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGJ20212.Game.AbhiTechGame;
public class Bullet : MonoBehaviour
{
    public float damage;
    private AudioManager audioManager;
    public GameObject sphereBulletDestroyEffect;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Bullet Throw");
        StartCoroutine(disapear());
    }
    
   IEnumerator disapear()
    {
        yield return new WaitForSeconds(3f);
        audioManager.Play("Bullet Break");
        Instantiate(sphereBulletDestroyEffect, transform.position, transform.rotation);
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
            DisapearNow();

        }
    }
    private void  DisapearNow()
    {

        audioManager.Play("Bullet Break");
        Instantiate(sphereBulletDestroyEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
       
    }
}
