using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TrainingUI : MonoBehaviour
{
    [SerializeField] BuildingType buildingType;
    [Header("Prefab")]
    [SerializeField] GameObject cardParent;
    [SerializeField] GameObject cardPrefab;
    [Header("UI")]
    [SerializeField] TMP_Text buildingName;

    void OnEnable(){
        InitializeTrainingStudents();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeTrainingStudents()
    {
        buildingName.text = buildingType.ToString();
        Student[] students = TrainingManager.instance.TrainingGroup[buildingType];
        for (int i = 0; i < students.Length; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardParent.transform);
            TrainingCardData cardData = card.GetComponent<TrainingCardData>();
            cardData.SetData(students[i]);
        }
    }
}
