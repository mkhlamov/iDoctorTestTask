using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace iDoctorTestTask
{
    public class PlaneManager : MonoBehaviour
    {
        public static event Action planeFound;
        public static event Action<Vector3> PlaceSelected;

        [SerializeField] private GameObject _previewPrefab;
        private GameObject _previewObject;
        private ARSessionOrigin _ARSessionOrigin;
        private ARPlaneManager _ARPlaneManager;
        private ARRaycastManager _ARRaycastManager;
        private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();
        private bool _MoveWithScreen = true;
        private bool _FirstPlaneFound = false;
        private Vector3 _lastHitPosePosition;
        private Vector2 _screenCenter;

        private void Awake()
        {
            _ARRaycastManager = FindObjectOfType<ARRaycastManager>();
            _ARPlaneManager = FindObjectOfType<ARPlaneManager>();
            _ARSessionOrigin = FindObjectOfType<ARSessionOrigin>();
            _screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }

        private void OnEnable()
        {
            _MoveWithScreen = false;
            _ARPlaneManager.planesChanged += OnPlanesChanged;
        }

        private void OnDisable()
        {
            _ARPlaneManager.planesChanged -= OnPlanesChanged;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs planesChanged)
        {
            if (!_FirstPlaneFound && planesChanged.added.Count > 0)
            {
                _FirstPlaneFound = true;
                _MoveWithScreen = true;
                _previewObject = Instantiate(_previewPrefab);
            }
        }

        private void Update()
        {
            if (!_MoveWithScreen){return;}
            if (!_FirstPlaneFound){return;}

            if (Input.touchCount > 0)
            {
                _MoveWithScreen = false;
                Destroy(_previewObject);
                //stop tracking new planes
                _ARPlaneManager.SetTrackablesActive(false);
                _ARPlaneManager.enabled = false;
                
                PlaceSelected?.Invoke(_lastHitPosePosition);
                return;
            }
            
            if (_ARRaycastManager.Raycast(_screenCenter, _hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                var hitPose = _hits[0].pose;
                _lastHitPosePosition = hitPose.position;
                _ARSessionOrigin.MakeContentAppearAt(_previewObject.transform, hitPose.position);
            }
        }
    }
}