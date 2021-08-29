using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject Shark;
    public Animal animal;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            animal.isFollowing = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            animal.isFollowing = false;
        }
    }
}