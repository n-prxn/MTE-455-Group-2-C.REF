using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaUI : MonoBehaviour
{
    [SerializeField] GameObject gauranteeButton;
    [SerializeField] TMP_Text currentElephs;
    [SerializeField] Toggle toggleNextBTN;
    private void OnEnable()
    {
        if (GameManager.Instance.setting.isGuaranteePull)
            gauranteeButton.SetActive(false);
        else
            gauranteeButton.SetActive(true);
            
        if (!toggleNextBTN.isOn)
        {
            toggleNextBTN.isOn = true;
        }
    }

    private void Update()
    {
        currentElephs.text = GameManager.Instance.elephs.ToString();
    }
}
