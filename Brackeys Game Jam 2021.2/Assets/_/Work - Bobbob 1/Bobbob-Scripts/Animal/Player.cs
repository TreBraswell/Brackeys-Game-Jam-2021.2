using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public static Player instance;
    public GameObject attacker = null;


    public float health;
    public float maxHealth;


    UnityEvent DieEvent;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        health = maxHealth;

    
    }


    public void GetHit(float dmg)
    {
        health -= dmg;
        if(health <= 0) {

            //Dying Part
         //   Die();
        
        }
        print(maxHealth+" " + health);
    }


    void Die()
    {
        Destroy(this.gameObject);
        //Death action;
        DieEvent?.Invoke();
    }
}
