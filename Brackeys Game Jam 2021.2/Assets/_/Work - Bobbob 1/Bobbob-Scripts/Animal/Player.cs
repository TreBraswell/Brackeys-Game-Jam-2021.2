using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public GameObject attacker = null;
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
}
