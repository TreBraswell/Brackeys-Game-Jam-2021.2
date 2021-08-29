using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BGJ20212.Game.Naron
{
    public class Spawner : MonoBehaviour
    {
        enemyOptions[] spawnPoint;
        private int waveNumber = 0;
        private int enemySpawnAmount = 0;
        public int enemyKilled = 0;

       
        private void Start()
        {
        
            StartWave();
        }
        private void Update()
        {
    
        }
        private void SpawnEnemy()
        {
            int randSpawnPlace = Random.Range(0, spawnPoint.Length);
            
          
        }
        private void StartWave()
        {
            waveNumber = 1;
            enemySpawnAmount = 2;
            enemyKilled = 0;
            for (int i = 0; i < enemySpawnAmount; i++)
            {
                SpawnEnemy();
            }
        }
        private void NextWave()
        {
            waveNumber++;
            enemySpawnAmount += 2;
            enemyKilled = 0;
        }
    }
        
        [System.Serializable]
        struct enemyOptions
        {
            [SerializeField] private int spawnPoints;

            [SerializeField] private GameObject enemyPrefab;

        }
    }

