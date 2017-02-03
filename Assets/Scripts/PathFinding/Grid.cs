using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject startingNode;
    public GameObject map;
    public GameObject nodeInfos;
    //public Transform detectorOne, detectorTwo;
    public int maxNodeSize
    {
        get
        {
            return gridNodeSizeX * gridNodeSizeZ;
        }
    }

    Node[,] grid;

    Vector3 startingNodePos;
    float gridSizeX;
    float gridSizeY;
    float gridSizeZ;

    int gridNodeSizeX;
    int gridNodeSizeY;
    int gridNodeSizeZ;

    float nodeX;
    float nodeY;
    float nodeZ;

    float nodeXRadius;
    float nodeYRadius;
    float nodeZRadius;

    //float nodeGridXRadius;
    float nodeGridZDiameter;
    float nodeGridZRadius;

    bool tempBool;
    Node tempNode;

    public List<Node> path;

    // Use this for initialization
    void Awake()
    {
        startingNodePos = startingNode.transform.position;
        Renderer nodeRenderer = startingNode.GetComponent<Renderer>();
        nodeX = nodeRenderer.bounds.size.x;
        nodeY = nodeRenderer.bounds.size.y;
        nodeZ = nodeRenderer.bounds.size.z;

        nodeXRadius = nodeX / 2;
        nodeYRadius = nodeY / 2;
        nodeZRadius = nodeZ / 2;
        //Vector3 scale = startingNode.transform.localScale;
        //scale.z = 10f;
        //startingNode.transform.localScale = scale;

        //nodeGridXRadius = (3 * nodeX) / 4;
        nodeGridZDiameter = (3 * nodeZ) / 4;
        nodeGridZRadius = nodeGridZDiameter / 2;

        Renderer mapRenderer = map.GetComponent<Renderer>();
        gridSizeX = mapRenderer.bounds.size.x;
        gridSizeY = mapRenderer.bounds.size.y;
        gridSizeZ = mapRenderer.bounds.size.z;

        gridNodeSizeX = Convert.ToInt32(Math.Floor(mapRenderer.bounds.size.x / nodeX));
        gridNodeSizeY = Convert.ToInt32(Math.Floor(mapRenderer.bounds.size.y / nodeY));
        gridNodeSizeZ = Convert.ToInt32(Math.Floor(mapRenderer.bounds.size.z / nodeGridZDiameter));

        CreateGrid();
        CleanGridSetUp();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                Vector3 hitPoint = hit.point;
                //tempBool = true;
                //Node node = NodeFromWorldPoint(hitPoint);
                //tempNode = node;

                //Debug.Log("HIT");

                //Debug.Log("X: " + tempX + " Z: " + tempZ + "     mouse: " + Input.mousePosition);
            }
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridNodeSizeX, gridNodeSizeZ];
        Vector3 worldBottomLeft = startingNode.transform.position;
        RaycastHit hit;

        int mapLayer = 8;
        int layerMask = 1 << mapLayer;

        for (int x = 0; x < gridNodeSizeX; x++)
        {
            for (int z = 0; z < gridNodeSizeZ; z++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeX) + Vector3.forward * (z * nodeGridZDiameter);
                if (z % 2 != 0)
                {
                    worldPoint.x -= nodeXRadius;
                }

                Vector3 bottomPoint = worldPoint;
                bottomPoint.y = worldPoint.y - gridSizeY;
                Vector3 topPoint = worldPoint;
                topPoint.y = worldPoint.y + gridSizeY;
                
                if(Physics.Linecast(topPoint, bottomPoint, out hit, layerMask))
                {
                    if(hit.collider != null)
                    {
                        Vector3 position = hit.point;
                        int numNodesY = Mathf.CeilToInt(position.y / nodeY);

                        worldPoint.y = position.y;
                        //Debug.Log(hit.collider.name + " : " + hit.transform.position);
                        if (hit.transform.tag.Equals("NodeInfo"))
                        {
                            Transform hitTransform = hit.transform;
                            worldPoint.y -= hitTransform.GetComponent<Renderer>().bounds.size.y;
                            Vector3 surfacePosition = worldPoint;
                            surfacePosition.y = worldPoint.y + nodeYRadius;

                            numNodesY = Mathf.CeilToInt(worldPoint.y / nodeY);
                            NodeInfo nodeInfo = hitTransform.GetComponent<NodeInfo>();
                            grid[x, z] = new Node(worldPoint, surfacePosition, x, numNodesY, z, nodeInfo);
                        } else if (hit.transform.tag.Equals("Map"))
                        {
                            grid[x, z] = new Node(worldPoint, x, numNodesY, z, false);
                        }
                    }
                } else
                {
                    grid[x, z] = new Node(worldPoint, x, 0, z, false);
                }
            }
        }
    }

    void CleanGridSetUp()
    {
        Destroy(startingNode);
        Destroy(nodeInfos);
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3 adjustedPos = worldPosition - startingNodePos;
        float absX = Math.Abs(adjustedPos.x);
        float absY = Math.Abs(adjustedPos.y);
        float absZ = Math.Abs(adjustedPos.z);
        adjustedPos = new Vector3(absX, absY, absZ);
        int x = Mathf.RoundToInt(adjustedPos.x / nodeX);
        int z = Mathf.RoundToInt(adjustedPos.z / nodeGridZDiameter);

        Node initialNode = grid[x, z];
        //float distanceX = Mathf.Pow(Math.Abs(worldPosition.x - initialNode.gridX), 2);
        //float distanceZ = Mathf.Pow(Math.Abs(worldPosition.z - initialNode.gridZ), 2);

        //float distance = (float)Math.Sqrt(distanceX + distanceZ);
        //float distance = Vector3.Distance(worldPosition, initialNode.worldPosition);
        Vector3 adjustedVec = worldPosition - initialNode.worldPosition;
        adjustedVec.y = 0;
        float distance = adjustedVec.magnitude;

        //Debug.DrawLine(worldPosition, initialNode.worldPosition, Color.red, 100);

        List<Node> neighbours = GetNodeNeighbours(initialNode);
        Node closestNode = initialNode;
        float closestDistance = distance;
        for(int i = 0; i < neighbours.Count; i++)
        {
            //Debug.Log("DISTANCE: " + closestDistance);
            //Debug.DrawLine(worldPosition, neighbours[i].worldPosition, Color.blue, 100);
            //distanceX = Mathf.Pow(Math.Abs(worldPosition.x - neighbours[i].gridX), 2);
            //distanceZ = Mathf.Pow(Math.Abs(worldPosition.z - neighbours[i].gridZ), 2);

            //distance = (float) Math.Sqrt(distanceX + distanceZ);

            //distance = Vector2.Distance(worldPosition, neighbours[i].worldPosition);

            adjustedVec = worldPosition - neighbours[i].worldPosition;
            adjustedVec.y = 0;

            distance = adjustedVec.magnitude;
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = neighbours[i];
            }
        }

        return closestNode;
        //return closestNode;

        //float percentageX = (worldPosition.x + gridSizeX / 2) / gridSizeX;
        //float percentageZ = (worldPosition.z + gridSizeZ / 2) / gridSizeZ;
        //percentageX = Mathf.Clamp01(percentageX);
        //percentageZ = Mathf.Clamp01(percentageZ);

        //int x = Mathf.RoundToInt((gridNodeSizeX - 1) * percentageX);
        //int z = Mathf.RoundToInt((gridNodeSizeZ - 1) * percentageZ);

        //Debug.Log("percentX: " + percentageX + " percentZ: " + percentageZ);
        //return grid[x, z];
    }

    public List<Node> GetNodeNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        if (node.gridZ % 2 != 0)
        {
            return SortNeighbourWhenOdd(node, neighbours);
        }

        return SortNeighbourWhenEven(node, neighbours);
    }

    List<Node> SortNeighbourWhenOdd(Node node, List<Node> neighbours)
    {
        for (int x = -1; x < 2; x++)
        {
            for (int z = -1; z < 2; z++)
            {
                if (x == 0 && z == 0)
                {
                    continue;
                }

                if (x == 1 && z != 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkZ = node.gridZ + z;

                if (checkX >= 0 && checkX < gridNodeSizeX && checkZ >= 0 && checkZ < gridNodeSizeZ)
                {
                    neighbours.Add(grid[checkX, checkZ]);
                }
            }
        }
        return neighbours;
    }

    List<Node> SortNeighbourWhenEven(Node node, List<Node> neighbours)
    {
        for (int x = -1; x < 2; x++)
        {
            for (int z = -1; z < 2; z++)
            {
                int checkX;
                int checkZ;
                if (x == 0 && z == 0)
                {
                    continue;
                }

                if (x == 1 && z != 0)
                {
                    continue;
                }

                if (z != 0)
                {
                    int adjustedX = x + 1;
                    checkX = node.gridX + adjustedX;
                }
                else
                {
                    checkX = node.gridX + x;
                }
                checkZ = node.gridZ + z;

                if (checkX >= 0 && checkX < gridNodeSizeX && checkZ >= 0 && checkZ < gridNodeSizeZ)
                {
                    neighbours.Add(grid[checkX, checkZ]);
                }
            }
        }

        return neighbours;
    }


    void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (tempBool)
                {
                    if (node == tempNode)
                    {
                        //List<Node> neighbours = this.GetNodeNeighbours(tempNode);
                        //foreach(Node neighbourNode in neighbours)
                        //{
                        //    Gizmos.color = Color.magenta;
                        //    Gizmos.DrawCube(neighbourNode.worldPosition, Vector3.one * (0.1f));
                        //    Debug.Log("NEIGHBOUR: " + neighbours.Count);
                        //}
                        Gizmos.color = Color.magenta;
                    }
                    else
                    {
                        Gizmos.color = Color.gray;
                    }
                }
                Gizmos.color = Color.gray;
                if (node.walkable)
                {
                    Gizmos.color = Color.green;
                }
                Vector3 worldPos = node.worldPosition;
                //worldPos.x -= nodeXRadius;

                Vector3 bottomLeft = worldPos;
                bottomLeft.x -= nodeXRadius;
                bottomLeft.z -= nodeGridZRadius;

                Vector3 bottomRight = worldPos;
                bottomRight.x += nodeXRadius;
                bottomRight.z -= nodeGridZRadius;

                Vector3 topLeft = worldPos;
                topLeft.x -= nodeXRadius;
                topLeft.z += nodeGridZRadius;

                Vector3 topRight = worldPos;
                topRight.x += nodeXRadius;
                topRight.z += nodeGridZRadius;

                Gizmos.DrawLine(bottomLeft, bottomRight);
                Gizmos.DrawLine(bottomLeft, topLeft);
                Gizmos.DrawLine(topLeft, topRight);
                Gizmos.DrawLine(topRight, bottomRight);

                Gizmos.DrawCube(worldPos, Vector3.one * (0.1f));
            }

            //if (path != null)
            //{
            //    foreach (Node node in path)
            //    {
            //        Gizmos.color = Color.blue;
            //        Gizmos.DrawCube(node.worldPosition, Vector3.one * (0.2f));
            //    }
            //}
        }
        //Gizmos.DrawWireCube(transform.position, new Vector3(gridSizeX, gridSizeY, gridSizeZ));
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawCube(startingNode.transform.position, Vector3.one * (nodeX - 0.1f));
        //Gizmos.DrawCube(startingNode.transform.position, Vector3.one * (nodeZRadius));
    }
}
