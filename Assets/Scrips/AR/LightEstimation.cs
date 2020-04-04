using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace iDoctorTestTask
{
    public class LightEstimation : MonoBehaviour
    {
        private ARCameraManager _cameraManager;

        [SerializeField] private float mIntensityMultiplier = 2f;

        void Awake()
        {
            _cameraManager = GetComponent<ARCameraManager>();
        }

        void OnEnable()
        {
            if (_cameraManager != null)
                _cameraManager.frameReceived += FrameChanged;
        }

        void OnDisable()
        {
            if (_cameraManager != null)
                _cameraManager.frameReceived -= FrameChanged;
        }

        void FrameChanged(ARCameraFrameEventArgs args)
        {
            if (args.lightEstimation.averageBrightness.HasValue)
            {
                RenderSettings.ambientIntensity = args.lightEstimation.averageBrightness.Value * mIntensityMultiplier;
            }

            if (args.lightEstimation.colorCorrection.HasValue)
            {
                RenderSettings.ambientLight = args.lightEstimation.colorCorrection.Value;
            }
        }
    }
}