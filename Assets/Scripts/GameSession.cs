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
    private int _currentScore = 0;
    private float _elapsedTime = 0;
    private bool _gameRunning = false;
    private string _levelName;
    private int _controls = 0;

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
        _gameRunning = true;
        scoreText.text = _currentScore.ToString();
        _levelName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

        if (_gameRunning)
        {
            CountTime();
        }
    }

    private void CountTime()
    {
        _elapsedTime += Time.deltaTime;

        float minutes = Mathf.Floor(_elapsedTime / 60);
        float seconds = Mathf.RoundToInt(_elapsedTime % 60);

        timeText.text =
            (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString()) + ":" +
            (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString());
    }

    public void AddToScore(int blockPoints)
    {
        _currentScore += blockPoints;
        scoreText.text = _currentScore.ToString();
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
        _gameRunning = false;
    }

    public void StartTimer()
    {
        _gameRunning = true;
    }
    
    public string LevelName
    {
        get => _levelName;
        set => _levelName = value;
    }

    public int Controls
    {
        get => _controls;
        set => _controls = value;
    }
}
