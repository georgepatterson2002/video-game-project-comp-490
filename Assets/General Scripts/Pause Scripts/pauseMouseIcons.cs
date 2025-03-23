using UnityEngine;

public class pauseMouseIcons : MonoBehaviour
{
    public Texture2D defaultCursor;  // Assign in the Inspector
    public Vector2 hotspot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }
}
