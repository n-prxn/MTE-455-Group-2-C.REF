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
    public int rollCost = 0;

    [Header("Schale Rank")]
    public int currentXP = 0;
    public int maxXP = 1000;
    public int rank = 1;


    public static GameManager instance;

    // Start is called before the first frame update
    void Awake(){
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
