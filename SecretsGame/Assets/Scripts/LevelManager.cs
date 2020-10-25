using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public GameObject completeLevelUI;
    public GameObject failLevelUI;

    private bool gameHasEnded = false;

    public void LevelComplete()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            completeLevelUI.SetActive(true);
        }
    }

    public void LevelFailed()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            failLevelUI.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
