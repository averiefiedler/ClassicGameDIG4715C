using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject PlayerMovement;
    public GameObject BasicEnemy;
    public GameObject gameOverUI;
    
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

 
    private int score;

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
