using UnityEngine;
 using UnityEngine.SceneManagement;
  public class link1 : MonoBehaviour
   { 
    public string sceneName;
     // Name of the scene to change to
      private void OnMouseDown()
       {
         SceneManager.LoadScene(sceneName);
          // Load the scene with the given name 
          } 
          }