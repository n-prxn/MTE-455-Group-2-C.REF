using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingSO : ScriptableObject
{
    [Header("Building Info")]
    public byte id;
    public new string name;
    public int unlockedRank = 1;
    public int maxStudent = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
