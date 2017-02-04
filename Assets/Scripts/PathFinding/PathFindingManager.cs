using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingManager : MonoBehaviour {
    static PathFindingManager instance;
    PathFinder pathFinder;

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathFinder = GetComponent<PathFinder>();
    }

	public static void RequestPath(Vector3 startPath, Vector3 endPath, Action<List<Node>, bool> callback)
    {
        PathRequest pathRequest = new PathRequest(startPath, endPath, callback);
        instance.pathRequestQueue.Enqueue(pathRequest);
        instance.TryProcessNext();
    }

    public static List<Node> RequestReachAbleNodes(Vector3 startPos, int unitSpeed)
    {
        return instance.pathFinder.GetReachableNodes(startPos, unitSpeed);
    }

    void TryProcessNext()
    {
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinder.StartPathFinder(currentPathRequest.startPath, currentPathRequest.endPath);
        }
    }

    public void FinishedProcessingPath(List<Node> path, bool isSuccess)
    {
        currentPathRequest.callback(path, isSuccess);
        isProcessingPath = false;
        TryProcessNext();
    }
}

public class PathRequest
{
    public Vector3 startPath;
    public Vector3 endPath;
    public Action<List<Node>, bool> callback;

    public PathRequest(Vector3 start, Vector3 end, Action<List<Node>, bool> call)
    {
        startPath = start;
        endPath = end;
        callback = call;
    }
}