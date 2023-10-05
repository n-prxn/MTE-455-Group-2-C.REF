using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaToggleUI : MonoBehaviour
{
    [SerializeField] private GameObject BTN;
    private void Awake()
    {
        this.GetComponent<Toggle>().onValueChanged.AddListener(
            delegate (bool isOn)
                {
                    BTN.SetActive(isOn);
                }
            );
    }
}
