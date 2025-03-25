using UnityEngine;
using UnityEngine.SceneManagement;

public class lostScript : MonoBehaviour
{
    void Start()
    {
        // Show and unlock the mouse cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Function to retry the game
    public void RetryGame()
    {
        SceneManager.LoadScene("Mini_Game1"); // Replace with your actual game scene name
    }

    // Function to go to the main menu (Add logic if needed)
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }

    // Function to go to the main map
    public void GoToMainMap()
    {
        SceneManager.LoadScene("MainMap");
    }
}
