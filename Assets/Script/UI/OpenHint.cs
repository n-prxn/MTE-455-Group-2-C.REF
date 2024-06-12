using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHint : MonoBehaviour
{
    public void ToggleHintUI(GameObject hintUI)
    {
        hintUI.SetActive(!hintUI.activeSelf);
    }
}
