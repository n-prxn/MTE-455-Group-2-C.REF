using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Turn")]
    public int currentTurn = 1;
    public int lastTurn = 150;

    [Header("Resource Amount")]
    public int credits = 0;
    public int pyroxenes = 0;
    public int happiness = 50;
    public int crimeRate = 50;
    public int rollCost = 0;

    [Header("Schale Rank")]
    public int currentXP = 0;
    public int maxXP = 1000;
    public int rank = 1;

    [Header("Capacity")]
    public int requestPerTurn = 3;
    public int maxRequestCapacity = 9;

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextTurn()
    {
        if (currentTurn < lastTurn)
        {
            currentTurn = currentTurn + 1;
            UpdateRequest();
        }
    }

    public void UpdateRequest(){
        foreach(RequestSO request in RequestManager.instance.OperatingRequests){
            request.CurrentTurn--;
            if(request.CurrentTurn <= 0){
                ReceiveRewards(request);
                request.ResetSquad();
                Debug.Log(request.name + " has finished!");
            }
        }
    }

    void ReceiveRewards(RequestSO request){
        credits += request.credit;
        pyroxenes += request.pyroxene;
        currentXP += request.xp;
        happiness += request.happiness;
        crimeRate -= request.crimeRate;
    }
}
