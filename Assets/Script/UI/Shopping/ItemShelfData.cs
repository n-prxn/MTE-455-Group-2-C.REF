using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public abstract class ItemShelfData : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI itemNameText;
    [SerializeField] protected TextMeshProUGUI itemDescText;
    [SerializeField] protected TextMeshProUGUI costText;
    [SerializeField] protected Image itemImage;
    [SerializeField] protected GameObject buyButton;
    [SerializeField] protected GameObject soldButton;

    protected bool isPurchased = false;
    public bool IsPurchased
    {
        get { return isPurchased; }
        set { isPurchased = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
