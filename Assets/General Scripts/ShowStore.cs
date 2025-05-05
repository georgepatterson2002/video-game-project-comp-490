using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStore : MonoBehaviour
{
    public GameObject storeMenu; // Assign your UI Panel in the inspector
    private bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isMenuOpen = !isMenuOpen;
            storeMenu.SetActive(isMenuOpen);
            if (isMenuOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f; // pause game
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f; // resume game
            }
        }
    }
}
