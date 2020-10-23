using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableController : MonoBehaviour
{
    public StepController stepController;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Push(Vector3 dir)
    {
        stepController.SetNextLocation(dir);
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    stepController.SetNextLocation(transform.TransformPoint(Vector3.forward));
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    stepController.SetNextLocation(transform.TransformPoint(Vector3.back));
        //}
        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    stepController.SetNextLocation(transform.TransformPoint(Vector3.left));
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    stepController.SetNextLocation(transform.TransformPoint(Vector3.right));
        //}
    }
}
