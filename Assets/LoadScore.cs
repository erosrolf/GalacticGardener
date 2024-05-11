using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScore : MonoBehaviour
{
    public TextMeshProUGUI countText;
    void Start()
    {
        var score = PlayerPrefs.GetInt("Score");
        countText.text = score.ToString();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
