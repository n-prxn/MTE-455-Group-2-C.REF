using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    [Header("Building")]
    private Dictionary<BuildingType, List<Student>> trainingGroup;
    public Dictionary<BuildingType, List<Student>> TrainingGroup
    {
        get { return trainingGroup; }
        set { trainingGroup = value; }
    }
    [SerializeField] private List<BuildingSO> buildings;
    public List<BuildingSO> Buildings
    {
        get { return buildings; }
        set { buildings = value; }
    }
    [SerializeField] private BuildingType currentBuilding;
    public BuildingType CurrentBuilding
    {
        get { return currentBuilding; }
        set { currentBuilding = value; }
    }

    public static TrainingManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTrainingGroup();
        }
        else
        {
            Destroy(gameObject);
        }
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
        if (trainingGroup != null)
            return;

        trainingGroup = new Dictionary<BuildingType, List<Student>>();
        List<Student> gymStudents = new(), libraryStudents = new(), cafeStudents = new(), dormitoryStudents = new();

        for (int i = 0; i < GetBuilding(BuildingType.Gym).StudentCapacity; i++)
            gymStudents.Add(null);

        for (int i = 0; i < GetBuilding(BuildingType.Gym).StudentCapacity; i++)
            libraryStudents.Add(null);

        for (int i = 0; i < GetBuilding(BuildingType.Gym).StudentCapacity; i++)
            cafeStudents.Add(null);

        for (int i = 0; i < GetBuilding(BuildingType.Gym).StudentCapacity; i++)
            dormitoryStudents.Add(null);

        trainingGroup.Add(BuildingType.Gym, gymStudents);
        trainingGroup.Add(BuildingType.Library, libraryStudents);
        trainingGroup.Add(BuildingType.Cafe, cafeStudents);
        trainingGroup.Add(BuildingType.Dormitory, dormitoryStudents);
    }

    public BuildingSO GetCurrentBuilding()
    {
        return buildings.Find(x => x.BuildingType == currentBuilding);
    }

    public BuildingSO GetBuilding(BuildingType buildingType)
    {
        return buildings.Find(x => x.BuildingType == buildingType);
    }

    public List<Student> GetCurrentStudentsInBuilding()
    {
        return trainingGroup[currentBuilding];
    }

    public void SetStudentInBuilding(int slot, Student student)
    {
        trainingGroup[currentBuilding][slot] = student;
    }

    public int BonusTraining(BuildingSO buildingSO)
    {
        int bonus = 0, amount = buildingSO.CurrentFurnitureAmount;

        if (amount >= 10)
            bonus = 6;
        else if (amount >= 8)
            bonus = 5;
        else if (amount >= 6)
            bonus = 4;
        else if (amount >= 4)
            bonus = 3;
        else if (amount >= 2)
            bonus = 2;
        else if (amount >= 0)
            bonus = 1;

        return bonus;
    }
}
