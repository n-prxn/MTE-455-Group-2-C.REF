using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class StudentSpawner : MonoBehaviour
{
    public int maxStudentOnMap;
    private Bounds floor;
    [SerializeField] private List<Student> studentOnMap = new List<Student>();
    [SerializeField] List<Student> validStudents = new List<Student>();
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
            validStudents = students.Where(
                        student => student.studentModel != null && !student.IsOperating && !student.IsTraining).ToList();
            ShuffleList(validStudents);
            if (validStudents.Count > 0)
                GenerateStudentOnMap(validStudents, validStudents.Count <= maxStudentOnMap ? validStudents.Count : maxStudentOnMap, false);
        }
        else
        {
            students = TrainingManager.instance.GetCurrentStudentsInBuilding();
            maxStudentOnMap = students.Count;
            if (maxStudentOnMap > 0)
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

            if (!inBuilding)
            {
                studentOnMap.Add(student);
            }

            // if (inBuilding)
            // {
            //     student = students[i];
            // }
            // else
            // {

            //     student = students[Mathf.FloorToInt(Random.Range(0, students.Count))];
            //     if (studentOnMap.Count > 0)
            //         foreach (Student studentList in studentOnMap)
            //         {
            //             if (studentList.id == student.id)
            //             {
            //                 int loopCount = 0;
            //                 do
            //                 {
            //                     loopCount++;
            //                     student = students[Mathf.FloorToInt(Random.Range(0, students.Count))];
            //                 } while (studentList.id == student.id || loopCount <= 10);
            //             }
            //         }
            //     studentOnMap.Add(student);
            // }

            if (student == null)
                continue;
            if (student.studentModel == null)
                continue;
            if (student.IsOperating)
                continue;
            // if (student.name == "Mari")
            //     continue;
            if (!inBuilding && student.IsTraining)
                continue;

            Vector3 spawnpoint = RandomSpawnpoint();
            GameObject studentPrefab = Instantiate(student.studentModel, spawnpoint, Quaternion.Euler(0, Random.Range(0, 360), 0));
            studentPrefab.transform.SetParent(studentParent);

            if (inBuilding)
            {
                studentPrefab.transform.localScale *= 4;
                studentPrefab.GetComponent<NavMeshAgent>().speed *= 4;
            }
        }
    }

    void ResetStudentOnMap()
    {
        if (studentParent.childCount > 0)
        {
            foreach (Transform student in studentParent)
            {
                Destroy(student.gameObject);
            }
        }
        validStudents.Clear();
        studentOnMap.Clear();
    }
    public Vector3 RandomSpawnpoint()
    {
        float rndX, rndZ;
        rndX = Random.Range(floor.min.x, floor.max.x);
        rndZ = Random.Range(floor.min.z, floor.max.z);
        Vector3 spawnpoint = new Vector3(rndX, studentParent.position.y, rndZ);
        return spawnpoint;
    }

    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            // Generate a random index between i and the end of the list
            int randomIndex = Random.Range(i, n);

            // Swap the elements at i and randomIndex
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
