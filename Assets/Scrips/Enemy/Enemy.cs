using System;
using UnityEngine;

namespace iDoctorTestTask
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _speedRotation = 50f;
        [SerializeField] private float _speedMove = 5f;
        [SerializeField] private float _attackDistSquared = 0.6f;
        [SerializeField] private AttackSO _attack;
        private GameObject _player;
        private ActorStats _actorStats;
        private bool _moveToPlayer = true;

        private void Awake()
        {
            _player = FindObjectOfType<ShooterFromCamera>().gameObject;
            _actorStats = GetComponent<ActorStats>();
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }

        private void Update()
        {
            LookAtPlayer();
            MoveToPlayer();
            AttackPlayer();
        }

        private void AttackPlayer()
        {
            if (Vector3.SqrMagnitude(transform.position - _player.transform.position) < _attackDistSquared)
            {
                var attack = _attack.CreateAttack(_actorStats);
                var attackableComponents = _player.GetComponentsInChildren<IAttackable>();
                foreach (var a in attackableComponents)
                {
                    a.OnAttack(gameObject, attack);
                }
            }
        }

        private void MoveToPlayer()
        {
            if (_moveToPlayer)
            {
                transform.position = Vector3.Slerp(transform.position, _player.transform.position,
                    Time.deltaTime * _speedMove);
            }
        }

        private void LookAtPlayer()
        {
            var direction = _player.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotation);
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _moveToPlayer = gameState == GameState.Running;
        }
    }
}