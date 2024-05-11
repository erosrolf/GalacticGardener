using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _scoreCount = 0;

    public int GetScore() => _scoreCount;

    void OnEnable()
    {
        CollideInspector.CollideWithTree += AddScore;
    }

    void OnDisable()
    {
        CollideInspector.CollideWithTree -= AddScore;
    }

    public void AddScore()
    {
        _scoreText.text = (++_scoreCount).ToString();
    }
}
