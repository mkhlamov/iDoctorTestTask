using System;
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
        GameManager.GameStarted += OnGameStarted;
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
    private void OnGameStarted()
    {
        _score = 0;
        UpdateScoreText();
    }
    
    private void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }

    #endregion
}
