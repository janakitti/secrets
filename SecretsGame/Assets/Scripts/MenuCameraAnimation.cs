using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraAnimation : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
