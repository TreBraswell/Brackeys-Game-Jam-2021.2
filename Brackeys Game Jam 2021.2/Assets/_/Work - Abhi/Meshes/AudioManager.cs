namespace BGJ20212.Game.AbhiTechGame
{
    using UnityEngine.Audio;
    using UnityEngine;
    using System;

    public class AudioManager : MonoBehaviour
    {
        public Sound[] Sound;
        // public static AudioManager instance;

        public string bgMusicName;

        private void Awake()
        {
            // if (instance == null)
            //     instance = this;
            // else
            // {
            //     Destroy(gameObject);
            //     return;
            // }
            // DontDestroyOnLoad(gameObject);

            foreach (Sound s in Sound)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.outputAudioMixerGroup = s.Output;

                s.source.clip = s.audioClip;

                s.source.volume = s.Volume;

                s.source.pitch = s.Pitch;

                s.source.loop = s.loop;
            }
        }

        private void Start()
        {
            // Play("Forest Background");
            Play(bgMusicName);
        }

        public void Play(string AudioName)
        {
            Sound s = Array.Find(Sound, sound => sound.Name == AudioName);
            if(s != null)
                s.source.Play();
        }
    }
}
