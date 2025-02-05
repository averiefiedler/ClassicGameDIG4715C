using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Player; // Reference to the player's Rigidbody2D
    private float horizontal;
    private float vertical;
    public float Speed = 350f;

    private Vector3 movement;

    // Screen bounds for restricting movement (calculated in Start)
    private float minY, maxY, minX, maxX;

    public GameObject gameOver; // Reference to the Game Over UI panel
    public int lives; // Reference to lives int in GameManager

    // Start is called before the first frame update
    void Start()
    {

        Player.freezeRotation = true;

        // Get the camera's main properties
        Camera mainCamera = Camera.main;

        // Calculate the screen bounds for the 1/3 vertical region
        float screenHeight = mainCamera.orthographicSize * 2f; // Total height of the camera view
        minY = mainCamera.transform.position.y - mainCamera.orthographicSize + (screenHeight / 4); // Bottom third
        maxY = mainCamera.transform.position.y - mainCamera.orthographicSize + (2 * screenHeight / 4); // Top third

        // Calculate the screen width bounds
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x; // Left edge of the screen
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x; // Right edge of the screen
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player for movement (only for horizontal and vertical directions)
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Create movement vector based on the input
        movement = new Vector3(horizontal, vertical, 0).normalized;

        // Apply movement to player velocity
        Player.velocity = new Vector2(movement.x * Speed, movement.y * Speed);

        // Restrict the player's position to stay within the bounds
        Vector3 currentPosition = transform.position;

        // Clamp Y position to stay within the defined bounds (1/3 of the screen height)
        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

        // Clamp X position to stay within the screen bounds (left and right edges)
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);

        // Apply the clamped position
        transform.position = currentPosition;
    }

    public void LoseALife()
    {
        Destroy(gameObject);
        lives--;
    }

    // Detect collision with a projectile
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            // Destroy the player upon collision
            other.GetComponent<PlayerMovement>().LoseALife();

            Destroy(other.gameObject);
            LoseALife();

            if (lives == 0)
            {
                // Trigger Game Over UI and level reload
                SceneManager.LoadScene("GameOver");
            }

        }
    }

  
}
