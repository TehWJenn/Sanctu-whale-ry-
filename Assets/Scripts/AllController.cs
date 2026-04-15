using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class AllController : MonoBehaviour
{
    public static AllController Instance;

    public int count = 0;
    public GameObject win; // The black screen with the sound
    
    [Header("Level Transition")]
    public LevelLoader levelLoader; // Drag your LevelLoader object here
    public float delayBeforeFade = 2.0f; // Time for the sound to play

    private void Awake()
    {
        Instance = this;
    }

    public void Add()
    {
        count += 1;
        if (count >= 3)
        {
            StartCoroutine(WinSequence());
        }
    }

    IEnumerator WinSequence()
    {
        // 1. Show the win screen and start the sound
        if (win != null)
        {
            win.SetActive(true);
        }

        // 2. Wait for the sound to play before starting the fade
        yield return new WaitForSeconds(delayBeforeFade);

        // 3. Call the CORRECT function name from your LevelLoader script
        if (levelLoader != null)
        {
            levelLoader.TriggerNextLevel(); 
        }
        else
        {
            // Fallback: just load the next scene index if LevelLoader is missing
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

// using UnityEngine;

// public class AllController : MonoBehaviour
// {
//     public static AllController Instance;

//     public int count = 0;
//     public GameObject win;

//     private void Awake()
//     {
//         Instance = this;
//     }

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     public void Add()
//     {
//         count += 1;
//         if (count >= 3)
//         {
//             win.gameObject.SetActive(true);
//         }
//     }
// }
