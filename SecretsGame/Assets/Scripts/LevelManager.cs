using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public GameObject completeLevelUI;

    private bool gameHasEnded = false;

    public void LevelComplete()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            completeLevelUI.SetActive(true);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
