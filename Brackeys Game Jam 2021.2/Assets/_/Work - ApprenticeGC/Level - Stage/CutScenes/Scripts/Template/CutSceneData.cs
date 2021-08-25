namespace BGJ20212.Game.ApprenticeGC.Dialogue.Template
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "CutSceneData", menuName = "BGJ20212/Dialogue/Cut Scene Data")]
    public class CutSceneData : ScriptableObject
    {
        public List<GameObject> cutScenePrefabs;
    }
}
