using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    public static UIManager Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void SetSpeedText(float value)
    {
        speedText.text = $"{value:F1}";
    }
    
    public void SetScoreText(int value)
    {
        scoreText.text = $"{value}";
    }
    
    public void SetTimeText(float value)
    {
        var span = TimeSpan.FromSeconds(value);
        timeText.text = span.ToString(@"m\:ss\.ff");
    }
}
