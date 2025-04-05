using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance { get; private set; }

    [Header("Values")]
    public int objectives;
    public int maxObjective;
    public float timer;
    public int activatedTriggers;
    
    [Header("States")]
    public bool isGameActive;
    public bool canRestart;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isGameActive = false;
        canRestart = false;

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            UIManager.Instance.SetTimeText(timer);
        }
        if (Input.GetKeyDown(KeyCode.Return) && canRestart)
        {
            SceneManager.LoadScene("Scenes/MainScene");
        }
    }

    private IEnumerator StartGame()
    {
        objectives = 0;
        timer = 0f;
        UIManager.Instance.SetScoreText(objectives);
        UIManager.Instance.SetTimeText(timer);
        
        UIManager.Instance.SetStatusText("Ready");
        yield return new WaitForSeconds(2f);
        UIManager.Instance.SetStatusText("Start!");
        isGameActive = true;
        yield return new WaitForSeconds(1f);
        UIManager.Instance.SetStatusText(string.Empty);
        canRestart = true;
    }

    public void AddScore(int value)
    {
        objectives += value;
        UIManager.Instance.SetScoreText(objectives);
    }

    public void ActivateTrigger(int value)
    {
        switch (value)
        {
            case 0:
                if (activatedTriggers == 1)
                {
                    StartCoroutine(FinishGame());
                }

                break;
            
            case 1:
                activatedTriggers = 1;
                break;
            
            default:
                Debug.LogWarning($"Undefined trigger value: {value}");
                break;
        }
    }

    private IEnumerator FinishGame()
    {
        isGameActive = false;
        canRestart = false;
        UIManager.Instance.SetStatusText("Finish!");
        yield return new WaitForSeconds(1f);
        UIManager.Instance.SetStatusText(string.Empty);

        var objectiveScore = objectives * 100;
        var timeScore = Mathf.RoundToInt(Mathf.Clamp(30000f - timer * 100f, 0f, 30000f));
        var totalScore = objectiveScore + timeScore;

        StartCoroutine(UIManager.Instance.ShowResult(objectiveScore.ToString(), timeScore.ToString(),
            totalScore.ToString()));

        yield return new WaitForSeconds(3f);
        canRestart = true;
    }
}
