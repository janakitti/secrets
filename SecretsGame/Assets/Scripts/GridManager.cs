using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int l;
    public int w;
    public PlayerContoller player;
    public GridObject playerObject;
    public SecretController secret;
    public GridObject secretObject;

    public LevelManager levelManager;

    private List<GridObject> movableList;
    public static Dictionary<Vector3, GridObject> gridTable;
    private List<StepController> steppingList;
    private enum State {Win, Lose, InGame}
    private State state;
    void Start()
    {
        state = State.InGame;
        movableList = new List<GridObject>();
        steppingList = new List<StepController>();

        gridTable = new Dictionary<Vector3, GridObject>();

        foreach (MovableController obj in FindObjectsOfType<MovableController>())
        {
            if (obj is PlayerContoller)
            {
                playerObject = new GridObject(player, player.transform.position);
            } else if (obj is SecretController)
            {
                secretObject = new GridObject(secret, secret.transform.position);
            } else
            {
                movableList.Add(new GridObject(obj, obj.transform.position));
                gridTable.Add(obj.transform.position, new GridObject(obj, obj.transform.position));
            }

        }
        gridTable.Add(player.transform.position, playerObject);
        gridTable.Add(secret.transform.position, secretObject);


        Debug.Log("LIST: " + movableList);
        PrintDebug();

    }

    void Update()
    {
        if(steppingList.Count == 0)
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
                Vector3 nextPos = player.transform.position;
                nextPos.x -= 1f;
                PushRecurse(playerObject, nextPos);
                ExecStep();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Vector3 nextPos = player.transform.position;
                nextPos.z += 1f;
                PushRecurse(playerObject, nextPos);
                ExecStep();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Vector3 nextPos = player.transform.position;
                nextPos.z -= 1f;
                PushRecurse(playerObject, nextPos);
                ExecStep();
            }
        }

        CheckState();
    }

    private void CheckState()
    {
        if (!isWithinBoundary(secretObject.GetPos()))
        {
            state = State.Win;
            ActOnState();
            return;
        }
        if (SecretRevealed(secretObject.GetPos()))
        {
            state = State.Lose;
            ActOnState();
            return;
        }
    }

    private void ActOnState()
    {
        if (state == State.Win)
        {
            levelManager.LevelComplete();
        } else if (state == State.Lose)
        {
            levelManager.LevelFailed();
        }
    }

    private bool SecretRevealed(Vector3 secretPos)
    {
        Vector3 testDirection = secretPos + new Vector3(1f, 0f, 0f);
        if (gridTable.ContainsKey(testDirection) && gridTable[testDirection].GetMovable() is NosyController)
        {
            return true;
        }
        testDirection = secretPos + new Vector3(-1f, 0f, 0f);
        if (gridTable.ContainsKey(testDirection) && gridTable[testDirection].GetMovable() is NosyController)
        {
            return true;
        }
        testDirection = secretPos + new Vector3(0f, 0f, 1f);
        if (gridTable.ContainsKey(testDirection) && gridTable[testDirection].GetMovable() is NosyController)
        {
            return true;
        }
        testDirection = secretPos + new Vector3(0f, 0f, -1f);
        if (gridTable.ContainsKey(testDirection) && gridTable[testDirection].GetMovable() is NosyController)
        {
            return true;
        }
        return false;
    }

    private void PrintDebug()
    {
        Debug.Log("P: " + playerObject.GetPos());
        foreach (KeyValuePair<Vector3, GridObject> gridObj in gridTable)
        {
            Debug.Log("B: " + gridObj.Value.GetPos());
        }
        Debug.Log(l / 2 + 1);
        Debug.Log("====");
    }

    private bool PushRecurse(GridObject curObj, Vector3 nextPos)
    {
        if (gridTable.ContainsKey(nextPos))
        {
            Vector3 nextNextPos = nextPos - curObj.GetPos() + nextPos;
            if (PushRecurse(GridManager.gridTable[nextPos], nextNextPos))
            {
                curObj.SetStep(nextPos);
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            if (isOnBoundary(nextPos) && (curObj.GetMovable() is SecretController))
            {
                curObj.SetStep(nextPos);
                return true;
            } else if (!isWithinBoundary(nextPos))
            {
                return false;
            } else
            {
                curObj.SetStep(nextPos);
                return true;
            }
        }
    }

    private bool isWithinBoundary(Vector3 pos)
    {
        return Math.Abs(pos.x) <= l / 2 && Math.Abs(pos.z) <= w / 2;
    }

    private bool isOnBoundary(Vector3 pos)
    {
        return Math.Abs(pos.x) == ((l / 2) + 1) || Math.Abs(pos.z) == ((w / 2) + 1);
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
            steppingList.Add(gridObj.Value.GetStepController());
            gridObj.Value.ExecNorth();
        }
    }

    public void RemoveFromSteppingList(StepController completedStep)
    {
        if (steppingList.Contains(completedStep))
        {
            steppingList.Remove(completedStep);
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
    public void SetStep(Vector3 nextStep)
    {
        GridObject obj = GridManager.gridTable[pos];
        GridManager.gridTable.Remove(pos);
        pos = nextStep;
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

    public StepController GetStepController()
    {
        return movable.stepController;
    }
    public MovableController GetMovable()
    {
        return movable;
    }
}