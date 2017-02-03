using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public GameState currentGameState;
    public Unit currentUnit;
    public Transform units;

    static TurnManager instance;

    //Queue<Unit> unitOrder = new Queue<Unit>();
    Dictionary<Unit, int> unitOrder = new Dictionary<Unit, int>();
    int initialOrder;
    bool isTakingTurn;

	// Use this for initialization
	void Start () {
        instance = this;

        currentGameState = GameState.battle;
        initialOrder = 100;
        isTakingTurn = false;

        foreach(Transform child in units.transform)
        {
            //unitOrder.Enqueue(child.GetComponent<Unit>());
            unitOrder.Add(child.GetComponent<Unit>(), initialOrder);
        }
        //unitOrder.Enqueue()
	}
	
	// Update is called once per frame
	void Update () {
        if (currentGameState == GameState.battle)
        {
            if(!isTakingTurn)
            {
                currentUnit = GetUnitForTurn();
                currentUnit.DoTurn();
                isTakingTurn = true;
            }
        }
	}

    public static void FinishedTurn()
    {
        instance.isTakingTurn = false;
    }

    Unit GetUnitForTurn()
    {
        Unit unitForTurn = null;
        int currentLowest = initialOrder;
        Unit[] unitsInGame = new Unit[unitOrder.Keys.Count];
        unitOrder.Keys.CopyTo(unitsInGame, 0);

        foreach(Unit unit in unitsInGame)
        {
            unitOrder[unit] -= unit.unitSpeed;
            if (unitOrder[unit] < currentLowest)
            {
                currentLowest = unitOrder[unit];
                unitForTurn = unit;
            }
        }

        unitOrder[unitForTurn] = initialOrder;

        //foreach(KeyValuePair<Unit, int> entry in unitOrder)
        //{
        //    unitOrder[entry.Key] = entry.Value - entry.Key.unitSpeed;
        //    if(entry.Value < currentLowest)
        //    {
        //        currentLowest = entry.Value;
        //        unitForTurn = entry.Key;
        //    }
        //}

        return unitForTurn;
    }
}

public enum GameState
{
    start,
    battle,
    end
}
