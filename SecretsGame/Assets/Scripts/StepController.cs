using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour
{
    public float speed = 4f;
    public bool isDoneMoving;

    private Vector3 nextLocation;


    void Start()
    {
        SetNextLocation(gameObject.transform.position);
        isDoneMoving = true;
    }

    void Update()
    {
        if (gameObject.transform.position != nextLocation)
        {
            isDoneMoving = false;
            SlideStep();
        } else
        {
            isDoneMoving = true;
        }
    }

    void SlideStep()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextLocation, speed * Time.deltaTime);
    }

    public void SetNextLocation(Vector3 location)
    {
        nextLocation = location;
        foreach(MovableController obj in FindObjectsOfType<MovableController>())
        {
            if (obj.transform.position == nextLocation)
            {
                Vector3 dir = (nextLocation - gameObject.transform.position) + obj.transform.position;
                obj.Push(dir);
            }
        }
    }
}
