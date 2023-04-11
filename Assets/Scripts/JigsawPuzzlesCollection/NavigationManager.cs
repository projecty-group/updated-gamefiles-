using UnityEngine;
using UnityEngine.SceneManagement;

namespace JigsawPuzzlesCollection.Scripts
{
    public class NavigationManager : MonoBehaviour
    {
        public void GoToTerrain()
        {
            SceneManager.LoadScene("MaracasBeach");
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

    }
}