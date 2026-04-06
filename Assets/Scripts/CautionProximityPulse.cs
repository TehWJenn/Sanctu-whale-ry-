using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CautionTriggerPulse : MonoBehaviour
{
    [Header("References")]
    public Light2D cautionLight;
    
    [Header("Pulse Settings")]
    public float pulseSpeed = 5f;
    public float maxIntensity = 4f;
    public float minIntensity = 0.5f;

    [Header("Transition Settings")]
    public float fadeSpeed = 2f; // How fast the light turns on/off
    
    private bool isPlayerInside = false;
    private float currentTargetIntensity = 0f;

    void Update()
    {
        if (isPlayerInside)
        {
            // Calculate the pulse wave (0 to 1)
            float pulse = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
            // Set target to a value between min and max
            currentTargetIntensity = Mathf.Lerp(minIntensity, maxIntensity, pulse);
        }
        else
        {
            // Fade out to zero
            currentTargetIntensity = 0f;
        }

        // Smoothly lerp the actual light intensity to the target
        cautionLight.intensity = Mathf.MoveTowards(cautionLight.intensity, currentTargetIntensity, fadeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure your player object has the Tag "Player"
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
        if (other.CompareTag("Player")){
        Debug.Log("Player entered the zone!"); // This will pop up in your Console
        isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

}