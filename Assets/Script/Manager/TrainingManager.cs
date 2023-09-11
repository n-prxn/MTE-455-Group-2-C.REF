using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    [Header("Building")]
    private Dictionary<BuildingType, Student[]> trainingGroup;
    public Dictionary<BuildingType, Student[]> TrainingGroup
    {
        get { return trainingGroup; }
        set { trainingGroup = value; }
    }
    [Header("Capacity")]
    [SerializeField] private int studentCapacity = 3;
    public int StudentCapacity{
        get{return studentCapacity;}
        set{studentCapacity = value;}
    }
    public static TrainingManager instance;
    void Awake()
    {
        instance = this;
        InitializeTrainingGroup();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeTrainingGroup()
    {
        if (trainingGroup == null)
        {
            trainingGroup = new Dictionary<BuildingType, Student[]>();
            Student[] students = new Student[studentCapacity];
            for (int i = 0; i < students.Length; i++)
                students[i] = null;

            trainingGroup.Add(BuildingType.Gym, students);
            trainingGroup.Add(BuildingType.Library, students);
            trainingGroup.Add(BuildingType.Cafe, students);
            trainingGroup.Add(BuildingType.Dormitory, students);
        }
    }

    void InitializeTrainingStudentList(){
        
    }
}
