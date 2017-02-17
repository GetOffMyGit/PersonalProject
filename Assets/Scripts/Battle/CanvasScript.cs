using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour {
    Canvas unitMenu;
    Transform moveButton;
    Transform actionButton;
    Transform waitButton;
    Transform moveCancelButton;

	// Use this for initialization
	void Awake () {
        unitMenu = GetComponent<Canvas>();

        foreach(Transform child in transform)
        {
            if(child.tag.Equals("MoveButton"))
            {
                moveButton = child;
                Debug.Log("MOVE");
            } else if(child.tag.Equals("ActionButton"))
            {
                actionButton = child;
                Debug.Log("ACTION");
            } else if(child.tag.Equals("WaitButton"))
            {
                waitButton = child;
                Debug.Log("WAIT");
            } else if(child.tag.Equals("MoveCancelButton"))
            {
                moveCancelButton = child;
                Debug.Log("CANCEL");
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ShowCanvas()
    {
        gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        gameObject.SetActive(false);
    }

    public void ShowMoveCancel()
    {
        gameObject.SetActive(true);
        moveButton.gameObject.SetActive(false);
        moveCancelButton.gameObject.SetActive(true);
        waitButton.gameObject.SetActive(true);
    }

    public void ShowMoveButton()
    {
        moveCancelButton.gameObject.SetActive(false);
        moveButton.gameObject.SetActive(true);
        waitButton.gameObject.SetActive(true);
    }
}
