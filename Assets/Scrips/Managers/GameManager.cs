using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace iDoctorTestTask
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<GameState> GameStateChanged;

        private GameState _currentGameState;

        #region Monobehaviour Methods

        protected override void Start()
        {
            base.Start();
            SpawnManager.AllEnemiesKilled += GameWon;
            SetGameState(GameState.Pregame);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SpawnManager.AllEnemiesKilled -= GameWon;
        }

        #endregion

        #region Public Methods

        public void StartGame()
        {
            SetGameState(GameState.Running);
        }

        #endregion
        
        #region Private Methods

        private void SetGameState(GameState gs)
        {
            _currentGameState = gs;
            GameStateChanged?.Invoke(_currentGameState);
        }

        private void GameWon()
        {
            SetGameState(GameState.Won);
        }
        
        private void GameLost()
        {
            SetGameState(GameState.Lost);
        }

        #endregion
    }

    public enum GameState
    {
        Pregame,
        Running,
        Won,
        Lost
    }
}