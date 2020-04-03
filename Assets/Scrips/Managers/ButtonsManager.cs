using System;
using System.Collections;
using System.Collections.Generic;
using iDoctorTestTask;
using UnityEngine;

public class ButtonsManager : Singleton<ButtonsManager>
{
    protected override void Start()
    {
        base.Start();
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        ShowButtons(gameState != GameState.Running);
    }

    private void ShowButtons(bool isOn)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isOn);
        }
    }
}
