using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainingManager : MonoBehaviour, IData
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

    [Header("Student Pool")]
    public GachaPool gachaPool;

    [Header("UI")]
    [SerializeField] TrainingUI trainingUI;

    public static TrainingManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeTrainingGroup();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeTrainingGroup()
    {
        if (trainingGroup != null)
            return;

        trainingGroup = new Dictionary<BuildingType, List<Student>>();
        List<Student> gymStudents = new(), libraryStudents = new(), cafeStudents = new(), dormitoryStudents = new();

        for (int i = 0; i < 7; i++)
            gymStudents.Add(null);

        for (int i = 0; i < 7; i++)
            libraryStudents.Add(null);

        for (int i = 0; i < 7; i++)
            cafeStudents.Add(null);

        for (int i = 0; i < 7; i++)
            dormitoryStudents.Add(null);

        trainingGroup.Add(BuildingType.Gym, gymStudents);
        trainingGroup.Add(BuildingType.Library, libraryStudents);
        trainingGroup.Add(BuildingType.Cafe, cafeStudents);
        trainingGroup.Add(BuildingType.Dormitory, dormitoryStudents);
    }

    public void UpdateTrainingPanel()
    {
        Calculate();
        trainingUI.InitializeTrainingStudents();
    }

    public void Calculate()
    {
        foreach (Student student in trainingGroup[currentBuilding].ToList())
        {
            if (student != null)
                student.ResetTrainedStat();
        }

        foreach (Student student in trainingGroup[currentBuilding].ToList())
        {
            if (student != null)
            {
                if (student.skill != null)
                    student.skill.PerformSkill(student);
                AddBonus(student);
            }
        }
    }

    public BuildingSO GetCurrentBuilding()
    {
        return buildings.Find(x => x.BuildingType == currentBuilding);
    }

    public bool isStudentAtBuilding(BuildingType buildingType, Student student)
    {
        foreach (Student trainedStudent in trainingGroup[buildingType])
        {
            if (trainedStudent == null)
                continue;

            if (trainedStudent.id == student.id)
                return true;
        }
        return false;
    }

    public BuildingSO GetBuilding(BuildingType buildingType)
    {
        return buildings.Find(x => x.BuildingType == buildingType);
    }

    public List<Student> GetCurrentStudentsInBuilding()
    {
        return trainingGroup[currentBuilding];
    }

    public List<Student> GetStudentsAtBuilding(BuildingSO building)
    {
        return trainingGroup[building.BuildingType];
    }

    public int GetStudentAmountInBuilding()
    {
        if (currentBuilding == BuildingType.Inventory)
            return 0;

        int count = 0;
        foreach (Student student in trainingGroup[currentBuilding].ToList())
        {
            if (student != null)
                count++;
        }
        return count;
    }

    public void RemoveStudentFromBuilding(Student student)
    {
        foreach (Student trainedStudent in trainingGroup[currentBuilding].ToList())
        {
            if (trainedStudent == null)
                continue;

            if (trainedStudent.id == student.id)
            {
                trainedStudent.ResetTrainedStat();
                trainedStudent.TrainingDuration = GetCurrentBuilding().TrainingDuration;
                trainingGroup[currentBuilding].Remove(trainedStudent);
                trainingGroup[currentBuilding].Add(null);
            }
        }
    }

    public void UpdateTrainingProcess()
    {

    }

    public void UpdateStudentTraining()
    {
        foreach (KeyValuePair<BuildingType, List<Student>> building in trainingGroup)
        {
            if (building.Key == BuildingType.Dormitory)
                continue;

            for (int i = 0; i < building.Value.ToList().Count; i++)
            {
                Student trainedStudent = building.Value[i];
                if (trainedStudent == null)
                    return;

                trainedStudent.TrainingDuration--;
                if (trainedStudent.TrainingDuration <= 0)
                {
                    trainedStudent.IsTraining = false;
                    trainedStudent.UpdateTrainedStats();
                    building.Value[i] = null;
                }

            }

            /*foreach (Student trainedStudent in building.Value.ToList())
            {
                if (trainedStudent == null)
                    continue;

                if (trainedStudent.id == student.id)
                {
                    trainedStudent.ResetTrainedStat();
                    trainedStudent.TrainingDuration = GetCurrentBuilding().TrainingDuration;
                    trainingGroup[building.Key].Remove(trainedStudent);
                    trainingGroup[building.Key].Add(null);
                    return;
                }
            }*/
        }
    }

    public void UpdateStudentResting()
    {
        for (int i = 0; i < trainingGroup[BuildingType.Dormitory].Count; i++)
        {
            Student restedStudent = trainingGroup[BuildingType.Dormitory][i];
            if (restedStudent == null)
                return;

            restedStudent.TrainingDuration--;
            if (restedStudent.TrainingDuration <= 0)
            {
                restedStudent.UpdateTrainedStats();
                if (restedStudent.CurrentStamina >= restedStudent.stamina)
                {
                    restedStudent.IsTraining = false;
                    trainingGroup[BuildingType.Dormitory][i] = null;
                }
            }
        }
    }

    public void SetStudentInBuilding(int slot, Student student)
    {
        trainingGroup[currentBuilding][slot] = student;
    }

    public int BonusTraining(BuildingSO buildingSO)
    {
        int bonus = 0, amount = buildingSO.CurrentFurnitureAmount;

        if (amount >= 5)
            bonus = buildingSO.BuildingType == BuildingType.Dormitory ? 20 : 6;
        else if (amount >= 4)
            bonus = buildingSO.BuildingType == BuildingType.Dormitory ? 15 : 5;
        else if (amount >= 3)
            bonus = buildingSO.BuildingType == BuildingType.Dormitory ? 15 : 4;
        else if (amount >= 2)
            bonus = buildingSO.BuildingType == BuildingType.Dormitory ? 10 : 3;
        else if (amount >= 1)
            bonus = buildingSO.BuildingType == BuildingType.Dormitory ? 10 : 2;
        else if (amount >= 0)
            bonus = buildingSO.BuildingType == BuildingType.Dormitory ? 5 : 1;

        return bonus;
    }

    public void AddBonus(Student student)
    {
        student.TrainedPHYStat += GetCurrentBuilding().BonusPHYTraining;
        student.TrainedINTStat += GetCurrentBuilding().BonusINTTraining;
        student.TrainedCOMStat += GetCurrentBuilding().BonusCOMTraining;
        student.RestedStamina += GetCurrentBuilding().BonusStaminaRested;
    }

    public int BonusStudentCapacity(BuildingSO buildingSO)
    {
        int bonus = 0, amount = buildingSO.CurrentFurnitureAmount;
        if (amount >= 5)
            bonus = 7;
        else if (amount >= 4)
            bonus = 5;
        else if (amount >= 3)
            bonus = 5;
        else if (amount >= 2)
            bonus = 3;
        else if (amount >= 1)
            bonus = 3;
        else if (amount >= 0)
            bonus = 3;

        return bonus;
    }

    public int BonusStudentCapacity(int furnitureAmount)
    {
        int bonus = 0;
        if (furnitureAmount >= 5)
            bonus = 7;
        else if (furnitureAmount >= 4)
            bonus = 5;
        else if (furnitureAmount >= 3)
            bonus = 5;
        else if (furnitureAmount >= 2)
            bonus = 3;
        else if (furnitureAmount >= 1)
            bonus = 3;
        else if (furnitureAmount >= 0)
            bonus = 3;

        return bonus;
    }

    public void UpdateBuildingBonus(BuildingSO buildingSO)
    {
        switch (buildingSO.BuildingType)
        {
            case BuildingType.Dormitory:
                buildingSO.BonusStaminaRested = BonusTraining(buildingSO);
                break;
            case BuildingType.Gym:
                buildingSO.BonusPHYTraining = BonusTraining(buildingSO);
                break;
            case BuildingType.Library:
                buildingSO.BonusINTTraining = BonusTraining(buildingSO);
                break;
            case BuildingType.Cafe:
                buildingSO.BonusCOMTraining = BonusTraining(buildingSO);
                break;
        }
        buildingSO.StudentCapacity = BonusStudentCapacity(buildingSO);
    }

    public void LoadData(GameData data)
    {
        InitializeTrainingGroup();
        foreach (BuildingTrainingData btData in data.trainingBuildings)
        {
            BuildingSO building = buildings.Find(x => x.id == btData.id);
            BuildingType buildingType = building.BuildingType;

            building.IsAvailable = btData.isAvailable;
            building.StudentCapacity = btData.studentCapacity;
            building.FurnitureCapacity = btData.furnitureCapacity;

            for (int i = 0; i < btData.students.Count; i++)
            {
                if (btData.students[i] == -1)
                    trainingGroup[buildingType][i] = null;
                else
                    trainingGroup[buildingType][i] = gachaPool.StudentsPool.Find(x => x.id == btData.students[i]);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.trainingBuildings = new List<BuildingTrainingData>();
        foreach (BuildingSO building in buildings)
        {
            data.trainingBuildings.Add(new BuildingTrainingData(building, GetStudentsAtBuilding(building)));
        }
    }
}
