using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Cage : Animal
{
    public override void Start()
    {
        myMesh = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        isEnemy = true;
    }
    public override void Die()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.GetComponent<Animal>())
            {
                child.gameObject.GetComponent<Animal>().MoveToPlayer();
                child.SetParent(this.transform.parent);
                
            }
        }
        Destroy(this.gameObject);
    }
}
