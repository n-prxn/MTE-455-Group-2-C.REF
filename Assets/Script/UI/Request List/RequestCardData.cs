using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RequestCardData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private GameObject noticeSymbol;
    [SerializeField] private TextMeshProUGUI requesterText;
    [SerializeField] private TextMeshProUGUI requesterStatus;
    [SerializeField] private Image border;
    private RequestSO requestData;
    public RequestSO RequestData
    {
        get { return requestData; }
        set { requestData = value; }
    }

    public event Action<RequestCardData> OnCardClicked;

    public void SetData(RequestSO request){
        requestData = request;
        requesterText.text = requestData.requesterName;
        requesterStatus.text = request.chatStatus;
        portraitImage.sprite = requestData.portrait;
        
        if(!requestData.IsRead){
            noticeSymbol.SetActive(true);
        }

        switch(request.difficulty){
            case Difficulty.Easy:
                border.color = new Color32(97,197,21,255);
                break;
            case Difficulty.Hardcore:
                border.color = new Color32(255,154,0,255);
                break;
            case Difficulty.Extreme:
                border.color = new Color32(234,78,52,255);
                break;
            case Difficulty.Insane:
                border.color = new Color32(121,76,207,255);
                break;
            case Difficulty.Emergency:
                border.color = Color.red;
                break;
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        PointerEventData pointerEventData = data;
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            OnCardClicked?.Invoke(this);
        }
    }

    public void HideNoticeSymbol(){
        noticeSymbol.SetActive(false);   
    }
}
