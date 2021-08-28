using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float secondsBetweenSpawn;
    private float elapsedTime = 0.0f;
    public GameObject toCreate;
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn)
        {
            elapsedTime = 0;
            

            
            Instantiate(toCreate);
            
        }
    }

}
