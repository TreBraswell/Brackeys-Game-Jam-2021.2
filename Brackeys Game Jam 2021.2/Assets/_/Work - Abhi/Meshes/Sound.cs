namespace BGJ20212.Game.AbhiTechGame
{

    using UnityEngine.Audio;
    using UnityEngine;

    [System.Serializable]
    public class Sound
    {
        public string Name;

        public AudioMixerGroup Output;

        public AudioClip audioClip;

        [Range(0f, 1f)]
        public float Volume = 1f;

        [Range(.1f, 3f)]
        public float Pitch = 1f;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
    
}
