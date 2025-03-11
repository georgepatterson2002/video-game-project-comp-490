using UnityEngine;
using UnityEngine.EventSystems;
using UVector2 = UnityEngine.Vector2;

public class SliderScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Reference to the slider's RectTransform (this GameObject)
    public RectTransform sliderRect;
    // Reference to the bar's RectTransform (parent container)
    public RectTransform barRect;

    // Optional: Reference to the AudioSource to adjust volume (if you add music later)
    public AudioSource audioSource;

    // Variables to store pointer offset data
    private UVector2 originalLocalPointerPosition;
    private UVector2 originalSliderLocalPosition;

    private void Start()
    {
        // If not set, try to get the RectTransform automatically
        if (sliderRect == null)
            sliderRect = GetComponent<RectTransform>();
    }

    // Called when the user starts dragging the slider
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Convert the pointer position to the local position in the bar
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            barRect, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
        // Save the slider's original local position
        originalSliderLocalPosition = sliderRect.localPosition;
    }

    // Called while the user drags the slider
    public void OnDrag(PointerEventData eventData)
    {
        if (sliderRect == null || barRect == null)
            return;

        UVector2 localPointerPosition;
        // Convert the screen pointer position into the bar's local space
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            barRect, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            // Calculate the offset from the original pointer position
            UVector2 offset = localPointerPosition - originalLocalPointerPosition;
            // Determine the new position of the slider
            UVector2 newLocalPosition = originalSliderLocalPosition + offset;

            // Get half the height of the bar to determine limits
            float halfHeight = barRect.rect.width / 2f;
            // Clamp the slider's vertical position so it stays within the bar
            newLocalPosition.x = Mathf.Clamp(newLocalPosition.x, -halfHeight, halfHeight);
            // Keep the slider's horizontal position unchanged
            newLocalPosition.y = originalSliderLocalPosition.y;

            // Update the slider's position
            sliderRect.localPosition = newLocalPosition;

            // Convert slider position to a normalized value (0 at bottom, 1 at top)
            float normalizedValue = (newLocalPosition.y + halfHeight) / barRect.rect.height;

            // Use the normalized value to adjust music volume (if audioSource is set)
            if (audioSource != null)
                audioSource.volume = normalizedValue;
        }
    }

    // Called when the user ends dragging the slider
    public void OnEndDrag(PointerEventData eventData)
    {
        // Optional: implement any logic needed when the drag ends.
    }
}
