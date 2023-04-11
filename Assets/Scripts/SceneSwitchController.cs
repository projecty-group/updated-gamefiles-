using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchController : MonoBehaviour
{
    // Reference to the GameObject or script from the previous scene
    public GameObject prevGameObject1;
    //public GameObject prevGameObject2;

    // Reference to the GameObject or script for this scene
    public GameObject thisGameObject1;
    //public GameObject thisGameObject2;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        
        // Find the GameObject or script from the previous scene
        prevGameObject1 = GameObject.Find("firstperson player");
        //prevGameObject2 = GameObject.Find(prevGameObject2);

        // Deactivate the GameObject or script from the previous scene
        if (prevGameObject1 != null)  //&& prevGameObject2!= null)
        {
            prevGameObject1.SetActive(false);
            //prevGameObject2.SetActive(false);
        }

        // Assign your new input events and control logic for this scene
        thisGameObject1 = GameObject.Find("Canvas");
        //thisGameObject2 = GameObject.Find(thisGameObject2);

        if (thisGameObject1 != null) //&& thisGameObject2 != null)
        {
            thisGameObject1.SetActive(true);
            //thisGameObject2.SetActive(true);
        }
    }

    // Switch to the previous scene
    public void SwitchToPreviousScene()
    {
        // Reactivate the GameObject or script from the previous scene
        if (prevGameObject1 != null ) //&& prevGameObject2!= null)
        {
            prevGameObject1.SetActive(true);
            //prevGameObject2.SetActive(true);
        }

        // Deactivate the GameObject or script for this scene
        if (thisGameObject1 != null) //&& thisGameObject2 != null)
        {
            thisGameObject1.SetActive(false);
            //thisGameObject2.SetActive(false);
        }

        // Load the previous scene
        SceneManager.LoadScene("MaracasBeach");
    }

    // Switch to the next scene
    public void SwitchToNextScene()
    {
        // Deactivate the GameObject or script from the previous scene
        if (prevGameObject1 != null) // && prevGameObject2!= null)
        {
            prevGameObject1.SetActive(false);
            //prevGameObject2.SetActive(false);
        }

        // Deactivate the GameObject or script for this scene
        if (thisGameObject1 != null) //&& thisGameObject2 != null)
        {
            thisGameObject1.SetActive(false);
            //thisGameObject2.SetActive(false);
        }

        // Load the next scene
        SceneManager.LoadScene("FishingGame");
    }
}