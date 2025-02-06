using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject PlayerMovement;
    public GameObject BasicEnemy;
    public int destroyedEnemies = 0;
    public int enemiesToDestroy = 10;
    public int currentLevel = 1;
    public static int lives = 3;
    public bool isVictory = false;
    public bool isCredits = false;
    public bool isStartScreen = true;
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
        if (isGameOver) return;
        {
            SceneManager.LoadScene("GameOver");
        }
        //Show game over screen
        Debug.Log("Game Over!");
    }

    public void Credits()
    {
        if (isCredits) return;
        {
            SceneManager.LoadScene("Credits");

        }
        //Show credits screen
        Debug.Log("Credits Scene");
    }

    public void StartScreen()
    {
        if (isStartScreen) return;
        {
            SceneManager.LoadScene("StartScreen");
        }
        //Show title screen
        Debug.Log("Start Scene");
    }

    public void Victory()
    {
        if (isVictory) return;
        {
            SceneManager.LoadScene("Victory");
        }
        Debug.Log("Victory!");
        //End of Alyssa updated Game Over 11:26pm 2/4/2025

        void RestartGame()
        {
            SceneManager.LoadScene("LevelOne");
        }

        void EnemyDestroyed()
        {
            destroyedEnemies++;
            CheckLevelTransition();
        }

        void CheckLevelTransition()
        {
            if (destroyedEnemies >= enemiesToDestroy && currentLevel == 1)
            {
                currentLevel = 2;
                LoadLevel2();
            }
        }

        void LoadLevel2()
        {
            Debug.Log("Transitioning to Level Two");

            SceneManager.LoadScene("LevelTwo");
            destroyedEnemies = 0;
            enemiesToDestroy = 1;

            if (destroyedEnemies >= enemiesToDestroy && currentLevel == 2)
            {
                isVictory = true;
            }
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
}