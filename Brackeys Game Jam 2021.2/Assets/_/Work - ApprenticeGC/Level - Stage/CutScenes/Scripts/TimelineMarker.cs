namespace BGJ20212.Game.ApprenticeGC.Dialogue
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    public class TimelineMarker : Marker, INotification
    {
        public PropertyName id { get; }

        public int actionId;
        public string paramOrder;

        public List<string> stringParams;
        public List<int> intParams;
        public List<float> floatParams;
        public List<GameObject> goParams;

        public TimelineMarker()
        {
            stringParams = new List<string>();
            intParams = new List<int>();
            floatParams = new List<float>();
            goParams = new List<GameObject>();
        }
    }
}
