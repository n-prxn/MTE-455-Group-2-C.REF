using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    DataManager dataManager;
    StudentSpawner studentSpawner;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        studentSpawner = GameObject.FindGameObjectWithTag("Student Parent").GetComponent<StudentSpawner>();

        dataManager.InitializeGame();
        studentSpawner.InitializeStudents();
        Debug.Log("Load Game with");
    }
}
