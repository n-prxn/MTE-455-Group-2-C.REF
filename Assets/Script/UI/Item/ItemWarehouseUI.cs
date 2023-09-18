using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWarehouseUI : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardParent;
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private GameObject idlePanel;

    [SerializeField] private ItemDescription itemDescription;
    private ItemSO currentItem;

    void OnEnable(){
        InitializeWarehouse();
    }

    void InitializeWarehouse(){
        ResetWarehouse();
        currentItem = null;
        foreach(ItemSO item in InventoryManager.instance.PresentList){
            GameObject itemCard = Instantiate(cardPrefab,cardParent.transform);
            ItemCardData itemCardData = itemCard.GetComponent<ItemCardData>();
            itemCardData.SetData(item);
            itemCardData.OnItemCardClicked += HandleCardSelection;

            if(itemCardData.ItemSO.Amount <= 0){
                Destroy(itemCard);
            }
        }

        descriptionPanel.SetActive(false);
        idlePanel.SetActive(true);
    }

    void ResetWarehouse(){
        if(cardParent.transform.childCount > 0){
            foreach(Transform card in cardParent.transform)
                Destroy(card.gameObject);
        }
    }

    void HandleCardSelection(ItemCardData obj){
        descriptionPanel.SetActive(true);
        idlePanel.SetActive(false);

        itemDescription.SetDescription(obj.ItemSO);
        currentItem = obj.ItemSO;
    }

    public void UseItem(){
        currentItem.skill.PerformSkill(currentItem);
        currentItem.Amount--;
        InitializeWarehouse();
    }
}
