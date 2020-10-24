using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.zero;

    private Vector3 nextPos;
    public StepController stepController;
    void Start()
    {
        nextPos = transform.position;
    }

    void Update()
    {
        //if (stepController.isDoneMoving)
        //{
        //    if (Input.GetKeyDown(KeyCode.W))
        //    {
        //        stepController.SetNextLocation(transform.TransformPoint(Vector3.forward));
        //    } else if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        stepController.SetNextLocation(transform.TransformPoint(Vector3.back));
        //    } else if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        stepController.SetNextLocation(transform.TransformPoint(Vector3.left));
        //    } else if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        stepController.SetNextLocation(transform.TransformPoint(Vector3.right));
        //    }
        //}


    }
    void Step(string dir)
    {
        //if (dir == "North")
        //{
        //    stepController.SetNextLocation(transform.TransformPoint(Vector3.forward));
        //    //transform.translate(1f, 0f, 0f);
        //}
        
    }
}



