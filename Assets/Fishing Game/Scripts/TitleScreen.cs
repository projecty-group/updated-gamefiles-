using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void PlayGame(){

        SceneManager.LoadScene("FishingGame"); //Goes to the fishing game screen to start the fishing game
    }

    public void QuitGame(){

        SceneManager.LoadScene("MaracasBeach"); //Goes to the Maracas Beach terrain when the player presses the back button
    }
}
