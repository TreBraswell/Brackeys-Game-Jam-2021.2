using BGJ20212.Game.Mark;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
public class Animal : MonoBehaviour
{
    enum EnemyState  {Idle,Chase,Attack};
    private EnemyState enemy_State;
    //[HideInInspector]
    public Animator animator;
    [Header("Stats")] public float move_Speed = 250f;
    public float run_Speed = 400f;
    public float speed;
    private double maxHealth;
    [SerializeField] Image healthBar;
    private GameObject healthCanvas;


    public double health;
    public double damage;
    public GameObject follow;
    public bool isEnemy; // kind of got a bit lazy here
    [HideInInspector] public NavMeshAgent agent;


    public bool destrotyRbsLengthZero = true;
    public bool playAnimationUntoggledRbs = false;

    [SerializeField]
    private float knockBackWhenDjes = 30f;

    private bool isAttacking;
    private bool canAttack;

    private bool isStanding;
    private bool isSpriting;

    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    public bool isFollowing;
    [Space(15)]
    [Header("AI")]
    [SerializeField] private float  baseChaseDistance;
    [SerializeField] private float atkDistance;
    float chaseDistance;
    [SerializeField]
    float AtkTimer, WaitBeforeAttack;

    [SerializeField] private BGJ20212.Game.Naron.GateOpen door;


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
        chaseDistance = baseChaseDistance;
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();

        }
        //myMesh.updatePosition = false;
        maxHealth = health;

        enemy_State = EnemyState.Idle;
        if (healthBar != null)
        {
            healthCanvas = healthBar.transform.parent.parent.gameObject;
            healthBar.fillAmount = (float)(health / maxHealth);
            healthCanvas.SetActive(false);
        }
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        gun = GetComponent<Gun>();
       ToggleRagdoll(false);
        if (door != null)
        {
            
        }
    }

   public virtual void Update()
   {
        if(!isDead)
        Check_States();
      
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

        agent.enabled = false;
       ToggleRagdoll(true);
   }
   void ToggleRagdoll(bool enable)
   {
       if(rbs.Length == 0)
       {
           if (destrotyRbsLengthZero)
           {
               //Destroy(this.gameObject);
           }
       }
       if (!enable)
       {
           for (int i = 0; i < rbs.Length; i++)
           {
               rbs[i].isKinematic = true;

               colliders[i].enabled = false;
           }

           collider.enabled = true;


           if (animator != null)
           {
               animator.enabled = true;
           }
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
           if (animator != null)
           {
               if (!playAnimationUntoggledRbs)
               {
                   animator.enabled = false;
               }
           }
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
        // Debug.Log(this.gameObject);

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



   public UnityEvent getKilled;

   public virtual void TakeDamage(double damage )
   {

       health -= damage;
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Clamp01((float)(health / maxHealth));
            if (healthBar.fillAmount != 0 && !healthCanvas.activeSelf) healthCanvas.SetActive(true);

            if(healthBar.fillAmount == 0)
            {
                healthCanvas.SetActive(false);
            }

        }

        chaseDistance = 10000f;

       if (health <= 0)
       {
            isDead = true;
            Die();
          getKilled?.Invoke();
       }

   }

    void Check_States()
    {
        if (enemy_State == EnemyState.Idle)
        {
            Idle();
        }
        if (enemy_State == EnemyState.Chase)
        {
            Chase();
        }
        if (enemy_State == EnemyState.Attack)
        {
            Attack();
        }
    }
    protected virtual void Idle()
    {
        //  if(agent.isStopped)
        // agent.isStopped = false;
        //  agent.speed = speed;


        //Notice player
        if (Vector3.Distance(transform.position, player.transform.position) <= chaseDistance )


        {
            if(door == null)
            enemy_State = EnemyState.Chase;
            else if(door.Open) enemy_State = EnemyState.Chase;
            //Notice sound
        }



    }
    protected virtual void Chase()
    {
       
        agent.isStopped = false;
        agent.speed = run_Speed;

        //move to player
        agent.SetDestination(player.transform.position);

        //walk if moving
        if (agent.velocity.sqrMagnitude > 0) ChaseAnim();
        else StopAnims();


        //Attack Distance
        if (Vector3.Distance(transform.position, player.transform.position) <= atkDistance)
        {
            StopAnims();
            enemy_State = EnemyState.Attack;
            AtkTimer = WaitBeforeAttack;
            //reset chase distance
            if (chaseDistance != baseChaseDistance) chaseDistance = baseChaseDistance;

        }
        //if Gone out of range
        else if (Vector3.Distance(transform.position, player.transform.position) >= 2 * chaseDistance)
        {
            StopAnims();//if runs stop
            enemy_State = EnemyState.Idle;
            //reset timer
            if (chaseDistance != baseChaseDistance) chaseDistance = baseChaseDistance;
        }

    }

    void Attack()
    {
   
        agent.velocity = Vector3.zero;// stop for hit
        agent.isStopped = true;
        AtkTimer += Time.deltaTime;
        //look at player
        LookToPlayer();
        if (AtkTimer > WaitBeforeAttack)
        {
            Hit();
            AtkTimer = 0;

            //play Sound


        }
        if (Vector3.Distance(transform.position, player.transform.position) >= atkDistance)
        {
            enemy_State = EnemyState.Chase;
        }
    }

    //set new Random patrol destination


    void LookToPlayer()
    {

        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1);

    }
    protected virtual void StopAnims()
    {
        animator.SetFloat("moveSpeed",0);
    }
    protected virtual void ChaseAnim()
    {
        animator.SetFloat("moveSpeed", 1);
    }


    protected virtual void Hit()
    {

        animator.SetTrigger("Attack");

        //HitPlayer


    }



}
