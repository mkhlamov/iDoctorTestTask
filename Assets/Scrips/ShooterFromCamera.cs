using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace iDoctorTestTask
{
    public class ShooterFromCamera : MonoBehaviour
    {
        [SerializeField] private Camera arCamera;
        
        [SerializeField] private float rayDistanceFromCamera = 10.0f;

        [SerializeField] private float shootingRate = 0.2f;
        private float shootingTimer = 0f;
        
        [SerializeField] private GameObject sight;

        private void Update()
        {
            if (shootingTimer >= shootingRate)
            {
                // Shoot on tap or click
#if UNITY_EDITOR
                if (Input.GetMouseButtonUp(0))
#else
                if (Input.touchCount > 0)
#endif
                {
                    var ray = arCamera.ScreenPointToRay(sight.transform.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, rayDistanceFromCamera))
                    {
                        var enemy = hit.transform;
                        if (enemy.name == "Enemy")
                        {
                            Debug.Log($"Hit {enemy.name} {enemy.transform.position}");
                        }
                    }

                    shootingTimer = 0f;
                }
            }
            else
            {
                shootingTimer += Time.unscaledDeltaTime;
            }
        }
    }
}