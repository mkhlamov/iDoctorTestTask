using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace iDoctorTestTask
{
    [RequireComponent(typeof(ActorStats))]
    public class ShooterFromCamera : MonoBehaviour
    {
        [SerializeField] private Camera _arCamera;
        
        [SerializeField] private float _rayDistanceFromCamera = 10.0f;

        [SerializeField] private float _shootingRate = 0.2f;
        private float _shootingTimer = 0f;
        
        [SerializeField] private GameObject _sight;
        
        [SerializeField] private AttackSO _attack;
        private ActorStats _stats;
        private bool _canShoot =  false;

        #region Monobehaviour
        private void Awake()
        {
            _stats = GetComponent<ActorStats>();
            _canShoot = false;
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        private void Update()
        {
            if (!_canShoot){return;}
            
            if (_shootingTimer >= _shootingRate)
            {
                // Shoot on tap or click
#if UNITY_EDITOR
                if (Input.GetMouseButtonUp(0))
#else
                if (Input.touchCount > 0)
#endif
                {
                    Shoot();
                    _shootingTimer = 0f;
                }
            }
            else
            {
                _shootingTimer += Time.deltaTime;
            }
        }
        #endregion

        #region Private Methods

        private void Shoot()
        {
            var ray = _arCamera.ScreenPointToRay(_sight.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _rayDistanceFromCamera))
            {
                var hitGO = hit.transform.gameObject;
                if (hitGO.GetComponent<IAttackable>() != null)
                {
                    var attack = _attack.CreateAttack(_stats);
                    var attackables = hitGO.GetComponentsInChildren<IAttackable>();
                    foreach (var a in attackables)
                    {
                        a.OnAttack(gameObject, attack);
                    }
                }
            }
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _canShoot = gameState == GameState.Running;
        }

        #endregion
    }
}