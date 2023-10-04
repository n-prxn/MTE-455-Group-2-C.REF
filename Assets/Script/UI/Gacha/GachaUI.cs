using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaUI : MonoBehaviour
{
    [SerializeField] GameObject gauranteeButton;
    [SerializeField] TMP_Text currentElephs;
    private void OnEnable()
    {
        if (GameManager.Instance.setting.isGuaranteePull)
            gauranteeButton.SetActive(false);
        else
            gauranteeButton.SetActive(true);
    }

    private void Update() {
        currentElephs.text = GameManager.Instance.elephs.ToString();
    }
}
