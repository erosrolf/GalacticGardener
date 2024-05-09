using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Architecture
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _SFXSlider;

        public void SetMusicVolume()
        {
            float dB = Mathf.Lerp(-80, 0, _musicSlider.value);
            _audioMixer.SetFloat("Music", dB);
        }

        public void SetSFXVolume()
        {
            float dB = Mathf.Lerp(-80, 0, _SFXSlider.value);
            _audioMixer.SetFloat("SFX", dB);
        }

    }
}