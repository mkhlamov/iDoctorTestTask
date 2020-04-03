using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace iDoctorTestTask
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action GameStarted;

        protected override void Start()
        {
            base.Start();
            var enemies = FindObjectsOfType<Enemy>();
            Debug.Log($"{ScoreManager.Instance == null}");
            Debug.Log($"{ScoreManager.IsInitialized}");
            foreach (var e in enemies)
            {
                ScoreManager.Instance.SubscribeToKillable(e.GetComponent<KillableEvent>());
            }
            GameStarted?.Invoke();
        }
    }

    public enum GameState
    {
        PREGAME,
        RUNNING,
        WON,
        LOST
    }
}