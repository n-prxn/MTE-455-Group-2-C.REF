using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FurnitureShelfData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI furnitureNameText;
    [SerializeField] TextMeshProUGUI furnitureDescText;
    [SerializeField] TextMeshProUGUI costText;
    private Furniture furniture;
    private GameObject furniturePrefab;
    public GameObject FurniturePrefab
    {
        get { return furniturePrefab; }
        set { furniturePrefab = value; }
    }

    private bool isPurchased = false;
    public bool IsPurchased
    {
        get { return isPurchased; }
        set { isPurchased = value; }
    }

    [SerializeField] private Image furnitureImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(GameObject furniture)
    {
        furniturePrefab = furniture;
        this.furniture = furniture.GetComponent<Furniture>();

        furnitureNameText.text = this.furniture.Name;
        furnitureDescText.text = this.furniture.Description;
        costText.text = this.furniture.Cost.ToString();
        furnitureImage.sprite = this.furniture.FurnitureSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PointerEventData pointerEventData = eventData;
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {

        }
    }
}
