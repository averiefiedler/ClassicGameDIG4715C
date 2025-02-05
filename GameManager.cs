using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject PlayerMovement;
    public GameObject BasicEnemy;
    public GameObject gameOverUI;
    public int destroyedEnemies = 0;
    public int enemiesToDestroy = 10;
    public int currentLevel = 1;

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("StartScreen");
        Debug.Log("Start Screen has loaded.");
    }
    public void LevelOne()
    {
        SceneManager.LoadScene("LevelOne");
        Debug.Log("LevelOne has loaded");
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
        Debug.Log("LevelTwo has loaded");
    }

    private bool isAlive;

    //Start of Alyssa updated Game Over 11:26pm 2/4/2025
    public int score = 0;
    public bool isGameOver = false;

    public void EarnScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            //UI needs to be updated for score display
            Debug.Log("Score: " + score);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        //Show game over screen
        Debug.Log("Game Over!");
    }
    //End of Alyssa updated Game Over 11:26pm 2/4/2025

    public void EnemyDestroyed()
    {
        destroyedEnemies++;
        CheckLevelTransition();
    }

    void Update()
    {
        // Here just in case wanting to change code to have health points
    }

    void CheckLevelTransition()
    {
        if (destroyedEnemies >= enemiesToDestroy)
        {
            currentLevel = 2;
            LoadLevel2();
        }
    }

    void LoadLevel2()
    {
        Debug.Log("Transitioning to Level 2);
        SceneManager.LoadScene("Level2");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
            Debug.Log("Game has quit.");
        }
    }
}
