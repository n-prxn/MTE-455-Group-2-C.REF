using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Drawing;

public class ItemCardData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ItemSO itemSO;
    public ItemSO ItemSO { get => itemSO; set => itemSO = value; }

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text amountText;
    public event Action<ItemCardData> OnItemCardClicked;

    public void SetData(ItemSO itemSO){
        this.itemSO = itemSO;
        itemImage.sprite = itemSO.sprite;
        amountText.text = itemSO.Amount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PointerEventData pointerEventData = eventData;
        if(pointerEventData.button == PointerEventData.InputButton.Left){
            OnItemCardClicked.Invoke(this);
        }
    }
}
