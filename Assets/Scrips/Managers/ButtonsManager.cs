using System;
using System.Collections;
using System.Collections.Generic;
using iDoctorTestTask;
using UnityEngine;

public class ButtonsManager : Singleton<ButtonsManager>
{
    private void OnEnable()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        ShowButtons(gameState == GameState.Won || gameState == GameState.Lost);
    }

    private void ShowButtons(bool isOn)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isOn);
        }
    }
}
