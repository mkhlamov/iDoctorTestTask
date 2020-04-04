using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace iDoctorTestTask
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<GameState> GameStateChanged;

        [SerializeField] private GameObject _game;
        private GameState _currentGameState;
        private ShooterFromCamera _player;

        #region Monobehaviour Methods

        protected override void Start()
        {
            base.Start();
            _player = FindObjectOfType<ShooterFromCamera>();
            _player.GetComponent<KillableEvent>().KillableDead += OnPlayerDead;
            SpawnManager.AllEnemiesKilled += GameWon;
            PlaneManager.PlaceSelected += OnPlaceSelected;
            SetGameState(GameState.Pregame);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_player != null) { _player.GetComponent<KillableEvent>().KillableDead -= OnPlayerDead; }
            SpawnManager.AllEnemiesKilled -= GameWon;
            PlaneManager.PlaceSelected -= OnPlaceSelected;
        }

        #endregion
        
        #region Private Methods

        private void StartGame()
        {
            SetGameState(GameState.Running);
        }
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
            _player.GetComponent<ActorStats>().ResetStats();
            GameLost();
        }
        
        private void OnPlaceSelected(Vector3 place)
        {
            _game.SetActive(true);
            FindObjectOfType<ARSessionOrigin>().MakeContentAppearAt(_game.transform, place);
            StartGame();
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