using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static int myActualScore = 0;   // Track the player's score
    public int nextLevelScore = 100;        // Score needed to move to the next level
    public string level = "LevelTwo";       // The next level to load
    public int scoreValue = 10;             // Each enemy gives 10 points when killed

    // Called when an enemy enters the trigger area (collision)
    public void OnTriggerEnter(Collider other)
    {
        // If the object that collided has the tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Add 10 points to the player's score
            myActualScore += scoreValue;

            // Print the updated score for debugging
            Debug.Log("Enemy killed! Current score: " + myActualScore);

            // Optionally, destroy the enemy after it's killed
            Destroy(other.gameObject);  // This will destroy the enemy object when it is killed
        }
    }

    // This method checks if the score is high enough to load the next level
    public void Update()
    {
        // If the player's score is greater than or equal to the required score for the next level
        if (myActualScore >= nextLevelScore)
        {
            // Load the next level (change this to the actual level you want to load)
            SceneManager.LoadScene(level);
            Debug.Log("Level completed! Loading " + level);
        }
    }

    // This method can be used if you need to manually add score (for example, from another script)
    public void UpdateScore(int scoreToAdd)
    {
        myActualScore += scoreToAdd;
        Debug.Log($"Current score updated: {myActualScore}");
    }
}
