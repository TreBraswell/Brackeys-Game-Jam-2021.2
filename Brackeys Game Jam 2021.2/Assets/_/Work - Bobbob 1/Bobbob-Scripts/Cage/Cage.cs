using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Cage : MonoBehaviour
{
    public GameObject animalHolder;
    public double health;
    public GameObject spawner;
    public  void Start()
    {

    }
    public void Update()
    {
        if(health<=0)
        {
            Die();
        }
    }
    public virtual void GetHit(double damage, GameObject attacker)
    {

        Debug.Log("this is the cage getting hit");
        if (this.gameObject.GetComponent<Player>())
        {
            Player.instance.attacker = attacker;
        }
        health -= damage;
    }
    public void Die()
    {
        foreach (Transform child in animalHolder.transform)
        {
            if(child.gameObject.GetComponent<Animal>())
            {
                //child.gameObject.GetComponent<Animal>().MoveToPlayer();
                child.SetParent(animalHolder.transform.parent);
                
            }
        }
        Destroy(this.gameObject);
        Destroy(animalHolder.transform.gameObject);
        spawner.SetActive(true);
    }
}
