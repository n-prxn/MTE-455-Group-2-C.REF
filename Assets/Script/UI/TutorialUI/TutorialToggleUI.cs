using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialToggleUI : MonoBehaviour
{

    [SerializeField] private GameObject BTN;
    [SerializeField] private GameObject altBTN;
    [SerializeField] private Toggle altToggle;
    private void Awake()
    {
        this.GetComponent<Toggle>().onValueChanged.AddListener(
            delegate (bool isOnThis)
                {
                    if (!isOnThis && !altToggle.isOn)
                    {
                        BTN.SetActive(true);
                        altBTN.SetActive(true);
                    }
                    else
                    {
                        BTN.SetActive(isOnThis);
                        altBTN.SetActive(false);
                    }
                }
            );
    }
}
