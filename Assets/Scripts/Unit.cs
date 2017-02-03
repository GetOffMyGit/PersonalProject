using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    public Grid grid;
    public Canvas unitMenu;
    public int unitID;
    public int unitSpeed;

    float speed = 0.05f;
    List<Node> path;
    int targetIndex;
    int layerMask;
    IEnumerator currentCoroutine;

    Node tempNode;

    // Use this for initialization
    void Start()
    {
        int mapLayer = 8;
        layerMask = 1 << mapLayer;
        unitID = GetInstanceID();

        unitSpeed = 1;

        //foreach(Transform child in transform)
        //{
        //    if(child.tag.Equals("UnitMenu"))
        //    {
        //        unitMenu = child;
        //    }
        //}
    }

    void Update()
    {
        unitMenu.transform.position = transform.position;
    }

    void FixedUpdate()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hit, layerMask))
        //    {
        //        if (hit.collider != null)
        //        {
        //            Node node = grid.NodeFromWorldPoint(hit.point);
        //            PathFindingManager.RequestPath(transform.position, node.worldPosition, OnPathFound);
        //            //Debug.DrawLine(new Vector3(-0.3f, 7.5f, -.5f), hit.point, Color.red, 100);
        //            //Debug.DrawLine(new Vector3(-0.3f, 7.5f, -.5f), node.worldPosition, Color.blue, 100);
        //        }
        //    }
        //}
    }

    public void DoTurn()
    {
        Debug.Log("SDFDSF");
        unitMenu.gameObject.SetActive(true);
    }

    public void FinishedTurn()
    {
        unitMenu.gameObject.SetActive(false);
        TurnManager.FinishedTurn();
    }
    
    public void StopFollowingPath()
    {
        StopCoroutine(currentCoroutine);
        targetIndex = 0;
    }

    void OnPathFound(List<Node> newPath, bool isSuccess)
    {
        Debug.Log("PATH FOUND: " + isSuccess);
        if (isSuccess)
        {
            path = newPath;
            if (currentCoroutine == null)
            {
                Debug.Log("FIRST");
                currentCoroutine = FollowPath();
                StartCoroutine(currentCoroutine);
            }
            else
            {
                Debug.Log("SUB");
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
                    //Debug.Log("LENGTH: " + path.Length);
                    tempNode = path[targetIndex - 1];
                    targetIndex = 0;
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
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (0.3f));
            }
        }
    }
}
