using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Unit"))
        {
            SceneManager.LoadScene("Scene_01", LoadSceneMode.Single);
        }
    }
}
