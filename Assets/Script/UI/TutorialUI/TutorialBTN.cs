using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialBTN : MonoBehaviour
{
    public void ClickTagetBTN()
    {
        TutorialManeger.instance.NextTutorial();
    }
}
