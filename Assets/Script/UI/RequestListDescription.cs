using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

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

    // Start is called before the first frame update

    void Awake(){
        ResetDescription();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDescription(RequestSO request){
        requesterPortrait.sprite = request.portrait;
        requestText.text = request.name;
        requesterText.text = request.requesterName;
        descriptionText.text = request.description;
        creditText.text = request.credit.ToString();
        pyroxeneText.text = request.pyroxene.ToString();
        expText.text = request.xp.ToString();
        happinessText.text = request.happiness.ToString();
        crimeRateText.text = request.crimeRate.ToString();
        leftDayText.text = (request.availableDuration - request.ExpireCount).ToString() + " Left Day(s).";
    }

    public void ResetDescription(){
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
