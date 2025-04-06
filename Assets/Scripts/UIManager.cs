using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Key Guide")]
    [SerializeField] private GameObject guidePanel;
    
    [Header("Result")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI objectiveDescriptionText;
    [SerializeField] private TextMeshProUGUI timeDescriptionText;
    [SerializeField] private TextMeshProUGUI totalDescriptionText;
    [SerializeField] private TextMeshProUGUI objectiveResultText;
    [SerializeField] private TextMeshProUGUI timeResultText;
    [SerializeField] private TextMeshProUGUI totalResultText;
    [SerializeField] private TextMeshProUGUI restartInfoText;
    
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
        objectiveText.text = $"{value} / {PlayManager.Instance.maxObjective}";
    }
    
    public void SetTimeText(float value)
    {
        var span = TimeSpan.FromSeconds(value);
        timeText.text = span.ToString(@"m\:ss\.ff");
    }

    public void SetStatusText(string value)
    {
        statusText.text = value;
    }

    public void SetGuidePanelActive(bool value)
    {
        guidePanel.SetActive(value);
    }

    public IEnumerator ShowResult(string objectiveScore, string timeScore, string totalScore)
    {
        objectiveResultText.text = objectiveScore;
        timeResultText.text = timeScore;
        totalResultText.text = totalScore;
        
        objectiveDescriptionText.gameObject.SetActive(false);
        timeDescriptionText.gameObject.SetActive(false);
        totalDescriptionText.gameObject.SetActive(false);
        objectiveResultText.gameObject.SetActive(false);
        timeResultText.gameObject.SetActive(false);
        totalResultText.gameObject.SetActive(false);
        restartInfoText.gameObject.SetActive(false);
        resultPanel.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        objectiveDescriptionText.gameObject.SetActive(true);
        objectiveResultText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        timeDescriptionText.gameObject.SetActive(true);
        timeResultText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        totalDescriptionText.gameObject.SetActive(true);
        totalResultText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        restartInfoText.gameObject.SetActive(true);
    }
}
