using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInfo : MonoBehaviour {

    public TerrainType terrainType;
    public bool customNode;
    public bool walkable;
    public bool blocksVision;
    public int weight;

	// Use this for initialization
	void Awake () {
		if(!customNode)
        {
            switch(terrainType)
            {
                case TerrainType.grassLand:
                    walkable = true;
                    blocksVision = false;
                    weight = 0;
                    break;

                case TerrainType.desert:
                    walkable = true;
                    blocksVision = false;
                    weight = 0;
                    break;

                case TerrainType.jungle:
                    walkable = true;
                    blocksVision = true;
                    weight = 1;
                    break;

                case TerrainType.lava:
                    walkable = true;
                    blocksVision = false;
                    weight = 2;
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
