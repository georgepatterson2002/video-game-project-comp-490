using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D hoverCursor;  // Assign in Inspector
    public Texture2D defaultCursor;
    public Vector2 hotspot = Vector2.zero;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }
}
