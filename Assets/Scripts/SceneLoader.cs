using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameSession _gameSession;

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }

    public void LoadNextLevel()
    {
        var currentSceneName = _gameSession.LevelName;
        Debug.Log(currentSceneName);
        string i = currentSceneName.Substring(currentSceneName.Length - 2);
        Debug.Log(i);
        long numeber = Int64.Parse(i);
        
        LoadLevelByName("Level " + (numeber + 1));
        
    }

    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex-1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start Menu");
        _gameSession.ResetGame();
    }

    public void LoadLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadMenuScene()
    {
        _gameSession.ResetGame();
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevelPass()
    {
        SceneManager.LoadScene("LevelPassed");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
