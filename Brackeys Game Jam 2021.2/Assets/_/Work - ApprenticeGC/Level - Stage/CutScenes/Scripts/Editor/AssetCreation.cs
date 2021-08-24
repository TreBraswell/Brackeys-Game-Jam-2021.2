namespace BGJ20212.Game.ApprenticeGC.Dialogue.EditorPart
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Bolt;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    public static class Utility
    {
        public static void RecreateDirectory(string directoryPath)
        {
            var relativeDirectoryPath = Path.Combine("Assets", directoryPath);
            var absoluteDirectoryPath = Path.Combine(Application.dataPath, directoryPath);
            var absoluteDirectoryPathExisted = Directory.Exists(absoluteDirectoryPath);

            if (absoluteDirectoryPathExisted)
            {
                FileUtil.DeleteFileOrDirectory(relativeDirectoryPath);
            }

            Directory.CreateDirectory(absoluteDirectoryPath);
        }
    }

    public partial class AssetCreation
    {
        [MenuItem("Assets/BGJ20212/Dialogue/Create")]
        public static void Organize()
        {
            // Create timeline for displaying dialogue kind of cut scene.
            // Pre setup

            var cutScenes = CreateCutScenes();
            // Post setup
            cutScenes.ToList().ForEach(x => GameObject.DestroyImmediate(x));
        }

        private static IEnumerable<GameObject> CreateCutScenes()
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

            var jsonDataDirectory = Path.Combine("Assets", "_", "Work - ApprenticeGC", "Preprocessed Assets", "design", "inky",
                "_Generated");

            var cutScenes = GetCutScenes(jsonDataDirectory);
            var cutSceneGOs = new List<GameObject>();
            cutScenes.ToList().ForEach(cutScene =>
            {
                var stickyRoot = cutScene?.Root[0].AnythingArray[0];
                var cutSceneName = stickyRoot.Value.String.Replace("^", "");
                // Debug.Log($"cutscene sticky {stickyRoot.Value.String}");

                //
                Utility.RecreateDirectory(Path.Combine(absoluteCutSceneBasePath, cutSceneName));
                var generatedCutSceneBasePath = Path.Combine(relativeCutSceneBasePath, cutSceneName);

                //
                var cutSceneGO = CreateCutSceneGO(
                    cutSceneName,
                    baseAssetPath,
                    generatedCutSceneBasePath,
                    flowMachineAsset,
                    cutScene);
                cutSceneGOs.Add(cutSceneGO);

                var prefabAssetPath = Path.Combine(generatedCutSceneBasePath, $"{cutSceneName}.prefab");
                PrefabUtility.SaveAsPrefabAsset(cutSceneGO, prefabAssetPath);
            });

            AssetDatabase.Refresh();

            return cutSceneGOs;
        }

        private static GameObject CreateCutSceneGO(
            string cutSceneName,
            string baseAssetPath,
            string creatureBasePath,
            FlowMacro flowMachineAsset,
            Generated.CutScene cutScene)
        {
            // var specificScenarioBasePath = Path.Combine(relativeCutSceneBasePath, $"{cutScene.Title}");
            // var absoluteSpecificScenarioBasePath = Path.Combine(absoluteCutSceneBasePath, $"{cutScene.Title}");
            //
            // Directory.CreateDirectory(absoluteSpecificScenarioBasePath);


            // Create scenario
            // var scenarioGO = new GameObject(cutScene.Title);
            var cutSceneGO = new GameObject(cutSceneName);
            var variablesComp = cutSceneGO.AddComponent<Variables>();
            var fmComp = cutSceneGO.AddComponent<FlowMachine>();

            // variablesComp.declarations.Set("title", cutScene.Title);
            //
            // variablesComp.declarations.Set("currentWaveIndex", 0);


            fmComp.nest.SwitchToMacro(flowMachineAsset);

            var canvasPrefabAssetPath = Path.Combine("Assets", baseAssetPath, "Hud", "Prefabs", "Canvas.prefab");
            var dialogueHudPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(canvasPrefabAssetPath);

            var dialogueGO =  CreateDialogueGO(
                cutSceneName,
                baseAssetPath,
                creatureBasePath,
                cutSceneGO,
                dialogueHudPrefab,
                cutScene);

            variablesComp.declarations.Set("dialogueGO", dialogueGO);


            // //
            // var waves = CreateWaves(
            //     specificScenarioBasePath,
            //     absoluteSpecificScenarioBasePath,
            //     scenario, scenarioGO);
            //
            // variablesComp.declarations.Set("waves", waves.ToList());

            return cutSceneGO;
        }

        private static GameObject CreateDialogueGO(
            string cutSceneName,
            string baseAssetPath,
            string creatureBasePath,
            GameObject cutSceneGO,
            GameObject dialogueHudPrefab,
            Generated.CutScene cutScene)
        {
            var waveGO = new GameObject("Dialogue");
            waveGO.transform.SetParent(cutSceneGO.transform);

            var pdComp = waveGO.AddComponent<PlayableDirector>();

            var timelineAsset = ScriptableObject.CreateInstance<TimelineAsset>();
            // Debug.Log(timelineAsset);

            var dataAssetPath = Path.Combine("Assets", baseAssetPath, "Data Assets");

            var flowMachineAssetPath = Path.Combine(dataAssetPath, "ScG - Dialogue.asset");
            var flowMachineAsset = AssetDatabase.LoadAssetAtPath<FlowMacro>(flowMachineAssetPath);

            var variablesComp = waveGO.AddComponent<Variables>();
            var fmComp = waveGO.AddComponent<FlowMachine>();

            fmComp.nest.SwitchToMacro(flowMachineAsset);

            var playableAssetPath = Path.Combine(creatureBasePath, $"{cutSceneName}.playable");

            AssetDatabase.CreateAsset(timelineAsset, playableAssetPath);
            AssetDatabase.Refresh();

            var dialogueHudGO = GameObject.Instantiate(dialogueHudPrefab, waveGO.transform);

            var hudVariablesComp = dialogueHudGO.GetComponent<Variables>();

            //
            var activationTrackA = timelineAsset.CreateTrack<ActivationTrack>();
            var activationTrackB = timelineAsset.CreateTrack<ActivationTrack>();

            activationTrackA.name = "Dialogue A Use";
            activationTrackB.name = "Dialogue B Use";

            var signalTrack = timelineAsset.CreateTrack<SignalTrack>();
            signalTrack.name = "Signal Use";

            foreach (PlayableBinding output in timelineAsset.outputs)
            {
                if (output.streamName == $"Signal Use")
                {
                    pdComp.SetGenericBinding(output.sourceObject, waveGO);
                }
                else if (output.streamName == $"Dialogue A Use")
                {
                    GameObject assignedGO = null;
                    if (hudVariablesComp != null)
                    {
                        var viewMainGO = hudVariablesComp.declarations.Get<GameObject>("viewMainGO");
                        if (viewMainGO != null)
                        {
                            var viewMainGOVariablesComp = viewMainGO.GetComponent<Variables>();
                            if (viewMainGOVariablesComp != null)
                            {
                                assignedGO = viewMainGOVariablesComp.declarations.Get<GameObject>("dialogueAGO");
                            }
                        }
                    }
                    pdComp.SetGenericBinding(output.sourceObject, assignedGO);
                }
                else if (output.streamName == $"Dialogue B Use")
                {
                    GameObject assignedGO = null;
                    if (hudVariablesComp != null)
                    {
                        var viewMainGO = hudVariablesComp.declarations.Get<GameObject>("viewMainGO");
                        if (viewMainGO != null)
                        {
                            var viewMainGOVariablesComp = viewMainGO.GetComponent<Variables>();
                            if (viewMainGOVariablesComp != null)
                            {
                                assignedGO = viewMainGOVariablesComp.declarations.Get<GameObject>("dialogueBGO");
                            }
                        }
                    }
                    pdComp.SetGenericBinding(output.sourceObject, assignedGO);
                }
            }

            // var scenarioMarker = signalTrack.CreateMarker<TimelineMarker>(activatedAt);
            var conversationList = cutScene?.Root[2].FluffyRoot?.Conversation.ToList();

            var index = 0;
            var elapsedTime = 1.0;
            var duration = 4.0;
            var contentList = new List<string>();
            var speakerList = new List<string>();
            while (index < conversationList.Count)
            {
                var currentConversation = conversationList[index];

                if (currentConversation.String == "end")
                {
                    index += 2;
                }
                else
                {
                    var content = conversationList[index + 0].String;
                    var speaker = conversationList[index + 1].ConversationClass.Empty;

                    content = content.Replace("^", "");

                    if (speaker == "monkey")
                    {
                        var timelineClip = activationTrackA.CreateDefaultClip();
                        timelineClip.start = elapsedTime + 1.0;
                        var timelineMarkerStart = signalTrack.CreateMarker<TimelineMarker>(elapsedTime + 1.0);

                        timelineMarkerStart.actionId = 21004100;

                        timelineMarkerStart.paramOrder = "1100";

                        // timelineMarkerStart.intParams = new List<int>();
                        // timelineMarkerStart.stringParams = new List<string>();
                        timelineMarkerStart.intParams.Add(1000);
                        timelineMarkerStart.stringParams.Add(speaker);

                        timelineClip.duration = duration;
                        elapsedTime += 1.0 + duration;
                        var timelineMarkerEnd = signalTrack.CreateMarker<TimelineMarker>(elapsedTime);

                        timelineMarkerEnd.paramOrder = "0000";
                        timelineMarkerEnd.actionId = 21004200;
                    }
                    else
                    {
                        var timelineClip = activationTrackB.CreateDefaultClip();
                        timelineClip.start = elapsedTime + 1.0;
                        var timelineMarkerStart = signalTrack.CreateMarker<TimelineMarker>(elapsedTime + 1.0);

                        timelineMarkerStart.actionId = 21004100;

                        timelineMarkerStart.paramOrder = "1100";

                        // timelineMarkerStart.intParams = new List<int>();
                        // timelineMarkerStart.stringParams = new List<string>();
                        timelineMarkerStart.intParams.Add(2000);
                        timelineMarkerStart.stringParams.Add(speaker);

                        timelineClip.duration = duration;
                        elapsedTime += 1.0 + duration;
                        var timelineMarkerEnd = signalTrack.CreateMarker<TimelineMarker>(elapsedTime);

                        timelineMarkerEnd.paramOrder = "0000";
                        timelineMarkerEnd.actionId = 21004200;
                    }

                    Debug.Log(content);
                    Debug.Log(speaker);

                    contentList.Add(content);
                    speakerList.Add(speaker);

                    index += 3;
                }
            }

            //
            variablesComp.declarations.Set("currentIndex", 0);
            variablesComp.declarations.Set("contentList", contentList);
            variablesComp.declarations.Set("speakerList", speakerList);
            variablesComp.declarations.Set("dialogueHudGO", dialogueHudGO);

            var srtvsComp = waveGO.AddComponent<SignalReceiverToVS>();
            srtvsComp.vsGameObjects = new List<GameObject>();
            srtvsComp.vsGameObjects.Add(waveGO);

            //
            pdComp.playableAsset = timelineAsset;

            return waveGO;
        }
    }
}
