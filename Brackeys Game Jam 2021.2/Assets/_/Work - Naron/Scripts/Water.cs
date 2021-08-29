using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject Shark;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (Player.instance && collision.gameObject == Player.instance.gameObject)
        {
            Shark.GetComponent<Animal>().isFollowing = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (Player.instance && collision.gameObject == Player.instance.gameObject)
        {
            Shark.GetComponent<Animal>().isFollowing = false;
        }
    }
}