using System;
using System.Collections;
using System.Collections.Generic;
using iDoctorTestTask;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Sight : MonoBehaviour
{
    private Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
    }
    
    private void OnGameStateChanged(GameState gs)
    {
        _image.enabled = gs == GameState.Running;
    }
}
