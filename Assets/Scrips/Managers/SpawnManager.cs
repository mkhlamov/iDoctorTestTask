using System;
using System.Collections;
using System.Collections.Generic;
using iDoctorTestTask;
using UnityEngine;
using Random = UnityEngine.Random;

namespace iDoctorTestTask
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        public static event Action AllEnemiesSpawned;
        public static event Action AllEnemiesKilled;
        public static event Action<GameObject> EnemySpawned;
        
        [SerializeField] private int _maxEnemies = 10;
        [SerializeField] private float _spawnRate = 0.7f;
        [SerializeField] private List<GameObject> _enemyPrefabs;
        [SerializeField] private Transform _spawnParent;
        private float _timeSinceLastSpawn = 0f;
        [SerializeField] private List<GameObject> _enemiesSpawned = new List<GameObject>();
        private int _enemiesSpawnedTotalCount = 0;
        private bool _needToSpawn = false;
        
        #region Monobehaviour Methods
        
        protected override void Awake()
        {
            base.Awake();
            _needToSpawn = false;
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        private void Update()
        {
            if (!_needToSpawn) {return;}
            if (_enemiesSpawnedTotalCount >= _maxEnemies) {return;}

            if (_timeSinceLastSpawn >= _spawnRate)
            {
                SpawnEnemy();

                _timeSinceLastSpawn = 0f;
                _enemiesSpawnedTotalCount += 1;
                if (_enemiesSpawnedTotalCount == _maxEnemies)
                {
                    AllEnemiesSpawned?.Invoke();
                }
            }
            else
            {
                _timeSinceLastSpawn += Time.deltaTime;
            }
        }

        #endregion

        #region Private Methods

        private void SpawnEnemy()
        {
            var spawnPlace = GetSpawnCoords();
            Debug.Log($"{spawnPlace}");
            var enemySpawned = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)], spawnPlace,
                Quaternion.identity);
            enemySpawned.transform.parent = _spawnParent;
            _enemiesSpawned.Add(enemySpawned);
            EnemySpawned?.Invoke(enemySpawned);
            var killableEvent = enemySpawned.GetComponent<KillableEvent>();
            if (killableEvent != null)
            {
                killableEvent.KillableDead += OnKillableDead;
            }
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _needToSpawn = gameState == GameState.Running;
            
            switch (gameState)
            {
                case GameState.Pregame:
                    ClearSpawnedEnemies();
                    break;
                case GameState.Running:
                    ClearSpawnedEnemies();
                    _timeSinceLastSpawn = 0f;
                    _enemiesSpawnedTotalCount = 0;
                    break;
                case GameState.Won:
                    break;
                case GameState.Lost:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        private void ClearSpawnedEnemies()
        {
            foreach (var go in _enemiesSpawned)
            {
                Destroy(go);
            }
            Debug.Log($"1 ClearSpawnedEnemies {_enemiesSpawned.Count}");
            _enemiesSpawned.Clear();
            Debug.Log($"2 ClearSpawnedEnemies {_enemiesSpawned.Count}");
        }

        private Vector3 GetSpawnCoords()
        {
            var angle = Random.Range(0f, 360f);
            var r = Random.Range(2f, 4f);
            return new Vector3(r * Mathf.Cos(angle), 
                Random.Range(0.3f, 2f),
                r * Mathf.Sin(angle));
        }
        
        private void OnKillableDead(KillableEvent obj)
        {
            _enemiesSpawned.Remove(obj.gameObject);
            if (_enemiesSpawnedTotalCount == _maxEnemies && _enemiesSpawned.Count == 0)
            {
                AllEnemiesKilled?.Invoke();
            }
        }
        
        #endregion
    }
}