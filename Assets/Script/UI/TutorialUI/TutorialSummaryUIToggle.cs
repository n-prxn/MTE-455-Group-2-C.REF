using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSummaryUIToggle : MonoBehaviour
{
    [SerializeField] Toggle toggleNextBTN;
    [SerializeField] Toggle togglePreBTN;
    [SerializeField] private GameObject BTN;
    [SerializeField] private GameObject thisBTN;

    // private void OnEnable()
    // {
    //     toggleNextBTN.isOn = true;
    // }
    // private void OnDisable()
    // {
    //     toggleNextBTN.isOn = true;
    // }
    // private void Start()
    // {
    //     toggleNextBTN.isOn = true;
    // }

    private void Update()
    {
        if (toggleNextBTN.isOn && !togglePreBTN.isOn)
        {
            thisBTN.SetActive(true);
            BTN.SetActive(false);
        }
        else if (!toggleNextBTN.isOn && togglePreBTN.isOn)
        {
            thisBTN.SetActive(false);
            BTN.SetActive(true);
        }
        else
        {
            thisBTN.SetActive(true);
            BTN.SetActive(true);
        }
    }
}
