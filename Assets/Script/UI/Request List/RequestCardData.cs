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
    private RequestSO requestData;
    public RequestSO RequestData
    {
        get { return requestData; }
        set { requestData = value; }
    }

    public event Action<RequestCardData> OnCardClicked;

    void Awake(){

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(RequestSO request){
        requestData = request;
        requesterText.text = requestData.requesterName;
        requesterStatus.text = request.chatStatus;
        portraitImage.sprite = requestData.portrait;
        
        if(!requestData.IsRead){
            noticeSymbol.SetActive(true);
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
