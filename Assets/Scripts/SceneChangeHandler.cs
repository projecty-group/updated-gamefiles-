using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    public GameObject playerInputHandler;
    public GameObject MainCamera;

    private void OnDisable()
    {
        // Disable the player input handler when the scene changes
        playerInputHandler.SetActive(false);
        MainCamera.SetActive(false);
    }

    private void OnEnable()
    {
        // Enable the player input handler when the scene changes back
        playerInputHandler.SetActive(true);
        MainCamera.SetActive(true);
    }
}