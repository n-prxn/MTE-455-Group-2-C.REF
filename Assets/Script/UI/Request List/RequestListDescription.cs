using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public enum RequestMode
{
    Available,
    InProgress,
    WaitResult
}

public class RequestListDescription : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image requesterPortrait;
    [SerializeField] TextMeshProUGUI requestText;
    [SerializeField] TextMeshProUGUI requesterText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI creditText;
    [SerializeField] TextMeshProUGUI pyroxeneText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI happinessText;
    [SerializeField] TextMeshProUGUI crimeRateText;
    [SerializeField] TextMeshProUGUI leftDayText;
    [SerializeField] GameObject acceptButton;
    [SerializeField] GameObject resultButton;
    [SerializeField] GameObject squadPanel;
    [SerializeField] GameObject squadPortraitPrefab;
    [SerializeField] GameObject squadPortraitParent;

    // Start is called before the first frame update

    void Awake()
    {
        ResetDescription();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDescription(RequestSO request, RequestMode mode)
    {
        requesterPortrait.sprite = request.portrait;
        requestText.text = request.name;
        requesterText.text = request.requesterName;
        descriptionText.text = request.description;
        creditText.text = request.credit.ToString();
        pyroxeneText.text = request.pyroxene.ToString();
        expText.text = request.xp.ToString();
        happinessText.text = request.happiness.ToString();
        crimeRateText.text = request.crimeRate.ToString();

        if (mode == RequestMode.Available)
        {
            leftDayText.text = (request.availableDuration - request.ExpireCount).ToString() + " Left Day(s).";
            squadPanel.SetActive(false);
            acceptButton.SetActive(true);
            resultButton.SetActive(false);
        }

        if (mode == RequestMode.InProgress)
        {
            leftDayText.text = "Complete in " + request.CurrentTurn.ToString() + " Day(s).";
            ResetSquadImages();
            foreach (Student student in request.squad)
            {
                if (student != null)
                {
                    GameObject squadImage = Instantiate(squadPortraitPrefab, squadPortraitParent.transform);
                    squadImage.transform.GetChild(0).GetComponent<Image>().sprite = student.portrait;
                }
            }
            squadPanel.SetActive(true);
            acceptButton.SetActive(false);
            resultButton.SetActive(false);
        }

        if (mode == RequestMode.WaitResult)
        {
            leftDayText.text = "";
            squadPanel.SetActive(false);
            acceptButton.SetActive(false);
            resultButton.SetActive(true);
        }
    }

    public void ResetSquadImages()
    {
        if (squadPanel.transform.childCount >= 0)
        {
            foreach (Transform image in squadPortraitParent.transform)
            {
                Destroy(image.gameObject);
            }
        }
    }

    public void ResetDescription()
    {
        requestText.text = "";
        requesterText.text = "";
        descriptionText.text = "";
        creditText.text = "";
        pyroxeneText.text = "";
        expText.text = "";
        happinessText.text = "";
        crimeRateText.text = "";
        leftDayText.text = "";
    }
}
