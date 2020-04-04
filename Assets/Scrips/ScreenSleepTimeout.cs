using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace iDoctorTestTask
{
    public class ScreenSleepTimeout : MonoBehaviour
    {
        private void Start()
        {
            // Disable screen dimming
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}