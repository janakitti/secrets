using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public bool isTutorial = false;

    public int l;
    public int w;
    public PlayerContoller player;
    public GridObject playerObject;
    public SecretController secret;
    public GridObject secretObject;
    public float moveSpeed = 8f;
    public LevelManager levelManager;

    public Material lockedMaterial;
    public Material unlockedMaterial;

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
                AdvanceMovers();
                ExecStep();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Vector3 nextPos = player.transform.position;
                nextPos.x -= 1f;
                PushRecurse(playerObject, nextPos);
                AdvanceMovers();
                ExecStep();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Vector3 nextPos = player.transform.position;
                nextPos.z += 1f;
                PushRecurse(playerObject, nextPos);
                AdvanceMovers();
                ExecStep();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Vector3 nextPos = player.transform.position;
                nextPos.z -= 1f;
                PushRecurse(playerObject, nextPos);
                AdvanceMovers();
                ExecStep();
            } else if (Input.GetKeyDown(KeyCode.R))
            {
                levelManager.Restart();
            }
        }

 
        CheckState();

        
    }

    private void CheckState()
    {
        if (!isTutorial)
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
            BlockUnlocked();
        } else
        {
            if (TutorialEnded(playerObject.GetPos()))
            {
                levelManager.LoadLevelSelect();
            } else if (SecretRevealed(secretObject.GetPos()))
            {
                levelManager.TutorialWallRed();
            } else
            {
                levelManager.TutorialWallNormal();
            }
            BlockUnlocked();
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
        if (gridTable.ContainsKey(testDirection) && (gridTable[testDirection].GetMovable() is NosyController || gridTable[testDirection].GetMovable() is MoverContoller))
        {
            return true;
        }
        testDirection = secretPos + new Vector3(-1f, 0f, 0f);
        if (gridTable.ContainsKey(testDirection) && (gridTable[testDirection].GetMovable() is NosyController || gridTable[testDirection].GetMovable() is MoverContoller))
        {
            return true;
        }
        testDirection = secretPos + new Vector3(0f, 0f, 1f);
        if (gridTable.ContainsKey(testDirection) && (gridTable[testDirection].GetMovable() is NosyController || gridTable[testDirection].GetMovable() is MoverContoller))
        {
            return true;
        }
        testDirection = secretPos + new Vector3(0f, 0f, -1f);
        if (gridTable.ContainsKey(testDirection) && (gridTable[testDirection].GetMovable() is NosyController || gridTable[testDirection].GetMovable() is MoverContoller))
        {
            return true;
        }
        return false;
    }

    private bool TutorialEnded(Vector3 playerPos)
    {
        Vector3 testDirection = playerPos + new Vector3(1f, 0f, 0f);
        if (gridTable.ContainsKey(testDirection) && gridTable[testDirection].GetMovable() is TutorialEnd)
        {
            return true;
        }
        testDirection = playerPos + new Vector3(-1f, 0f, 0f);
        if (gridTable.ContainsKey(testDirection) && gridTable[testDirection].GetMovable() is TutorialEnd)
        {
            return true;
        }
        testDirection = playerPos + new Vector3(0f, 0f, 1f);
        if (gridTable.ContainsKey(testDirection) && gridTable[testDirection].GetMovable() is TutorialEnd)
        {
            return true;
        }
        testDirection = playerPos + new Vector3(0f, 0f, -1f);
        if (gridTable.ContainsKey(testDirection) && gridTable[testDirection].GetMovable() is TutorialEnd)
        {
            return true;
        }
        return false;
    }

    private void BlockUnlocked()
    {
        foreach (KeyValuePair<Vector3, GridObject> gridObj in gridTable)
        {
            if (gridObj.Value.GetMovable() is LockedController)
            {
                Vector3 secretPos = secretObject.GetPos();
                Vector3 lockedPos = gridObj.Value.GetPos();
                Vector3 north = lockedPos + new Vector3(1f, 0f, 0f);
                Vector3 south = lockedPos + new Vector3(-1f, 0f, 0f);
                Vector3 east = lockedPos + new Vector3(0f, 0f, 1f);
                Vector3 west = lockedPos + new Vector3(0f, 0f, -1f);

                if (secretPos == north || secretPos == south || secretPos == east || secretPos == west)
                {
                    gridObj.Value.GetMovable().GetComponent<MeshRenderer>().material = unlockedMaterial;
                }
                else
                {
                    gridObj.Value.GetMovable().GetComponent<MeshRenderer>().material = lockedMaterial;
                }
            }
        }
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
            if (curObj.GetMovable() is LockedController)
            {
                Vector3 prevPos = (curObj.GetPos() - nextPos) + curObj.GetPos();
                if (secretObject.GetPos() == prevPos)
                {
                    Debug.Log("Y");
                    curObj.SetStep(nextPos);
                    return true;
                }
                else
                {
                    Debug.Log("N");
                    return false;
                }
            }
            if (PushRecurse(gridTable[nextPos], nextNextPos))
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
            } else if (!isTutorial && !isWithinBoundary(nextPos))
            {
                return false;
            } else if (curObj.GetMovable() is LockedController) {
                Vector3 prevPos = (curObj.GetPos() - nextPos) + curObj.GetPos();
                if (secretObject.GetPos() == prevPos)
                {
                    Debug.Log("Y");
                    curObj.SetStep(nextPos);
                    return true;
                } else
                {
                    Debug.Log("N");
                    return false;
                }
            } else
            {
                curObj.SetStep(nextPos);
                return true;
            }
        }
    }

    private void AdvanceMovers()
    {
        Dictionary<Vector3, GridObject> tempGridTable = new Dictionary<Vector3, GridObject>(gridTable);
        foreach (KeyValuePair<Vector3, GridObject> gridObj in tempGridTable)
        {
            if(gridObj.Value.GetMovable() is MoverContoller)
            {
                GridObject mover = gridObj.Value;
                if (mover.GetMovable().transform.rotation == Quaternion.Euler(90f, 0f, 0f) && mover.GetPos().x == secretObject.GetPos().x)
                {
                    Vector3 nextPos = Vector3.Normalize(secretObject.GetPos() - mover.GetPos()) + gridObj.Value.GetPos();
                    PushRecurse(mover, nextPos);
                } else if (mover.GetMovable().transform.rotation == Quaternion.Euler(90f, 90f, 0f) && mover.GetPos().z == secretObject.GetPos().z)
                {
                    Vector3 nextPos = Vector3.Normalize(secretObject.GetPos() - mover.GetPos()) + gridObj.Value.GetPos();
                    PushRecurse(mover, nextPos);
                }
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