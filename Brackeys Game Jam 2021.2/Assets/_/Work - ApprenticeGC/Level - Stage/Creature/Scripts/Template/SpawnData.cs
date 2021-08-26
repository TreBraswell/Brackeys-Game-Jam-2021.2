namespace BGJ20212.Game.ApprenticeGC.Dialogue.Template
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class SpawnLocation
    {
        public Vector3 position;
        public GameObject creaturePrefab;
    }

    [CreateAssetMenu(fileName = "SpawnData", menuName = "BGJ20212/Creature/SpawnData")]
    public class SpawnData : ScriptableObject
    {
        public List<SpawnLocation> spawnLocations;
    }
}
