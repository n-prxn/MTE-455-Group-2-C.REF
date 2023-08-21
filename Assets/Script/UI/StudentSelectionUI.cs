using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StudentSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject studentListParent;
    [SerializeField] GameObject studentPortraitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InitializeStudents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeStudents(){
        foreach(Student student in SquadController.instance.Students){
            GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.GetComponent<Transform>());
            studentCard.GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = student.portrait;
        }
    }
}
