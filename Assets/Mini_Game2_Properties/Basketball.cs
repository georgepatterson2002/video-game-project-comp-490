using UnityEngine;

public class Basketball : MonoBehaviour
{
    public float maxForce = 20f;

    private Rigidbody2D rb;
    private Vector2 dragStart;
    private bool isDragging = false;
    private bool hasShot = false;
    private BasketballShoot spawner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        spawner = FindObjectOfType<BasketballShoot>();
    }

    void Update()
    {
        if (hasShot) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.gameObject == gameObject)
            {
                isDragging = true;
                dragStart = mousePos;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentMousePos;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            hasShot = true;
            rb.isKinematic = false;

            Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 force = (dragStart - dragEnd).normalized * Mathf.Min(Vector2.Distance(dragStart, dragEnd), 5f) * maxForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            spawner.NotifyBallDestroyed();
            Destroy(gameObject);
        }
    }
}
