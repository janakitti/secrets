using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour
{
    public bool isDoneMoving;
    public GridManager gridManager;

    private Vector3 nextLocation;


    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
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
            gridManager.RemoveFromSteppingList(this);
        }
    }

    void SlideStep()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextLocation, gridManager.moveSpeed * Time.deltaTime);
    }

    public void SetNextLocation(Vector3 location)
    {
        nextLocation = location;
        //foreach(MovableController obj in FindObjectsOfType<MovableController>())
        //{
        //    if (obj.transform.position == nextLocation)
        //    {
        //        Vector3 dir = (nextLocation - gameObject.transform.position) + obj.transform.position;
        //        obj.Push(dir);
        //    }
        //}
    }
}
