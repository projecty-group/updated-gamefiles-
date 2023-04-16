using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TriggerNotifier : MonoBehaviour
{
    public KeyCode startKey;
    public GameObject PlayGamePopUp;
    public GameObject player; // Reference to the player GameObject
    public GameObject mainCamera; // Reference to the main camera GameObject
   


    private bool canStartMiniGame = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canStartMiniGame = true;
            //messageText.gameObject.SetActive(true);
            PlayGamePopUp.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canStartMiniGame = false;
            //messageText.gameObject.SetActive(false);
            PlayGamePopUp.SetActive(false);
        }
    }

    void Update()
    {
        if (canStartMiniGame && Input.GetKeyDown(startKey))
        {
            if (gameObject.tag == "BoardWalk"){
                PlayGamePopUp.SetActive(false);
                SceneManager.LoadScene("TitleScreen");
            }
            if (gameObject.tag == "Sign"){
                PlayGamePopUp.SetActive(false);
                SceneManager.LoadScene("Menu");
            }
            
        }
    }
   
   /*

    // When a new scene is loaded, update the references to the player and main camera
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Subscribe to the sceneLoaded event when the script is enabled
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unsubscribe from the sceneLoaded event when the script is disabled
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    */
}