using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    public Grid grid;
    PathFindingManager pathFindingManager;
    //public Transform seeker, target;
    
	void Awake () {
        pathFindingManager = GetComponent<PathFindingManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //FindPath(seeker.position, target.position);
	}

    public void StartPathFinder(Vector3 startPos, Vector3 endPos)
    {
        StartCoroutine(FindPath(startPos, endPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 endPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node endNode = grid.NodeFromWorldPoint(endPos);

        Vector3[] wayPoints = new Vector3[0];
        bool isSuccess = false;

        if (startNode.walkable && endNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.maxNodeSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count() > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == endNode)
                {
                    isSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNodeNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, endNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        } else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
        
        yield return null;

        if(isSuccess)
        {
            wayPoints = RetracePath(startNode, endNode);
        }

        pathFindingManager.FinishedProcessingPath(wayPoints, isSuccess);
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);
        int distanceY = nodeB.gridY - nodeA.gridY;
        if(distanceY < 0)
        {
            distanceY = 0;
        }

        return distanceX + distanceZ + distanceY;
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        Vector3[] wayPoints = new Vector3[path.Count];

        grid.path = path;
        for (int i = 0; i < path.Count; i++)
        {
            wayPoints[i] = path[i].worldPosition;
        }
        return wayPoints;
    }

}
