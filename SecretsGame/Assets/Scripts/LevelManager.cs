using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool gameHasEnded = false;
    public void LevelComplete()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("COMPLETE");
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
