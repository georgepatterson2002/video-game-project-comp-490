using UnityEngine;

public class BasketballShooter : MonoBehaviour
{
    public GameObject basketballPrefab;   // Prefab reference
    public Transform shootPoint;          // Where the ball spawns (player position)
    public float maxPower = 20f;          // Max launch force
    public float powerChargeSpeed = 10f;  // Speed at which power increases

    private float currentPower = 0f;
    private GameObject currentBall;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBasketball();
        }

        if (Input.GetMouseButton(0))
        {
            if (currentBall != null)
            {
                currentPower += powerChargeSpeed * Time.deltaTime;
                currentPower = Mathf.Clamp(currentPower, 0, maxPower);

                Debug.Log("Charging Power: " + currentPower);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentBall != null)
            {
                LaunchBasketball();
            }
        }
    }

    void SpawnBasketball()
    {
        currentBall = Instantiate(basketballPrefab, shootPoint.position, Quaternion.identity);
        currentPower = 0f;
    }

   void LaunchBasketball()
{
    if (currentBall == null)
    {
        Debug.LogError("Current ball is null!");
        return;
    }

    // Get mouse position in world space
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Vector3 shootDirection;

    // If the raycast hits something, calculate the direction to the hit point
    if (Physics.Raycast(ray, out RaycastHit hit))
    {
        shootDirection = (hit.point - shootPoint.position).normalized;
        Debug.Log("Raycast hit: " + hit.point);  // Log raycast hit point
    }
    else
    {
        // If the ray does not hit anything, shoot in a default direction (forward)
        shootDirection = shootPoint.forward;
        Debug.Log("No hit, shooting forward: " + shootDirection);
    }

    // Debugging: Draw a ray in the Scene view to visualize the shooting direction
    Debug.DrawRay(shootPoint.position, shootDirection * 10f, Color.red, 2f);

    // Get the Rigidbody component of the ball and apply the force to launch it
    Rigidbody rb = currentBall.GetComponent<Rigidbody>();
    rb.AddForce(shootDirection * currentPower, ForceMode.Impulse);

    // Log power and direction for debugging
    Debug.Log("Shot with Power: " + currentPower + ", Direction: " + shootDirection);

    // Reset the ball reference after the shot
    currentBall = null;
}

}
