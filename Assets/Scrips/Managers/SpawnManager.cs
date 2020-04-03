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
        public static event Action<GameObject> EnemySpawned;
        
        [SerializeField] private int _maxEnemies = 10;
        [SerializeField] private float _spawnRate = 0.7f;
        [SerializeField] private List<GameObject> _enemyPrefabs;
        private float _timeSinceLastSpawn = 0f;
        [SerializeField] private List<GameObject> _enemiesSpawned = new List<GameObject>();
        private int _enemiesSpawnedTotalCount = 0;
        private bool _needToSpawn = false;
        
        #region Monobehaviour Methods
        
        protected override void Awake()
        {
            base.Awake();
            _needToSpawn = false;
            GameManager.GameStarted += OnGameStarted;
        }

        private void Update()
        {
            if (!_needToSpawn) {return;}
            if (_enemiesSpawnedTotalCount >= _maxEnemies) {return;}

            if (_timeSinceLastSpawn >= _spawnRate)
            {
                var spawnPlace = GetSpawnCoords();
                Debug.Log($"{spawnPlace}");
                var enemySpawned = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)], spawnPlace,
                    Quaternion.identity);
                enemySpawned.transform.parent = gameObject.transform;
                _enemiesSpawned.Add(enemySpawned);
                EnemySpawned?.Invoke(enemySpawned);
                var killableEvent = enemySpawned.GetComponent<KillableEvent>();
                if (killableEvent != null)
                {
                    killableEvent.KillableDead += OnKillableDead;
                }

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

        private void OnGameStarted()
        {
            _needToSpawn = true;
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
        }
        
        #endregion
    }
}