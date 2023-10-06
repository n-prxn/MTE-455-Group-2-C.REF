using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManeger : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialPanal;
    [SerializeField] private int tutorialIndex;
    static public TutorialManeger instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        tutorialIndex = 0;
        tutorialPanal[tutorialIndex].SetActive(true);
    }

    public void NextTutorial()
    {
        tutorialPanal[tutorialIndex].SetActive(false);

        if (tutorialPanal[++tutorialIndex] != null)
            tutorialPanal[tutorialIndex].SetActive(true);
    }
}
