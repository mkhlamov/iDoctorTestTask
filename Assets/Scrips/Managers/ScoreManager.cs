﻿using System;
using System.Collections;
using System.Collections.Generic;
using iDoctorTestTask;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreManager : Singleton<ScoreManager>
{
    private Text _scoreText;
    private int _score = 0;

    #region Monobehaviour Methods
    
    protected override void Awake()
    {
        base.Awake();
        _scoreText = GetComponent<Text>();
        UpdateScoreText();
        GameManager.GameStateChanged += OnGameStateChanged;
        SpawnManager.EnemySpawned += OnEnemySpawned;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.GameStateChanged -= OnGameStateChanged;
        SpawnManager.EnemySpawned -= OnEnemySpawned;
    }

    #endregion

    #region Public Methods

    public void SubscribeToKillable(KillableEvent ke)
    {
        ke.KillableDead += OnKill;
    }

    #endregion

    #region Private Methods
    private void OnKill(KillableEvent killableEvent)
    {
        killableEvent.KillableDead -= OnKill;
        _score += 1;
        UpdateScoreText();
    }
    private void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Running:
                _score = 0;
                UpdateScoreText();
                break;
            case GameState.Pregame:
                _score = 0;
                ClearScoreText();
                break;
            case GameState.Won:
                break;
            case GameState.Lost:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
        
    }

    private void OnEnemySpawned(GameObject e)
    {
        if (e.GetComponent<KillableEvent>() != null)
        {
            SubscribeToKillable(e.GetComponent<KillableEvent>());
        }
        else
        {
            Debug.LogError($"No killableEvent on {e.name}");
        }
    }
    
    private void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }
    
    private void ClearScoreText()
    {
        _scoreText.text = "";
    }

    #endregion
}
