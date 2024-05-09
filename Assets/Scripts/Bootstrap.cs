using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Architecture;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Scrollbar _loadbar;
    void Start()
    {
        _loadbar.size = 0;
        var width = SettingsManager.Instance.Settings.ResolutionWidth;
        var height = SettingsManager.Instance.Settings.ResolutionHeight;
        var fullScreen = SettingsManager.Instance.Settings.FullScreen;

        Screen.SetResolution(width, height, fullScreen);

        StartCoroutine(LoadSceneWithDelay(1f));
    }

    IEnumerator LoadSceneWithDelay(float delay)
    {
        float elapsedTime = 0;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            _loadbar.size = Mathf.Clamp01(elapsedTime / delay);
            yield return null;
        }

        GameManager.Instance.LoadMenu();
    }
}
