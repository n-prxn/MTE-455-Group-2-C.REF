using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemShopUI : MonoBehaviour
{
    [Header("Shelf")]
    [SerializeField] protected GameObject shelfPrefab;
    [SerializeField] protected GameObject shelfParent;

    protected GameObject currentSelectedItem;
    public GameObject CurrentSelectedItem{
        get{ return currentSelectedItem;}
        set{ currentSelectedItem = value;}
    }

    public void ResetShelf(){
        if(shelfParent.transform.childCount > 0){
            foreach(Transform shelf in shelfParent.transform){
                Destroy(shelf.gameObject);
            }
        }
    }
}
