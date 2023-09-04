using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureDescription : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private Image furnitureImage;
    [SerializeField] private TextMeshProUGUI furnitureName;
    [SerializeField] private TextMeshProUGUI furnitureDesc;

    [Header("Button")]
    [SerializeField] private Button placeButton;
    [SerializeField] private Button removeButton;
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

    public void SetDescription(Furniture furniture)
    {
        furnitureImage.sprite = furniture.FurnitureSprite;
        furnitureName.text = furniture.Name;
        furnitureDesc.text = furniture.Description;
    }

    public void ResetDescription()
    {
        furnitureName.text = "";
        furnitureDesc.text = "";
    }
}
