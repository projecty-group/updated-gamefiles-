using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameLoader : MonoBehaviour
{
    public string miniGameSceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Save the user's settings before loading the mini-game scene
            SaveSettings();

            SceneManager.LoadScene(miniGameSceneName);
        }
    }

    void SaveSettings()
    {
        // Add code here to save the user's settings for the current scene
        // This can be done using PlayerPrefs or by writing data to a file
    }
}
