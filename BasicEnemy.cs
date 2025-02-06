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

    private float nextShootTime; // Time to shoot next projectile
    private Vector2 startPosition; // Start position off-screen
    private bool isInFormation = false;
    private bool isSwooping = false;

    private GameController gameController; // Reference to the GameController

    private GameManager gameManager; // Reference to the GameManager

    private void Start()
    {
        startPosition = new Vector2(Random.Range(-10f, 10f), 10f);
        transform.position = startPosition;

        nextShootTime = Time.time + shootInterval; // Schedule the first shot
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); // Cache GameManager reference
        gameController = GameObject.Find("GameController").GetComponent<GameController>(); // Cache GameController reference
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

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.CompareTag("Player"))
        {
            // Lose life for the player
            whatIHit.GetComponent<PlayerMovement>().LoseALife(); // Fixed the typo here
            Destroy(gameObject); // Destroy the enemy
        }
        else if (whatIHit.CompareTag("Weapon"))
        {
            // Handle scoring and enemy destruction
            gameManager.EarnScore(5);
            Destroy(whatIHit.gameObject); // Destroy the weapon projectile
            Destroy(gameObject); // Destroy the enemy
        }

        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void LoseALife()
    {
        Destroy(gameObject); // Destroy the enemy
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
