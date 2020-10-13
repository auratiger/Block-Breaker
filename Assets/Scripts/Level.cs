using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int breakableBlocks;

    private SceneLoader _sceneLoader;
    private GameSession _gameSession;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _gameSession = FindObjectOfType<GameSession>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            _gameSession.ResetGame();
            _sceneLoader.LoadMenuScene();
        }
    }
    
}
