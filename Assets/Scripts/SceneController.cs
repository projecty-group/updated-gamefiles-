using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{
    private void Start()
    {
        // Update the input module when switching scenes
        EventSystem.current = FindObjectOfType<EventSystem>(); //this ensures that the current scene uses it's own event system

        Cursor.lockState = CursorLockMode.None; //ensure that the mouse is not locked to the screen.
        Cursor.visible = true; //ensures that the mouse is visible on the screen
    }
}