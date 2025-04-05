using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Values")]
    public int score;
    public float timer;
    public int scorePerHit;
    
    [Header("States")]
    public bool isGameActive;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Start()
    {
        isGameActive = false;

        StartGame();
    }

    private void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            UIManager.Instance.SetTimeText(timer);
        }
    }

    public void StartGame()
    {
        score = 0;
        timer = 0f;
        UIManager.Instance.SetScoreText(score);
        UIManager.Instance.SetTimeText(timer);

        Time.timeScale = 1f;
        isGameActive = true;
    }

    public void AddScore(int value)
    {
        score += value;
        UIManager.Instance.SetScoreText(score);
    }
}
