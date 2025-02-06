using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static int myActualScore = 0;
    public int nextLevelScore = 100;
    public string level = "LevelTwo";
    public int scoreValue = 10;

    public void LevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
        Debug.Log("LevelTwo has loaded");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            myActualScore++;
        }
    }

    public void Update()
    {
        if(myActualScore >= nextLevelScore)
        {
            SceneManager.LoadScene("LevelTwo");
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        scoreValue += scoreToAdd;
        Debug.Log("Current score updated.", myActualScore);

    }
    
    

}
