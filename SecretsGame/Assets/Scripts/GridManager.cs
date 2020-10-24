using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    private List<GridObject> movableList;

    void Start()
    {
        movableList = new List<GridObject>();
        foreach (MovableController obj in FindObjectsOfType<MovableController>())
        {
            movableList.Add(new GridObject(obj, obj.transform.position));
        }

        Debug.Log("LIST: " + movableList);
          
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Step();
            ExecStep();
      
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
  
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
       
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
    }

    private void Step()
    {
        foreach (GridObject obj in movableList)
        {
            obj.StepNorth();
        }
    }

    private void ExecStep()
    {
        foreach (GridObject obj in movableList)
        {
            obj.ExecNorth();
        }
    }
}

public class GridObject
{
    private MovableController movable;
    private Vector3 pos;

    public GridObject(MovableController movable, Vector3 pos)
    {
        this.movable = movable;
        this.pos = pos;
    }
    public void StepNorth()
    {
        pos.x += 1f;
    }
    public void ExecNorth()
    {
        movable.Push(pos);
    }
}