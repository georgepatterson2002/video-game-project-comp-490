using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPromptTriggerMiniGame : MonoBehaviour
{
    public GameObject startPromptCanvas; 
    private bool isPlayerInArea = false; 

    private void Start()
    {

        if (startPromptCanvas != null)
        {
            startPromptCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        // When player is in area and presses 'E'
        if (isPlayerInArea && Input.GetKeyDown(KeyCode.E))
        {
            // Enter code to start the game
            Debug.Log("Game Starting...");
            SceneManager.LoadScene("MiniGameOnePond");

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = true;
            if (startPromptCanvas != null)
            {
                startPromptCanvas.SetActive(true); // Show "Press E" prompt
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player has exited the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;
            if (startPromptCanvas != null)
            {
                startPromptCanvas.SetActive(false); // Hide "Press E" prompt
            }
        }
    }
}