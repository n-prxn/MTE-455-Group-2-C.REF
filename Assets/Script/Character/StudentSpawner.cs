using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StudentSpawner : MonoBehaviour
{
    public int maxStudentOnMap = 4;
    private Bounds floor;
    [SerializeField] private Transform studentParent;
    void Awake()
    {
        floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Renderer>().bounds;
    }

    void OnEnable()
    {
        InitializeStudents();
    }

    public void InitializeStudents()
    {
        ResetStudentOnMap();
        List<Student> students = new List<Student>();
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Gameplay")
        {
            students = SquadController.instance.Students;
            GenerateStudentOnMap(students, students.Count <= maxStudentOnMap ? students.Count : maxStudentOnMap, false);
        }
        else
        {
            students = TrainingManager.instance.GetCurrentStudentsInBuilding();
            maxStudentOnMap = students.Count;
            GenerateStudentOnMap(students, maxStudentOnMap, true);
        }
    }

    public void GenerateStudentOnMap(List<Student> students, int spawnAmount, bool inBuilding)
    {
        if (students.Count <= 0)
            return;

        for (int i = 0; i < spawnAmount; i++)
        {
            Student student = students[i];

            if(student == null)
                continue;

            if (student.studentModel == null)
                continue;

            if (student.IsOperating)
                continue;

            if (!inBuilding && student.IsTraining)
                continue;

            Vector3 spawnpoint = RandomSpawnpoint();
            GameObject studentPrefab = Instantiate(student.studentModel, spawnpoint, Quaternion.Euler(0, Random.Range(0, 360), 0));
            studentPrefab.transform.SetParent(studentParent);

            if (inBuilding)
            {
                studentPrefab.transform.localScale *= 5;
                studentPrefab.GetComponent<NavMeshAgent>().speed *= 5;
            }
        }
    }

    void ResetStudentOnMap()
    {
        foreach(Transform student in studentParent){
            Destroy(student.gameObject);
        }
    }
    public Vector3 RandomSpawnpoint()
    {
        float rndX, rndZ;
        rndX = Random.Range(floor.min.x, floor.max.x);
        rndZ = Random.Range(floor.min.z, floor.max.z);
        Vector3 spawnpoint = new Vector3(rndX, studentParent.position.y, rndZ);
        return spawnpoint;
    }
}
