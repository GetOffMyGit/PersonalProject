using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour {

    static HighlightManager instance;

    public GameObject moveHighlight;

    List<GameObject> currentMoveHighlights;


    void Awake()
    {
        instance = this;
    }

    public static void MoveHighlight(List<Node> moveNodes)
    {
        instance.currentMoveHighlights = new List<GameObject>();

        foreach (Node node in moveNodes)
        {
            GameObject highlight = Instantiate(instance.moveHighlight, node.surfacePosition, instance.moveHighlight.transform.rotation);
            instance.currentMoveHighlights.Add(highlight);
        }
    }

    public static void DestroyMoveHighlight()
    {
        foreach(GameObject highlight in instance.currentMoveHighlights)
        {
            Destroy(highlight);
        }
    }
}
