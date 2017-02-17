using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreUnit : MonoBehaviour
{
    public Grid grid;

    float speed = 0.05f;
    List<Node> path;
    int targetIndex;
    int layerMask;
    IEnumerator currentCoroutine;

    bool finishedFollowingPath;

    // Use this for initialization
    void Start()
    {
        int mapLayer = 8;
        layerMask = 1 << mapLayer;
        finishedFollowingPath = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishedFollowingPath)
        {
            CameraScript.LookAt(transform.position);
        }
    }

    void FixedUpdate()
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
                    PathFindingManager.RequestPath(transform.position, node.worldPosition, OnPathFound);
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
                    PathFindingManager.RequestPath(transform.position, node.worldPosition, OnPathFound);
                }
            }
        }
    }

    void PathFinished()
    {
        targetIndex = 0;
        finishedFollowingPath = true;
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
                targetIndex = 0;
                StopCoroutine(currentCoroutine);
                currentCoroutine = FollowPath();
                StartCoroutine(currentCoroutine);
            }
        }
    }

    IEnumerator FollowPath()
    {
        Node currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint.surfacePosition)
            {
                targetIndex++;
                if (targetIndex >= path.Count)
                {
                    PathFinished();
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.surfacePosition, speed);
            yield return null;
        }
    }
}
