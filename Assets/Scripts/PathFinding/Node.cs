using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {
    public Vector3 worldPosition;
    public Vector3 surfacePosition;
    public int gridX;
    public int gridY;
    public int gridZ;
    public TerrainType terrainType;
    public bool walkable;
    public bool blocksVision;
    public int weight;

    public Node parent;

    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    int index;

    public int heapIndex
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

	public Node(Vector3 position, int x, int y, int z, bool isWalkable)
    {
        worldPosition = position;
        gridX = x;
        gridY = y;
        gridZ = z;
        walkable = isWalkable;
    }

    public Node(Vector3 position, Vector3 surface, int x, int y, int z, NodeInfo nodeInfo)
    {
        worldPosition = position;
        surfacePosition = surface;
        gridX = x;
        gridY = y;
        gridZ = z;
        ExtractNodeInfo(nodeInfo);
    }

    void ExtractNodeInfo(NodeInfo nodeInfo)
    {
        terrainType = nodeInfo.terrainType;
        walkable = nodeInfo.walkable;
        blocksVision = nodeInfo.blocksVision;
        weight = nodeInfo.weight;
    }

    public int CompareTo(Node node)
    {
        int compare = fCost.CompareTo(node.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(node.hCost);
        }

        return -compare;
    }
}
