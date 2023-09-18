using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SideButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject hoverBox;

    void Start(){
        hoverBox.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
