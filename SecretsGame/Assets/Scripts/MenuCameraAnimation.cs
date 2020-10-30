using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCameraAnimation : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void LoadLevelSelect()
    {
        SceneManager.LoadScene(1);
    }
}
