﻿using System;
using UnityEngine;

namespace iDoctorTestTask
{
    public class KillableEvent : MonoBehaviour, IKillable
    {
        public event Action KillableDead;

        public void OnDeath(GameObject killer)
        {
            KillableDead?.Invoke();
        }
    }
}