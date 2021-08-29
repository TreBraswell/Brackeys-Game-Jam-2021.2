using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BGJ20212.Game.Naron
{
    public class Spawner : MonoBehaviour
    {[SerializeField]
        enemyOptions[] spawnPoint;
        
       
        private void Start()
        {
        
            StartWave();
        }
        private void Update()
        {
    
        }
     

        IEnumerator Spawn()
        {
            int time = Random.Range(5, 10);
            int randSpawnPlace = Random.Range(0, spawnPoint.Length);
            yield return new WaitForSeconds(time);
            Instantiate(spawnPoint[randSpawnPlace].enemyPrefab, spawnPoint[randSpawnPlace].spawnPoints.position, Quaternion.identity);
            StartWave();
            yield break;

        }
        private void StartWave()
        {
            StopAllCoroutines();
                StartCoroutine(Spawn());
           
        }
   
    }
        
        [System.Serializable]
        struct enemyOptions
        {
             public Transform spawnPoints;

            public GameObject enemyPrefab;

        }
    }

