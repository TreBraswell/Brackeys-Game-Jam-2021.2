namespace BGJ20212.Game.ApprenticeGC.Creature.Template
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "CreatureData", menuName = "BGJ20212/Creature/CreatureData")]
    public class CreatureData : ScriptableObject
    {
        public List<GameObject> creaturePrefabs;
    }
}
