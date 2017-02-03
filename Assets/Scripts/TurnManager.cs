using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public GameState currentGameState;
    public Unit currentUnit;

    Queue<Unit> unitOrder = new Queue<Unit>();

	// Use this for initialization
	void Start () {
        currentGameState = GameState.battle;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentGameState == GameState.battle)
        {
            currentUnit = unitOrder.Dequeue();
        }
	}
}

public enum GameState
{
    start,
    battle,
    end
}
