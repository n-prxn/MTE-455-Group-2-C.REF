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

    [Header("Back Button")]
    [SerializeField] GameObject backButton;

    [Header("Turn UI")]
    [SerializeField] TMP_Text turnText;

    [Header("Resource UI")]
    [SerializeField] TMP_Text creditAmountText;
    [SerializeField] TMP_Text pyroxeneAmountText;
    [SerializeField] TMP_Text crimeRateText;
    [SerializeField] TMP_Text happinessText;

    [Header("Player Info UI")]
    [SerializeField] TMP_Text locationText;
    [SerializeField] TMP_Text rankText;
    [SerializeField] Image xpBar;

    [Header("Side Menu")]
    [SerializeField] GameObject gachaButton;
    [SerializeField] GameObject requestButton;
    [SerializeField] GameObject sidePanel;
    [SerializeField] GameObject nextButton;
    
    [Header("Mode Header")]
    [SerializeField] GameObject placingMode;
    [SerializeField] GameObject movingMode;
    [SerializeField] GameObject storingMode;

    [Header("Character")]
    [SerializeField] GameObject characterParent;
    public static UIDisplay instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIResource();
        UpdateButton();

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Gameplay"){
            backButton.SetActive(true);
            sidePanel.transform.GetChild(2).gameObject.SetActive(true);
            sidePanel.transform.GetChild(5).gameObject.SetActive(true);
            locationText.text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }else{
            backButton.SetActive(false);
            sidePanel.transform.GetChild(2).gameObject.SetActive(false);
            sidePanel.transform.GetChild(5).gameObject.SetActive(false);
            locationText.text = "Schale";
        }
    }

    void UpdateButton()
    {
        if (GameManager.instance.pyroxenes >= 1200)
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
        turnText.text = GameManager.instance.currentTurn.ToString();

        //Update Resource
        creditAmountText.text = GameManager.instance.credits.ToString();
        pyroxeneAmountText.text = GameManager.instance.pyroxenes.ToString();

        //Update Rank
        rankText.text = "RANK " + GameManager.instance.rank.ToString();
        xpBar.fillAmount = (float)GameManager.instance.currentXP / (float)GameManager.instance.maxXP;

        crimeRateText.text = GameManager.instance.crimeRate.ToString() + "%";
        happinessText.text = GameManager.instance.happiness.ToString() + "%";

        //Debug.Log(GameManager.instance.currentXP/GameManager.instance.maxXP);

    }

    public void ToggleGachaPanel()
    {
        if (gachaPanel.activeSelf)
        {
            gachaPanel.SetActive(false);
            blackBackground.SetActive(false);
        }
        else
        {
            gachaPanel.SetActive(true);
            blackBackground.SetActive(true);
        }
    }

    public void ToggleRequestListPanel()
    {
        if (requestListPanel.activeSelf)
        {
            requestListPanel.SetActive(false);
            blackBackground.SetActive(false);
        }
        else
        {
            requestListPanel.SetActive(true);
            blackBackground.SetActive(true);
        }
    }

    public void TogglePanel(GameObject panel)
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            blackBackground.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            blackBackground.SetActive(true);
        }
    }

    public void ToggleBlackBackground(bool isToggled){
        blackBackground.SetActive(isToggled);
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

    public void EnableMode(int mode){
        
        sidePanel.SetActive(false);
        nextButton.SetActive(false);
        backButton.SetActive(false);
        
        placingMode.SetActive(false);
        movingMode.SetActive(false);
        storingMode.SetActive(false);

        characterParent.SetActive(false);

        switch(mode){
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

    public void DisableMode(){
        sidePanel.SetActive(true);
        nextButton.SetActive(true);
        backButton.SetActive(true);

        placingMode.SetActive(false);
        movingMode.SetActive(false);
        storingMode.SetActive(false);

        characterParent.SetActive(true);
    }
}
