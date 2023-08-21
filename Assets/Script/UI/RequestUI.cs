using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image currentPHYBar;
    [SerializeField] Image currentINTBar;
    [SerializeField] Image currentCOMBar;

    [SerializeField] Image PHYReqBar;
    [SerializeField] Image INTReqBar;
    [SerializeField] Image COMReqBar;

    [SerializeField] private RequestSO currentRequest;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateRequestInfo();
    }

    void UpdateRequestInfo(){
        UpdateRequestRequirement();
    }

    void UpdateRequestRequirement(){
        PHYReqBar.rectTransform.localPosition = new Vector2((float)currentRequest.phyStat/300f * 485f, PHYReqBar.rectTransform.localPosition.y);
        INTReqBar.rectTransform.localPosition = new Vector2((float)currentRequest.intStat/300f * 485f, INTReqBar.rectTransform.localPosition.y);
        COMReqBar.rectTransform.localPosition = new Vector2((float)currentRequest.comStat/300f * 485f, COMReqBar.rectTransform.localPosition.y);
    }
}
