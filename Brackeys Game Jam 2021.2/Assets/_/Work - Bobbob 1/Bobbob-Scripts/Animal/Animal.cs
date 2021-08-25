namespace BGJ20212.Game.Bobbob
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : MonoBehaviour
{
    private Animator animator;
    [Header("Stats")] public float move_Speed = 250f;
    public float run_Speed = 400f;
    public float speed;
    public double health;
    public double damage;

    private NavMeshAgent myMesh;
    public bool isFollowing = false;

    private bool isAttacking;
    private bool canAttack;

    private bool isStanding;
    private bool isSpriting;
    
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    public virtual void Start()
    {
        myMesh = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        //myMesh.updatePosition = false;
    }
    public virtual void Update()
    {
        if(isFollowing && Player.instance)
        {
            
            myMesh.SetDestination(Player.instance.gameObject.transform.position);
            AnimationCheck();
        }
    }
    //this will be used for damage
    public virtual void OnCollisionEnter(Collision collision)
    {
        //probaly minus health
        Debug.Log(collision.gameObject.name);
    }
    //this will be used to follow the player after being freed

    public virtual void MoveToPlayer()
    {
        isFollowing = true;
    }
    public virtual void TakeDamage(double damage)
    {
        health -= damage;
    }
    public virtual void Die()
    {
        public double Health;
        public double Attack;

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

        public virtual void DealDamage(double damage)
        {
            Health -= damage;
        }

        public virtual void Die()
        {
            Destroy(this.gameObject);
        }
    }
    public virtual void DealDamage(GameObject enemy)
    {
        health -= damage;
    }
    public virtual void AnimationCheck()
    {
        Debug.Log(myMesh.speed);
        if (myMesh.velocity.x==0 && myMesh.velocity.z == 0 )
        {
            animator.SetBool("Move", false);
            animator.SetFloat("moveSpeed", 0);
        }
        else
        {
            animator.SetBool("Move", true);
            animator.SetFloat("moveSpeed", 1);
        }

        
        

    }
}
