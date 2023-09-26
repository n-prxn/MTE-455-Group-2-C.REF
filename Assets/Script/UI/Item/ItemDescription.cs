using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemSkillDesc;
    
    [Header("Button")]
    [SerializeField] private Button usageButton;
    // Start is called before the first frame update
    void Awake()
    {
        ResetDescription();
    }

    public void SetDescription(ItemSO item){
        itemImage.sprite = item.sprite;
        itemName.text = item.name;
        itemDesc.text = item.description;
        itemSkillDesc.text = item.skillDescription;
    }

    public void ResetDescription(){
        itemName.text = "";
        itemDesc.text = "";
        itemSkillDesc.text = "";
    }
}
