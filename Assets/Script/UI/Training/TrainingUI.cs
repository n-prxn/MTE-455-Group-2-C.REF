using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TrainingUI : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject cardParent;
    [SerializeField] GameObject cardPrefab;
    [Header("UI")]
    [SerializeField] TMP_Text buildingName;
    [SerializeField] StudentSelectionUI selectionUI;
    [SerializeField] Image backgroundImage;
    [Header("Background")]
    [SerializeField] Sprite[] backgrounds;

    void OnEnable()
    {
        TrainingManager.instance.Calculate();
        InitializeTrainingStudents();
    }

    public void InitializeTrainingStudents()
    {
        BuildingType currentBuilding = TrainingManager.instance.CurrentBuilding;
        buildingName.text = currentBuilding.ToString();
        ResetTrainingList();
        List<Student> students = TrainingManager.instance.TrainingGroup[currentBuilding];
        for (int i = 0; i < students.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardParent.transform);
            TrainingCardData cardData = card.GetComponent<TrainingCardData>();
            cardData.SetData(i, students[i]);
            cardData.OnStudentClicked += HandleStudentAssign;
        }

        switch (currentBuilding)
        {
            case BuildingType.Dormitory:
                backgroundImage.sprite = backgrounds[0];
                break;
            case BuildingType.Gym:
                backgroundImage.sprite = backgrounds[1];
                break;
            case BuildingType.Library:
                backgroundImage.sprite = backgrounds[2];
                break;
            case BuildingType.Cafe:
                backgroundImage.sprite = backgrounds[3];
                break;
        }
    }

    void ResetTrainingList()
    {
        if (cardParent.transform.childCount > 0)
        {
            foreach (Transform card in cardParent.transform)
            {
                Destroy(card.gameObject);
            }
        }
    }

    public void HandleStudentAssign(TrainingCardData obj)
    {
        selectionUI.SlotIndex = obj.Index;
        if (obj.TrainingStudent != null)
            selectionUI.CurrentSelectedStudent = obj.TrainingStudent;

        selectionUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
