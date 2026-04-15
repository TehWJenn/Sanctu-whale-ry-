using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // We removed the Update() method entirely so clicking does nothing by default.

    // This is the method your Puzzle script will call
    public void TriggerNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}


// using UnityEngine;
// using System.Collections; // ADD THIS: Needed for the non-generic IEnumerator
// using System.Collections.Generic;
// using UnityEngine.SceneManagement;

// public class LevelLoader : MonoBehaviour
// {
//     public Animator transition;
//     public float transitionTime = 1f;

//     void Update()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             LoadNextLevel();
//         }
//     }

//     public void LoadNextLevel()
//     {
//         // This will now work because IEnumerator is correctly identified
//         StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
//     }

//     // This was the line causing the error CS0305
//     IEnumerator LoadLevel(int levelIndex)
//     {
//         transition.SetTrigger("Start");
//         yield return new WaitForSeconds(transitionTime);
        
//         // Fix typo: SceneManger -> SceneManager
//         SceneManager.LoadScene(levelIndex);
//     }
// }
