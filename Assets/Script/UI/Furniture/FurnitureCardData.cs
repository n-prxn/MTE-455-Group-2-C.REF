using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurnitureCardData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Furniture furniture;
    private GameObject furniturePrefab;
    public GameObject FurniturePrefab{
        get { return furniturePrefab;}
        set { furniturePrefab = value;}
    }
    public Furniture Furniture{
        get { return furniture;}
        set { furniture = value;}
    }
    [SerializeField] private Image furnitureImage;
    public event Action<FurnitureCardData> OnFurnitureCardClicked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(Furniture furniture, GameObject prefab){
        this.furniture = furniture;
        furniturePrefab = prefab;
        furnitureImage.sprite = furniture.FurnitureSprite;
        if(furniture.IsPlaced){
            furnitureImage.color = Color.black;
        }else{
            furnitureImage.color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        PointerEventData pointerEventData = eventData;
        if(pointerEventData.button == PointerEventData.InputButton.Left){
            OnFurnitureCardClicked.Invoke(this);
        }
    }
}
