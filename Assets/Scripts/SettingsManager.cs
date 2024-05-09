using System;
using UnityEngine;

namespace Architecture
{
    public class SettingsManager : MonoBehaviour
    {

        public SettingsSaveData Settings { get; private set; }
        public static SettingsManager Instance;

        void Awake()
        {
            if (Instance is null)
            {
                Settings = new SettingsSaveData();
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetVolume(Action<float> setVolumeAction, float volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                setVolumeAction(volume);
            }
        }

        public void SetResolution(int width, int height)
        {
            if (width >= 600 && width <= 3840 &&
                height >= 400 && height <= 2160)
            {
                Settings.ResolutionWidth = width;
                Settings.ResolutionHeight = height;
            }
        }

        public void SetFullscreen(bool b) => Settings.FullScreen = b;
    }
}