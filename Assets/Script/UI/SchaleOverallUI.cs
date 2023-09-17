using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SchaleOverallUI : MonoBehaviour
{
    [Header("Request")]
    [SerializeField] TMP_Text successRequestAmount;
    [SerializeField] TMP_Text failedRequestAmount;
    [SerializeField] TMP_Text requestCapacityAmount;
    [SerializeField] TMP_Text requestPerDayAmount;

    [Header("Student")]
    [SerializeField] TMP_Text totalStudentAmount;
    [SerializeField] TMP_Text vacantStudentAmount;

    [Header("Building")]
    [SerializeField] TMP_Text gymStatus;
    [SerializeField] TMP_Text gymFurnitureAmount;
    [SerializeField] TMP_Text PHYBonus;
    [SerializeField] TMP_Text libraryStatus;
    [SerializeField] TMP_Text libraryFurnitureAmount;
    [SerializeField] TMP_Text INTBonus;
    [SerializeField] TMP_Text cafeStatus;
    [SerializeField] TMP_Text cafeFurnitureAmount;
    [SerializeField] TMP_Text COMBonus;
    [SerializeField] TMP_Text dormStatus;
    [SerializeField] TMP_Text dormFurnitureAmount;
    [SerializeField] TMP_Text staminaBonus;
    [SerializeField] TMP_Text shopStatus;
    [SerializeField] TMP_Text shopCapacityAmount;
    // Start is called before the first frame update
    void OnEnable()
    {
        successRequestAmount.text = GameManager.instance.successRequest.ToString();
        failedRequestAmount.text = GameManager.instance.failedRequest.ToString();
        requestCapacityAmount.text = RequestManager.instance.maxRequestCapacity.ToString();
        requestPerDayAmount.text = RequestManager.instance.requestPerTurn.ToString();

        totalStudentAmount.text = SquadController.instance.Students.Count.ToString();
        vacantStudentAmount.text = SquadController.instance.GetAllVacantStudentAmount().ToString();

        BuildingSO gym = TrainingManager.instance.GetBuilding(BuildingType.Gym);
        gymStatus.text = GameManager.instance.rank >= 3 ? "Unlocked" : "Locked";
        gymFurnitureAmount.text = gym.CurrentFurnitureAmount.ToString();
        PHYBonus.text = "+" + TrainingManager.instance.BonusTraining(gym).ToString();

        BuildingSO library = TrainingManager.instance.GetBuilding(BuildingType.Library);
        libraryStatus.text = GameManager.instance.rank >= 3 ? "Unlocked" : "Locked";
        libraryFurnitureAmount.text = library.CurrentFurnitureAmount.ToString();
        INTBonus.text = "+" + TrainingManager.instance.BonusTraining(library).ToString();
        
        BuildingSO cafe = TrainingManager.instance.GetBuilding(BuildingType.Cafe);
        cafeStatus.text = GameManager.instance.rank >= 3 ? "Unlocked" : "Locked";
        cafeFurnitureAmount.text = cafe.CurrentFurnitureAmount.ToString();
        COMBonus.text = "+" + TrainingManager.instance.BonusTraining(cafe).ToString();

        BuildingSO dorm = TrainingManager.instance.GetBuilding(BuildingType.Dormitory);
        dormStatus.text = GameManager.instance.rank >= 2 ? "Unlocked" : "Locked";
        dormFurnitureAmount.text = dorm.CurrentFurnitureAmount.ToString();
        staminaBonus.text = "+" + TrainingManager.instance.BonusTraining(dorm).ToString();
    
        shopStatus.text = GameManager.instance.rank >= 4 ? "Unlocked" : "Locked";
        shopCapacityAmount.text = ShopManager.instance.MaxItem.ToString();
    }
}
