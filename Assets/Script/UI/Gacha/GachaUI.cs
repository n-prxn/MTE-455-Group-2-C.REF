using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaUI : MonoBehaviour
{
    [SerializeField] GameObject gauranteeButton;
    private void OnEnable()
    {
        if (GameManager.Instance.setting.isGuaranteePull)
            gauranteeButton.SetActive(false);
        else
            gauranteeButton.SetActive(true);
    }
}
