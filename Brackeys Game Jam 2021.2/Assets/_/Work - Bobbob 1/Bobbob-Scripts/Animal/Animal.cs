
using BGJ20212.Game.Mark;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [Header("Stats")] public float move_Speed = 250f;
    public float run_Speed = 400f;
    public float speed;
    public double health;
    public double damage;
    public GameObject follow;
    public bool isEnemy; // kind of got a bit lazy here
    [HideInInspector] public NavMeshAgent myMesh;
    public bool isFollowing = false;

    [SerializeField]
    private float knockBackWhenDjes = 30f;

    private bool isAttacking;
    private bool canAttack;

    private bool isStanding;
    private bool isSpriting;
    
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;


    bool isDead;
    GameObject player;

    [SerializeField] Rigidbody[] rbs;
    [SerializeField] Collider[] colliders;
    private Collider collider;
    private Rigidbody rb;
    Vector3 LastBulletPos;
    Gun gun;
   
    public virtual void Start()
    {
        myMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //myMesh.updatePosition = false;

        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        gun = GetComponent<Gun>();
      ToggleRagdoll(false);
    }
  
   public virtual void Update()
   {
       if(isFollowing && follow == null)
       {
           follow = Player.instance.gameObject;
       }
       if(health<=0 && !isDead)
       {
           Die();
           isDead = true;
       }
       if(isFollowing && Player.instance)
       {

           myMesh.SetDestination(follow.transform.position);
           AnimationCheck();
       }
       if(gun && gun.CheckForEnemy(isEnemy))
       {
           animator.SetTrigger("Attack");
           StartCoroutine(RefreshAttack());
           gun.Shoot();

       }
       if(follow == Player.instance.gameObject && Player.instance.attacker)
       {
           follow = Player.instance.attacker;
       }

   }
   private IEnumerator RefreshAttack()
   {
       isAttacking = false;
       yield return new WaitForSeconds(1f);
       canAttack = true;
       yield break;
   }
   public virtual void Die()
   {
       if(this.gameObject == Player.instance.attacker)
       {
           Player.instance.attacker = null;
       }
       ToggleRagdoll(true);
        Destroy(this.gameObject, 5);
   }
   void ToggleRagdoll(bool enable)
   {
       if(rbs.Length == 0)
       {
           Destroy(this.gameObject);
       }
       if (!enable)
       {
           for (int i = 0; i < rbs.Length; i++)
           {
               rbs[i].isKinematic = true;

               colliders[i].enabled = false;
           }

           collider.enabled = true;
           animator.enabled = true;

       }
       else
       {
           for (int i = 0; i < rbs.Length; i++)
           {
               rbs[i].isKinematic = false;
               if(LastBulletPos!= Vector3.zero)
               {
                   rbs[i].AddForce((transform.position - LastBulletPos).normalized * knockBackWhenDjes, ForceMode.Impulse);
               }
               colliders[i].enabled = true;
           }

           collider.enabled = false;
           animator.enabled = false;
       }


   }

 


   //this will be used for damage
   /*public virtual void OnCollisionEnter(Collision collision)
   {
       //probaly minus health
       Debug.Log("this happened");
   }*/
   
    public virtual void GetHit(double damage, GameObject attacker, Vector3 pos = new Vector3())
    {
        follow = attacker;
        
        if (this.gameObject.GetComponent<Player>())
        {
            Player.instance.attacker = attacker;
        }
    
        if(pos != new Vector3())
        {
            
            LastBulletPos =new Vector3(pos.x,pos.y,pos.z);
          
        }
        else
        {
            LastBulletPos = Vector3.zero;
        }
       TakeDamage(damage);
    }
 
   //this will be used to follow the player after being freed

   public virtual void MoveToPlayer()
   {
       isFollowing = true;
   }
   public virtual void TakeDamage(double damage )
   {

       health -= damage;

   }



       //this will be used to follow the player after being freed



   public virtual void DealDamage(GameObject enemy)
   {
       health -= damage;
   }
   public virtual void AnimationCheck()
   {

       /*if (myMesh.velocity.x==0 && myMesh.velocity.z == 0 )
       {
           animator.SetBool("Move", false);
           animator.SetFloat("moveSpeed", 0);
       }
       else
       {
           animator.SetBool("Move", true);
           animator.SetFloat("moveSpeed", 1);
       }*/




     }

}
