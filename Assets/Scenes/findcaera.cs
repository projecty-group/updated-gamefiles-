using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class findcaera : MonoBehaviour

   {
    public GameObject mainCamera;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (SceneManager.GetActiveScene().name == "MaracasBeach")
        {
            mainCamera = GameObject.FindWithTag("MainCamera1");
        }
        else if (SceneManager.GetActiveScene().name == ("TitleScreen"))
        {
            mainCamera = GameObject.FindWithTag("MainCamera2");
        }
        mainCamera.SetActive(true);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == ("MaracasBeach"))
        {
            mainCamera.SetActive(false);
            mainCamera = GameObject.FindWithTag("MainCamera1");
        }
        else if (scene.name == ("TitleScreen"))
        {
            mainCamera.SetActive(false);
            mainCamera = GameObject.FindWithTag("MainCamera2");
        }
        mainCamera.SetActive(true);
    }
    
}
