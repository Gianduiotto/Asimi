using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Gianniscene"); // Load your core gameplay scene called "Gianniscene"
    }
}
