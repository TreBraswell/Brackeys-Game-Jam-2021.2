using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Animal
{
    // Start is called before the first frame update
    public  override void Start()
    {
        myMesh = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        isEnemy = true;
    }
}
