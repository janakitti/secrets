using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public GameObject completeLevelUI;
    public GameObject failLevelUI;

    public GameObject floor;
    public Material completeFloorMaterial;
    public Material failFloorMaterial;
    public Material normalFloorMaterial;

    private bool gameHasEnded = false;

    public void LevelComplete()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            completeLevelUI.SetActive(true);
            floor.GetComponent<MeshRenderer>().material = completeFloorMaterial;
        }
    }

    public void LevelFailed()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            failLevelUI.SetActive(true);
            floor.GetComponent<MeshRenderer>().material = failFloorMaterial;
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelNumber(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void TutorialWallRed()
    {
        GameObject.FindGameObjectWithTag("WallWest").GetComponent<MeshRenderer>().material = failFloorMaterial;
    }

    public void TutorialWallNormal()
    {
        GameObject.FindGameObjectWithTag("WallWest").GetComponent<MeshRenderer>().material = normalFloorMaterial;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
