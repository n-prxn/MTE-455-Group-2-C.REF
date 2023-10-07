using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSummaryUIToggle : MonoBehaviour
{
    [SerializeField] Toggle toggleNextBTN;
    [SerializeField] Toggle toggleNextBTNAlt;
    [SerializeField] private GameObject BTN;
    [SerializeField] private GameObject thisBTN;

    private void OnDisable()
    {
        toggleNextBTN.isOn = true;
        // toggleNextBTNAlt.isOn = false;
        // thisBTN.SetActive(true);
        // BTN.SetActive(false);
    }
}
