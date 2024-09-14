using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARCameraManager))]
public class LightEstimation : MonoBehaviour
{
    private ARCameraManager cameraManager;

    void Awake()
    {
        cameraManager = GetComponent<ARCameraManager>();
        cameraManager.frameReceived += FrameUpdated;
    }

    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            RenderSettings.ambientIntensity = args.lightEstimation.averageBrightness.Value * 1.5f;
        }
        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            RenderSettings.ambientLight = ColorTemperatureToColor(args.lightEstimation.averageColorTemperature.Value);
        }
        if (args.lightEstimation.mainLightDirection.HasValue && args.lightEstimation.mainLightIntensityLumens.HasValue)
        {
            Light mainLight = RenderSettings.sun;
            mainLight.intensity = args.lightEstimation.mainLightIntensityLumens.Value;
            mainLight.transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);
        }
    }

    Color ColorTemperatureToColor(float temp)
    {
        return new Color(1.0f, 0.9f, 0.8f); 
    }

    void OnDestroy()
    {
        cameraManager.frameReceived -= FrameUpdated;
    }
}
