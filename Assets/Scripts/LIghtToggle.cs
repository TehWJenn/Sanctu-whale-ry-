using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D

public class LightToggle : MonoBehaviour
{
    private Light2D myLight;

    void Start()
    {
        // Get the Light2D component on this object
        myLight = GetComponent<Light2D>();
        
        // Start with the light turned off
        if (myLight != null)
            myLight.enabled = false;
    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Check if it's a double click based on the time since last click
            if (IsDoubleClick())
            {
                ToggleLight();
            }
        }
    }

    private float lastClickTime;
    private float doubleClickThreshold = 0.3f; // Adjust this for speed

    bool IsDoubleClick()
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;
        return timeSinceLastClick <= doubleClickThreshold;
    }

    void ToggleLight()
    {
        if (myLight != null)
        {
            myLight.enabled = !myLight.enabled;
        }
    }
}