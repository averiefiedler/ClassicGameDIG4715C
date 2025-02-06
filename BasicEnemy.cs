using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float movementSpeed = 3f; // Speed at which the enemy moves
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float shootInterval = 2f; // Time between shooting projectiles
    public Vector2 targetPosition; // Formation for them to come in
    public float entrySpeed = 2f; // Speed when entering screen
    public float swoopSpeed = 4f; // Speed when swooping down
    public int scoreValue = 10;

    private float nextShootTime; // Time to shoot next projectile
    private Vector2 startPosition; // Start position off-screen
    private bool isInFormation = false;
    private bool isSwooping = false;

    private GameManager gameManager; // Reference to the GameManager

    private void Start()
    {
        startPosition = new Vector2(Random.Range(-10f, 10f), 10f);
        transform.position = startPosition;

        nextShootTime = Time.time + shootInterval; // Schedule the first shot
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); // Cache GameManager reference
        
    }

    private void Update()
    {
        if (!isInFormation)
        {
            MoveOntoScreen();
        }
        else if (!isSwooping)
        {
            MoveToFormation();
        }
        else
        {
            SwoopDown();
        }

        if (Time.time >= nextShootTime)
        {
            ShootProjectile();
            nextShootTime = Time.time + shootInterval; // Reset shoot timer
        }
    }

    private void MoveOntoScreen()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, entrySpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            isInFormation = true;
        }
    }

    private void MoveToFormation()
    {
        // Placeholder for more complex movement towards formation if needed in future
    }

    private void SwoopDown()
    {
        Vector2 playerPosition = GameObject.FindWithTag("Player").transform.position;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, playerPosition.y - 1f), swoopSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, new Vector2(transform.position.x, playerPosition.y - 1f)) < 0.1f)
        {
            isSwooping = false;
        }
    }

    private void ShootProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    // Fixed OnTriggerEnter2D to handle collisions properly
    public void OnTriggerEnter2D(Collider2D other)
    {
        // If the enemy collides with the player
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.LoseALife(); // Call the LoseALife method of the player
            }
            Destroy(gameObject); // Destroy the enemy
        }

        // If the enemy collides with a player's projectile (Weapon)
        else if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject); // Destroy the player's projectile
            Destroy(gameObject); // Destroy the enemy

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.UpdateScore(scoreValue);
            }

   
        }
    }

    // LoseALife method for the enemy (optional here if you want specific behavior)
    public void LoseALife()
    {
        Destroy(gameObject); // Destroy the enemy when it "loses a life"
    }
}
