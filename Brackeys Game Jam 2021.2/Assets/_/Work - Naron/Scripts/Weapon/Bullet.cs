using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * 20f;
        StartCoroutine(disapear());
        
    }
    
   IEnumerator disapear()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shootable"))
        {
            //Damage Part

            Destroy(this.gameObject);

        }
    }
}
