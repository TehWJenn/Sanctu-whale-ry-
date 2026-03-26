using UnityEngine;
using UnityEngine.Rendering.Universal; 
using System.Collections;

public class LightToggle : MonoBehaviour
{
    private Light2D myLight;
    private Coroutine currentCoroutine;

    [Header("Settings")]
    [SerializeField] private float lightDuration = 2f; // How long it stays bright
    [SerializeField] private float fadeDuration = 2f;   // How long it takes to dim
    [SerializeField] private float maxIntensity = 2f;   // The brightness when ON

    void Start()
    {
        myLight = GetComponent<Light2D>();
        if (myLight != null)
        {
            myLight.enabled = false;
            myLight.intensity = 0f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        if (myLight == null) return;

        // Stop any current timer or fade happening
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);

        if (!myLight.enabled || myLight.intensity < 0.1f)
        {
            // Turn ON
            myLight.enabled = true;
            myLight.intensity = maxIntensity;
            currentCoroutine = StartCoroutine(WaitThenFade());
        }
        else
        {
            // Turn OFF immediately if pressed again
            myLight.intensity = 0f;
            myLight.enabled = false;
        }
    }

    IEnumerator WaitThenFade()
    {
        // 1. Wait for the 30 seconds
        yield return new WaitForSeconds(lightDuration);

        // 2. Start the Fade
        float startIntensity = myLight.intensity;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            // Math.Lerp moves smoothly between two numbers
            myLight.intensity = Mathf.Lerp(startIntensity, 0f, elapsed / fadeDuration);
            yield return null; // Wait for the next frame
        }

        myLight.intensity = 0f;
        myLight.enabled = false;
        Debug.Log("Light has completely faded out.");
    }
}




// using UnityEngine;
// using UnityEngine.Rendering.Universal; // Required for Light2D

// public class LightToggle : MonoBehaviour
// {
//     private Light2D myLight;

//     void Start()
//     {
//         // Get the Light2D component on this object
//         myLight = GetComponent<Light2D>();
        
//         // Start with the light turned off
//         if (myLight != null)
//             myLight.enabled = false;
//     }

//     void Update()
//     {
//         // Check for left mouse button click
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             // Check if it's a double click based on the time since last click
//             // if (IsDoubleClick())
//             // {
//                 ToggleLight();
//             // }
//         }
//     }

//     private float lastClickTime;
//     private float doubleClickThreshold = 0.3f; // Adjust this for speed

//     bool IsDoubleClick()
//     {
//         float timeSinceLastClick = Time.time - lastClickTime;
//         lastClickTime = Time.time;
//         return timeSinceLastClick <= doubleClickThreshold;
//     }

//     void ToggleLight()
//     {
//         if (myLight != null)
//         {
//             myLight.enabled = !myLight.enabled;
//         }
//     }
// }