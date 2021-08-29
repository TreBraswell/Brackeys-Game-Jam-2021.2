using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Cage : MonoBehaviour
{
    public GameObject animalHolder;
    public double health;
    public GameObject spawner;

    private GameObject _rank1Manager;
    public  void Start()
    {
        _rank1Manager = GameObject.FindGameObjectWithTag("Rank1Manager");
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

        if (this.gameObject.GetComponent<Player>())
        {
            Player.instance.attacker = attacker;
        }
        health -= damage;
    }

    public UnityEvent gateOpened;

    public void Die()
    {
        Debug.Log("Cage die");

        if (_rank1Manager != null)
        {
            CustomEvent.Trigger(_rank1Manager, "Cage Opened");
        }
        gateOpened?.Invoke();

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
