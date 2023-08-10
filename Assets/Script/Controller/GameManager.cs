using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int credits = 0;
    public int pyroxenes = 0;

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
