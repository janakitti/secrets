using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCubeClick : MonoBehaviour
{

    public MenuCameraAnimation camera;
    private void Start()
    {

    }
    void OnMouseDown()
    {
        camera.anim.SetTrigger("ClickCube");
    }
}
