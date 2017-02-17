using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    public Grid grid;
    public Canvas unitMenu;
    public int unitID;
    public int unitTurnSpeed;
    public int unitMovementSpeed;

    CanvasScript canvasScript;

    float speed = 0.05f;
    List<Node> path;
    int targetIndex;
    int layerMask;
    IEnumerator currentCoroutine;

    bool finishedFollowingPath;
    bool isMoving;
    bool moveCommandGiven;

    Node tempNode;
    List<Node> reachableNodes;

    Vector3 originalPos;

    // Use this for initialization
    void Start()
    {
        canvasScript = unitMenu.GetComponent<CanvasScript>();

        int mapLayer = 8;
        layerMask = 1 << mapLayer;
        unitID = GetInstanceID();

        unitTurnSpeed = 1;
        unitMovementSpeed = 3;
        isMoving = false;
        finishedFollowingPath = true;
        moveCommandGiven = false;
    }

    void Update()
    {
        unitMenu.transform.position = transform.position;
        if(!finishedFollowingPath)
        {
            CameraScript.LookAt(transform.position);
        }

        //if(moveCommandGiven)
        //{
        //    FinishedTurn();
        //}
    }

    void FixedUpdate()
    {
        if(isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, layerMask))
                {
                    if (hit.collider != null)
                    {
                        Node node = grid.NodeFromWorldPoint(hit.point);
                        if(reachableNodes.Contains(node))
                        {
                            PathFindingManager.RequestPath(transform.position, node.worldPosition, OnPathFound);
                            isMoving = false;
                        }
                        //Debug.DrawLine(new Vector3(-0.3f, 7.5f, -.5f), hit.point, Color.red, 100);
                        //Debug.DrawLine(new Vector3(-0.3f, 7.5f, -.5f), node.worldPosition, Color.blue, 100);
                    }
                }
            }

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, layerMask))
                {
                    if (hit.collider != null)
                    {
                        Node node = grid.NodeFromWorldPoint(hit.point);
                        if (reachableNodes.Contains(node))
                        {
                            PathFindingManager.RequestPath(transform.position, node.worldPosition, OnPathFound);
                            isMoving = false;
                        }
                        //Debug.DrawLine(new Vector3(-0.3f, 7.5f, -.5f), hit.point, Color.red, 100);
                        //Debug.DrawLine(new Vector3(-0.3f, 7.5f, -.5f), node.worldPosition, Color.blue, 100);
                    }
                }
            }
        }
    }

    public void DoTurn()
    {
        canvasScript.ShowCanvas();
        canvasScript.ShowMoveButton();
        originalPos = transform.position;
        //unitMenu.gameObject.SetActive(true);
    }

    public void Move()
    {
        //unitMenu.gameObject.SetActive(false);
        canvasScript.HideCanvas();
        isMoving = true;
        reachableNodes = PathFindingManager.RequestReachAbleNodes(transform.position, unitMovementSpeed);
        HighlightManager.MoveHighlight(reachableNodes);
        //Debug.Log(tempList.Count);
    }

    void PathFinished()
    {
        HighlightManager.DestroyMoveHighlight();
        targetIndex = 0;
        finishedFollowingPath = true;
        moveCommandGiven = true;

        canvasScript.ShowMoveCancel();
    }

    public void MoveCancel()
    {
        moveCommandGiven = false;
        transform.position = originalPos;
        CameraScript.LookAt(transform.position);
        canvasScript.ShowMoveButton();
    }

    public void FinishedTurn()
    {
        moveCommandGiven = false;
        canvasScript.HideCanvas();
        TurnManager.FinishedTurn();
    }
    
    public void StopFollowingPath()
    {
        StopCoroutine(currentCoroutine);
        targetIndex = 0;
    }

    void OnPathFound(List<Node> newPath, bool isSuccess)
    {
        if (isSuccess)
        {
            finishedFollowingPath = false;
            path = newPath;
            if (currentCoroutine == null)
            {
                currentCoroutine = FollowPath();
                StartCoroutine(currentCoroutine);
            }
            else
            {
                //PathFindingManager.ResetPathFinding();
                targetIndex = 0;
                StopCoroutine(currentCoroutine);
                currentCoroutine = FollowPath();
                StartCoroutine(currentCoroutine);
            }
            //StopCoroutine(FollowPath());
            //StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        Node currentWaypoint = path[0];

        while (true)
        {
            // Debug.Log("FOLLOW PATH: " + targetIndex + " MAX : " + path.Length);
            if (transform.position == currentWaypoint.surfacePosition)
            {
                //Debug.Log("INDEX: " + targetIndex);
                targetIndex++;
                if (targetIndex >= path.Count)
                {
                    PathFinished();
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            // Debug.Log("MOVE POS: " + transform.position + " : " + currentWaypoint);

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.surfacePosition, speed);
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        if (tempNode != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(tempNode.worldPosition, Vector3.one * (0.5f));
            Gizmos.color = Color.black;
            Gizmos.DrawCube(transform.position, Vector3.one * (0.5f));
        }

        if(path != null)
        {
            foreach(Node node in path)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(node.surfacePosition, Vector3.one * (0.1f));
            }
        }

        //if(reachableNodes != null)
        //{
        //    foreach(Node node in reachableNodes)
        //    {
        //        Gizmos.color = Color.blue;
        //        Gizmos.DrawCube(node.worldPosition, Vector3.one * (0.3f));
        //    }
        //}
    }
}
