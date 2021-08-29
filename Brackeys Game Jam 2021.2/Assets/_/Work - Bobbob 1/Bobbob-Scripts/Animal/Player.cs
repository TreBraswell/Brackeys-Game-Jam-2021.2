using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public static Player instance;
    public GameObject attacker = null;


    public float health;
    public float maxHealth;


    public UnityEvent<float> hpPercentageChangedEvent;
    public UnityEvent dieEvent;


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
        var percentage = health / maxHealth;
        Debug.Log("sending hp percentage changed event");
        hpPercentageChangedEvent?.Invoke(percentage);
        if(health <= 0) {
            Die();

            //Dying Part
         //   Die();
        }
        print(maxHealth+" " + health);
    }


    void Die()
    {
        // Destroy(this.gameObject);
        //Death action;
        // Will handle destroy in subscriber side
        dieEvent?.Invoke();
    }
}
