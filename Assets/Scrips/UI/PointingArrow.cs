using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace iDoctorTestTask
{
    public class PointingArrow : MonoBehaviour
    {
        private RectTransform _arrow;
        private GameObject _player;

        private void Awake()
        {
            _arrow = transform.GetChild(0).GetComponent<RectTransform>();
            _arrow.gameObject.SetActive(false);
            _player = FindObjectOfType<ShooterFromCamera>().gameObject;
        }


        private void OnEnable()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
        }
        
        private void OnDisable()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }

        private void Update()
        {
            if (!_arrow.gameObject.activeSelf){return;}

            var nearestEnemyPosition = SpawnManager.Instance.GetNearestEnemyPosition();
            if (nearestEnemyPosition != _player.transform.position)
            {
                var playerXZ = Vector3.ProjectOnPlane(_player.transform.forward, Vector3.up);
                var enemyXZ = Vector3.ProjectOnPlane(nearestEnemyPosition, Vector3.up);
                var angle = Vector3.SignedAngle(enemyXZ, playerXZ, Vector3.up);
                //_arrow.Rotate(new Vector3(0, 0, angle));
                var rot = _arrow.rotation.eulerAngles;
                rot.z = angle;
                _arrow.rotation = Quaternion.Euler(rot);
            }
        }

        private void OnGameStateChanged(GameState gameState)
        {
            _arrow.gameObject.SetActive(gameState == GameState.Running);
        }
    }
}