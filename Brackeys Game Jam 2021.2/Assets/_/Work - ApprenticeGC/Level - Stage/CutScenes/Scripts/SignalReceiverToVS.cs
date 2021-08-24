namespace BGJ20212.Game.ApprenticeGC.Dialogue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Bolt;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    public class SignalReceiverToVS : MonoBehaviour, INotificationReceiver
    {
        // public string eventName;
        public List<GameObject> vsGameObjects;

        public void OnNotify(Playable origin, INotification notification, object context)
        {
            Debug.Log(notification);

            var marker = notification as TimelineMarker;
            if (marker != null)
            {
                Debug.Log("marker != null");
                if (marker.paramOrder.Length != 4) return;

                var stringParamCount = Int32.Parse(marker.paramOrder[0].ToString());
                var intParamCount = Int32.Parse(marker.paramOrder[1].ToString());
                var floatParamCount = Int32.Parse(marker.paramOrder[2].ToString());
                var gameObjectParamCount = Int32.Parse(marker.paramOrder[3].ToString());

                var paramsToPass = new List<object>();
                // paramsToPass.Add($"TM - {marker.actionId}");
                marker.stringParams?.ForEach(p => paramsToPass.Add(p));
                marker.intParams?.ForEach(p => paramsToPass.Add(p));
                marker.floatParams?.ForEach(p => paramsToPass.Add(p));
                marker.goParams?.ForEach(p => paramsToPass.Add(p));

                vsGameObjects.ForEach(x =>
                {
                    Debug.Log($"marker actionId: {marker.actionId.ToString()}");

                    CustomEvent.Trigger(x.gameObject, $"TM - {marker.actionId}", paramsToPass.ToArray());
                });
            }
        }
    }
}
