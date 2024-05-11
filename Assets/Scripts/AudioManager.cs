using System;
using UnityEngine;

namespace Architecture
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayMusic(string name)
        {
            Sound sound = Array.Find(musicSounds, x => x.name == name);
            if (sound == null)
            {
                Debug.Log($"Sound {name} not found!");
            }
            else
            {
                musicSource.clip = sound.clip;
                musicSource.Play();
            }
        }

        public void PlaySFX(string name, AudioSource source = null)
        {
            Sound sfx = Array.Find(sfxSounds, s => s.name == name);
            if (sfx == null)
            {
                Debug.Log($"Sound {name} not found!");
            }
            else
            {
                AudioSource audioSource = source ?? sfxSource;
                audioSource.clip = sfx.clip;
                audioSource.Play();
            }
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }

        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
        }

        public void MusicVolume(float volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                musicSource.volume = volume;
            }
        }

        public void SFXVolume(float volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                sfxSource.volume = volume;
            }
        }
    }
}

