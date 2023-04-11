using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
{ // Start is called before the first frame update
  

    public GameObject mainCamera;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MaracasBeach")
        {
            mainCamera = GameObject.FindWithTag("MainCamera1");
        }
        else if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            mainCamera = GameObject.FindWithTag("MainCamera2");
        }
        mainCamera.SetActive(true);
    }

}
