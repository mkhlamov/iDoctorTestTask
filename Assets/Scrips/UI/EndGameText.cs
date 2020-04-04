using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace iDoctorTestTask
{
    public class EndGameText : MonoBehaviour
    {
        [SerializeField] private Text _winText;
        [SerializeField] private Text _loseText;

        private void OnEnable()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
            DisableTexts();
        }

        private void OnDisable()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Pregame:
                    DisableTexts();
                    break;
                case GameState.Running:
                    DisableTexts();
                    break;
                case GameState.Won:
                    OnWin();
                    break;
                case GameState.Lost:
                    OnLose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        private void DisableTexts()
        {
            _winText.enabled = false;
            _loseText.enabled = false;
        }

        private void OnWin()
        {
            _winText.enabled = true;
            _loseText.enabled = false;
        }
        
        private void OnLose()
        {
            _winText.enabled = false;
            _loseText.enabled = true;
        }
    }
}