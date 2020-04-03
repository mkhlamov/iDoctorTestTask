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
        private ShooterFromCamera _player;

        #region Monobehaviour Methods

        protected override void Start()
        {
            base.Start();
            _player = FindObjectOfType<ShooterFromCamera>();
            _player.GetComponent<KillableEvent>().KillableDead += OnPlayerDead;
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
        
        private void OnPlayerDead(KillableEvent obj)
        {
            GameLost();
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