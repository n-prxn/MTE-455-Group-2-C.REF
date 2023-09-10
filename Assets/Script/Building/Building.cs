using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private byte id;
    [SerializeField] private new string name;
    [SerializeField] private int unlockedRank = 1;
    [SerializeField] private int studentCapacity = 3;

    [SerializeField] private GameObject availableModel;
    [SerializeField] private GameObject unavailableModel;
    private bool isAvailable = false;
    public bool IsAvailable{
        get{return isAvailable;}
        set{isAvailable = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.rank < unlockedRank){
            availableModel.SetActive(false);
            unavailableModel.SetActive(true);
            isAvailable = false;
        }else{
            availableModel.SetActive(true);
            unavailableModel.SetActive(false);
            isAvailable = true;
        }
    }
}
