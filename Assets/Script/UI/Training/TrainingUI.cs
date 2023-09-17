using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TrainingUI : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject cardParent;
    [SerializeField] GameObject cardPrefab;
    [Header("UI")]
    [SerializeField] TMP_Text buildingName;
    [SerializeField] StudentSelectionUI selectionUI;

    void OnEnable()
    {
        TrainingManager.instance.Calculate();
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
        buildingName.text = TrainingManager.instance.CurrentBuilding.ToString();
        ResetTrainingList();
        List<Student> students = TrainingManager.instance.TrainingGroup[TrainingManager.instance.CurrentBuilding];
        for (int i = 0; i < students.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardParent.transform);
            TrainingCardData cardData = card.GetComponent<TrainingCardData>();
            cardData.SetData(i, students[i]);
            cardData.OnStudentClicked += HandleStudentAssign;
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
