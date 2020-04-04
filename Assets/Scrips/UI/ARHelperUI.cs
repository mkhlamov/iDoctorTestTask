using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace iDoctorTestTask
{
    public class ARHelperUI : MonoBehaviour
    {
        private Text _text;
        private const string FindingPlaneHint = "Водите экраном, пока не найдется плоскость";
        private const string FoundPlaneHint = "Выберите позицию на плоскости и нажмите в любое место экрана";

        private void Awake()
        {
            _text = transform.GetComponentInChildren<Text>();
            _text.text = FindingPlaneHint;
            _text.enabled = true;
        }

        private void OnEnable()
        {
            PlaneManager.planeFound += OnPlaneFound;
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            PlaneManager.planeFound -= OnPlaneFound;
            GameManager.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            _text.enabled = gameState == GameState.Pregame;
        }

        private void OnPlaneFound()
        {
            _text.text = FoundPlaneHint;
        }
    }
}