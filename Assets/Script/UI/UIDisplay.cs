using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIDisplay : MonoBehaviour
{
    [Header("Panel")]
    public GameObject gachaPanel;
    public GameObject requestListPanel;
    public GameObject overallPanel;
    public GameObject shopPanel;
    public GameObject blackBackground;
    public GameObject gameplayGUI;
    public GameObject buildingDetailPanel;

    [Header("Back Button")]
    [SerializeField] GameObject backButton;

    [Header("Turn UI")]
    [SerializeField] TMP_Text turnText;

    [Header("Resource UI")]
    [SerializeField] TMP_Text creditAmountText;
    [SerializeField] TMP_Text pyroxeneAmountText;
    [SerializeField] TMP_Text crimeRateText;
    [SerializeField] TMP_Text happinessText;
    [SerializeField] TMP_Text studentAmountText;

    [Header("Buidling Info UI")]
    [SerializeField] TMP_Text traineeAmountText;
    [SerializeField] TMP_Text furnitureAmountText;
    [SerializeField] TMP_Text bonusAmountText;

    [Header("Player Info UI")]
    [SerializeField] TMP_Text locationText;
    [SerializeField] TMP_Text rankText;
    [SerializeField] TMP_Text XPText;
    [SerializeField] Image xpBar;

    [Header("Side Menu")]
    [SerializeField] GameObject gachaButton;
    [SerializeField] GameObject requestButton;
    [SerializeField] GameObject sidePanel;
    [SerializeField] GameObject nextButton;
    [Header("Splash Screen")]
    [SerializeField] GameObject splashScreen;
    [SerializeField] GameObject emergencyScreen;

    [Header("Mode Header")]
    [SerializeField] GameObject placingMode;
    [SerializeField] GameObject movingMode;
    [SerializeField] GameObject storingMode;

    [Header("Character")]
    GameObject characterParent;
    [Header("Condition")]
    private bool hasPlayEmergencyScreen = false;
    public bool HasPlayEmergencyScreen { get => hasPlayEmergencyScreen; set => hasPlayEmergencyScreen = value; }
    [Header("Notification")]
    [SerializeField] private GameObject levelUpPrefab;
    [SerializeField] private GameObject notificationParent;

    public static UIDisplay instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        DisableAllPanel();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        characterParent = GameObject.FindGameObjectWithTag("Student Parent");
        UpdateUIResource();
        UpdateButton();

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Gameplay")
        {
            backButton.SetActive(true);
            sidePanel.transform.GetChild(2).gameObject.SetActive(true);
            sidePanel.transform.GetChild(5).gameObject.SetActive(true);
            locationText.text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            buildingDetailPanel.SetActive(true);
        }
        else
        {
            backButton.SetActive(false);
            sidePanel.transform.GetChild(2).gameObject.SetActive(false);
            sidePanel.transform.GetChild(5).gameObject.SetActive(false);
            locationText.text = "Schale";

            buildingDetailPanel.SetActive(false);
        }

        if(GameManager.Instance.rank < 3){
            sidePanel.transform.GetChild(4).gameObject.SetActive(false);
        }else{
            sidePanel.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    void UpdateButton()
    {
        if (GameManager.Instance.pyroxenes >= 1200)
        {
            gachaButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gachaButton.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (RequestManager.instance.isNotice())
        {
            requestButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            requestButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void UpdateUIResource()
    {
        //Update Turn
        turnText.text = GameManager.Instance.currentTurn.ToString();

        //Update Resource
        creditAmountText.text = GameManager.Instance.credits.ToString();
        pyroxeneAmountText.text = GameManager.Instance.pyroxenes.ToString();

        //Update Rank
        rankText.text = "RANK " + GameManager.Instance.rank.ToString();
        xpBar.fillAmount = (float)GameManager.Instance.currentXP / (float)GameManager.Instance.maxXP;
        XPText.text = GameManager.Instance.currentXP.ToString() + " / 220";

        crimeRateText.text = GameManager.Instance.crimeRate.ToString() + "%";
        happinessText.text = GameManager.Instance.happiness.ToString() + "%";

        studentAmountText.text = SquadController.instance.Students.Count.ToString();

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Gameplay" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Menu" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Setting")
        {
            traineeAmountText.text = TrainingManager.instance.GetStudentAmountInBuilding().ToString() + " / " + TrainingManager.instance.GetCurrentBuilding().StudentCapacity.ToString();
            furnitureAmountText.text = TrainingManager.instance.GetCurrentBuilding().CurrentFurnitureAmount.ToString();
            switch (TrainingManager.instance.GetCurrentBuilding().BuildingType)
            {
                case BuildingType.Dormitory:
                    bonusAmountText.text = TrainingManager.instance.GetCurrentBuilding().BonusStaminaRested.ToString();
                    break;
                case BuildingType.Gym:
                    bonusAmountText.text = TrainingManager.instance.GetCurrentBuilding().BonusPHYTraining.ToString();
                    break;
                case BuildingType.Library:
                    bonusAmountText.text = TrainingManager.instance.GetCurrentBuilding().BonusINTTraining.ToString();
                    break;
                case BuildingType.Cafe:
                    bonusAmountText.text = TrainingManager.instance.GetCurrentBuilding().BonusCOMTraining.ToString();
                    break;
                case BuildingType.Inventory:
                    break;
            }
        }
        //Debug.Log(GameManager.instance.currentXP/GameManager.instance.maxXP);

    }

    public void ToggleGachaPanel()
    {
        if (gachaPanel.activeSelf)
        {
            gachaPanel.SetActive(false);
            blackBackground.SetActive(false);
            GameManager.Instance.uiIsOpen = false;
        }
        else
        {
            gachaPanel.SetActive(true);
            blackBackground.SetActive(true);
            GameManager.Instance.uiIsOpen = true;
        }
    }

    public void ToggleRequestListPanel()
    {
        if (requestListPanel.activeSelf)
        {
            requestListPanel.SetActive(false);
            blackBackground.SetActive(false);
            GameManager.Instance.uiIsOpen = false;
        }
        else
        {
            requestListPanel.SetActive(true);
            blackBackground.SetActive(true);
            GameManager.Instance.uiIsOpen = true;
        }
    }

    public void TogglePanel(GameObject panel)
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            blackBackground.SetActive(false);
            GameManager.Instance.uiIsOpen = false;
        }
        else
        {
            panel.SetActive(true);
            blackBackground.SetActive(true);
            GameManager.Instance.uiIsOpen = true;
        }
    }

    public void ToggleOnlyThisPanel(GameObject panel)
    {
        TogglePanel(panel);
        foreach (Transform UIPanel in gameplayGUI.transform)
        {
            if (UIPanel.gameObject.name != panel.name)
            {
                UIPanel.gameObject.SetActive(false);
            }
        }
    }

    public void DisableAllPanel()
    {
        foreach (Transform panel in gameplayGUI.transform)
        {
            panel.gameObject.SetActive(false);
        }
    }

    public void ToggleBlackBackground(bool isToggled)
    {
        blackBackground.SetActive(isToggled);
    }

    public void ToggleBlackBackground()
    {
        blackBackground.SetActive(!blackBackground.activeSelf);
    }

    public void ToggleSidePanel()
    {
        if (sidePanel.activeSelf && nextButton.activeSelf)
        {
            sidePanel.SetActive(false);
            nextButton.SetActive(false);
        }
        else
        {
            sidePanel.SetActive(true);
            nextButton.SetActive(true);
        }
    }

    public void EnableMode(int mode)
    {

        sidePanel.SetActive(false);
        nextButton.SetActive(false);
        backButton.SetActive(false);

        placingMode.SetActive(false);
        movingMode.SetActive(false);
        storingMode.SetActive(false);

        foreach (Transform student in characterParent.transform)
        {
            student.gameObject.SetActive(false);
        }

        switch (mode)
        {
            case 0:
                placingMode.SetActive(true);
                break;
            case 1:
                movingMode.SetActive(true);
                break;
            case 2:
                storingMode.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void DisableMode()
    {
        sidePanel.SetActive(true);
        nextButton.SetActive(true);
        backButton.SetActive(true);

        placingMode.SetActive(false);
        movingMode.SetActive(false);
        storingMode.SetActive(false);

        foreach (Transform student in characterParent.transform)
        {
            student.gameObject.SetActive(true);
        }
    }

    public void PlaySplashScreen()
    {
        splashScreen.SetActive(true);
    }

    public void PlayEmergencyScreen()
    {
        if(!hasPlayEmergencyScreen)
        {
            emergencyScreen.SetActive(true);
            hasPlayEmergencyScreen = true;
        }
    }

    public void GenerateLevelUpNotification(ItemSO itemReward){
        GameObject levelUpPanel = Instantiate(levelUpPrefab, notificationParent.transform);
        levelUpPanel.GetComponent<RankUpNotice>().UpdateRewardUI(itemReward);
        ToggleBlackBackground(true);
    }
}
