using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // config
    [Range(0.1f, 10f)] [SerializeField] private float gameSpeed = 1f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private bool isAutoPlayEnabled;
    
    // state
    private int currentScore = 0;
    private float elapsedTime = 0;
    private bool gameRunning = false;
    private string levelName;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        gameRunning = true;
        scoreText.text = currentScore.ToString();
        levelName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

        if (gameRunning)
        {
            CountTime();
        }
    }

    private void CountTime()
    {
        elapsedTime += Time.deltaTime;

        float minutes = Mathf.Floor(elapsedTime / 60);
        float seconds = Mathf.RoundToInt(elapsedTime % 60);

        timeText.text =
            (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString()) + ":" +
            (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString());
    }

    public void AddToScore(int blockPoints)
    {
        currentScore += blockPoints;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public void StopTimer()
    {
        gameRunning = false;
    }

    public void StartTimer()
    {
        gameRunning = true;
    }
    
    public string LevelName
    {
        get { return levelName; }
        set { levelName = value; }
    }
}
