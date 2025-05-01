using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class pauseChangeIconHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Regular button sprites
    public Sprite normalSprite;
    public Sprite hoverSprite;

    // Toggle button sprite (the one that persists when toggled on)
    public Sprite toggledSprite;

    public bool isToggle = false;

    private Image imageComponent;

    // Tracks whether the toggle button is currently toggled on.
    private bool toggled = false;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("No Image component found on this GameObject.");
        }
    }

    // Called when the mouse enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // If the button is a toggle and is toggled on, don't change the sprite.
        if (isToggle && toggled)
            return;

        if (imageComponent != null && hoverSprite != null)
        {
            imageComponent.sprite = hoverSprite;
        }


    }

    // Called when the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        // If the button is a toggle and is toggled on, keep the toggled sprite.
        if (isToggle && toggled)
            return;

        if (imageComponent != null && normalSprite != null)
        {
            imageComponent.sprite = normalSprite;
        }
    }

    // Called when the button is clicked.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isToggle)
        {
            // Toggle the state
            toggled = !toggled;

            // Update the sprite based on the new toggle state.
            if (toggled)
            {
                if (toggledSprite != null)
                    imageComponent.sprite = toggledSprite;
            }
            else
            {
                if (normalSprite != null)
                    imageComponent.sprite = normalSprite;
            }
        }
    }

    
}
