using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeIconHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite;
    public Sprite hoverSprite;

    private Image imageComponent;

    void Awake()
    {
        // Get the Image component attached to this button
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("No Image component found on this GameObject.");
        }
    }

    // Called when the mouse enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (imageComponent != null && hoverSprite != null)
        {
            imageComponent.sprite = hoverSprite;
        }
    }

    // Called when the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        if (imageComponent != null && normalSprite != null)
        {
            imageComponent.sprite = normalSprite;
        }
    }

    // ---------------- Button Action Methods ----------------

    /// Loads the first scene.
    public void NewGame()
    {
        // Load the first scene (index 1).
        SceneManager.LoadScene(1);
    }

    /// No Functionality Yet
    public void Continue()
    {
        Debug.Log("Continue button pressed. (Functionality not implemented yet)");
    }

    /// No Functionality Yet
    public void Settings()
    {
        Debug.Log("Settings button pressed. (Functionality not implemented yet)");
    }

    /// Quits the game.
    /// Will only work in a built application, not in the Unity editor.
    public void Quit()
    {
        Debug.Log("Quit button pressed. Exiting game.");
        Application.Quit();
    }
}
