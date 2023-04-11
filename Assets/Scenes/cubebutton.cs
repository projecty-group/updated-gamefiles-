using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cubebutton : MonoBehaviour
{

  private void LoadNextScene()
    {
        // Disable all controls in the first scene
        GameObject[] controls = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject control in controls)
        {
            control.SetActive(false);
        }
        
        // Load the next scene
        SceneManager.LoadScene("SecondScene");
    }

    
}
