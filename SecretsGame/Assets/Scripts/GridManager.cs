﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public PlayerContoller player;
    public GridObject playerObject;
    private List<GridObject> movableList;
    public static Dictionary<Vector3, GridObject> gridTable;

    void Start()
    {
        movableList = new List<GridObject>();

        gridTable = new Dictionary<Vector3, GridObject>();

        foreach (MovableController obj in FindObjectsOfType<MovableController>())
        {
            if (obj is PlayerContoller)
            {
                playerObject = new GridObject(player, player.transform.position);
            } else
            {
                movableList.Add(new GridObject(obj, obj.transform.position));
                gridTable.Add(obj.transform.position, new GridObject(obj, obj.transform.position));
            }

        }
        gridTable.Add(player.transform.position, playerObject);
       

        Debug.Log("LIST: " + movableList);
        PrintDebug();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            Vector3 nextPos = player.transform.position;
            nextPos.x += 1f;
            PushRecurse(playerObject, nextPos);
            ExecStep();
            PrintDebug();
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

    private void PrintDebug()
    {
        Debug.Log("P: " + playerObject.GetPos());
        foreach (KeyValuePair<Vector3, GridObject> gridObj in gridTable)
        {
            Debug.Log("B: " + gridObj.Value.GetPos());
        }
        Debug.Log("====");
    }

    private bool PushRecurse(GridObject curObj, Vector3 nextPos)
    {
        if (GridManager.gridTable.ContainsKey(nextPos))
        {
            Vector3 nextNextPos = nextPos;
            nextNextPos.x += 1f;
            if (PushRecurse(GridManager.gridTable[nextPos], nextNextPos))
            {
                Step(curObj);

                return true;
            } else
            {
                Step(curObj);

                return false;
            }
        } else
        {
            Step(curObj);

            return false;
        }
    }

    private void Step(GridObject obj2)
    {
        obj2.StepNorth();
        //foreach (GridObject obj in movableList)
        //{
        //    obj.StepNorth();
        //}
    }

    private void ExecStep()
    {
        foreach (KeyValuePair<Vector3, GridObject> gridObj in gridTable)
        {
            gridObj.Value.ExecNorth();
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
        GridObject obj = GridManager.gridTable[pos];
        GridManager.gridTable.Remove(pos);
        pos.x += 1f;
        GridManager.gridTable.Add(pos, obj);
        
    }
    public void ExecNorth()
    {
        movable.Push(pos);
    }

    public Vector3 GetPos()
    {
        return pos;
    }
}