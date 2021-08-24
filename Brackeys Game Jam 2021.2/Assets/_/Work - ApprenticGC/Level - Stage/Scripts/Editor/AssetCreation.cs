namespace BGJ20212.Game.ApprenticeGC.Dialogue
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Bolt;
    using UnityEditor;
    using UnityEngine;

    public partial class AssetCreation
    {
        [MenuItem("Assets/BGJ20212/Dialogue/Create")]
        public static void Organize()
        {
            // Create timeline for displaying dialogue kind of cut scene.
            // Pre setup

            // Post setup
        }

        private static IEnumerable<GameObject> CreateScenarios()
        {
            var workPath = Path.Combine("_", "Work - ApprenticeGC");
            var cutSceneBasePath = Path.Combine(workPath, $"_Generated - CutScenes");
            var relativeCutSceneBasePath = Path.Combine("Assets", cutSceneBasePath);
            var absoluteCutSceneBasePath = Path.Combine(Application.dataPath, cutSceneBasePath);
            var absoluteCutSceneBasePathExisted = Directory.Exists(absoluteCutSceneBasePath);

            if (absoluteCutSceneBasePathExisted)
            {
                FileUtil.DeleteFileOrDirectory(relativeCutSceneBasePath);
            }

            Directory.CreateDirectory(absoluteCutSceneBasePath);

            //
            var baseAssetPath = Path.Combine("_", "Work - ApprenticeGC", "Level - Stage", "CutScenes");

            var dataAssetPath = Path.Combine("Assets", baseAssetPath, "Data Assets");

            var flowMachineAssetPath = Path.Combine(dataAssetPath, "ScG - CutScene.asset");
            var flowMachineAsset = AssetDatabase.LoadAssetAtPath<FlowMacro>(flowMachineAssetPath);

            var jsonDataDirectory = Path.Combine("_", "Work - ApprenticeGC", "Preprocessed Assets", "design", "inky",
                "_Generated");

            var scenarios = GetCutScenes(jsonDataDirectory);
            var scenarioGOs = new List<GameObject>();
            scenarios.ToList().ForEach(scenario =>
            {
                var specificScenarioBasePath = Path.Combine(relativeCutSceneBasePath, $"{scenario.Title}");
                var absoluteSpecificScenarioBasePath = Path.Combine(absoluteCutSceneBasePath, $"{scenario.Title}");

                Directory.CreateDirectory(absoluteSpecificScenarioBasePath);


                // Create scenario
                var scenarioGO = new GameObject(scenario.Title);
                var variablesComp = scenarioGO.AddComponent<Variables>();
                var fmComp = scenarioGO.AddComponent<FlowMachine>();

                scenarioGOs.Add(scenarioGO);

                variablesComp.declarations.Set("title", scenario.Title);

                variablesComp.declarations.Set("currentWaveIndex", 0);


                fmComp.nest.SwitchToMacro(flowMachineAsset);

                // //
                // var waves = CreateWaves(
                //     specificScenarioBasePath,
                //     absoluteSpecificScenarioBasePath,
                //     scenario, scenarioGO);
                //
                // variablesComp.declarations.Set("waves", waves.ToList());
            });

            return scenarioGOs;
        }
    }
}
