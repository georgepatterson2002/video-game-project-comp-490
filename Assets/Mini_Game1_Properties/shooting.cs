using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera mainCamera;
    public GameScore gameScore;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            //Debug.Log("Click Detected");

            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // Raycast to detect what the mouse is pointing at


            if (hit.collider != null)
            {
                   
                if (hit.collider.CompareTag("Fish"))
                {
                    gameScore.AddScore(10); 
                    Destroy(hit.collider.gameObject); 
                }
                if (hit.collider.CompareTag("BadFish"))
                {
                    Destroy(hit.collider.gameObject); 
                }
            }
        }
    }
}
