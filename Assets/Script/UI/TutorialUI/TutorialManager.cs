using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Tutorial
{
    public GameObject tutorialPanel;
    public TutorialDialogueBox tutorialDialogueBox;
    public Button clickButton;
}
public class TutorialManager : MonoBehaviour
{
    [SerializeField] Tutorial[] tutorials;
    [SerializeField] private int tutorialIndex;
    public int TutorialIndex { get => tutorialIndex; set => tutorialIndex = value; }

    [SerializeField] private SettingSO setting;

    static public TutorialManager instance;


    private void Awake()
    {
        instance = this;
        tutorialIndex = 0;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        GameManager.Instance.uiIsOpen = true;
        tutorials[tutorialIndex].tutorialPanel.SetActive(true);
    }

    void Update()
    {
        if (tutorials[tutorialIndex].tutorialDialogueBox != null)
        {
            if (!tutorials[tutorialIndex].tutorialDialogueBox.canShowNextTutorial)
            {
                if (tutorials[tutorialIndex].clickButton != null)
                    tutorials[tutorialIndex].clickButton.interactable = false;
            }
            else
            {
                if (tutorials[tutorialIndex].clickButton != null)
                    tutorials[tutorialIndex].clickButton.interactable = true;
            }
        }
    }

    public void NextTutorial()
    {
        GameObject tutorialPanel;
        tutorialPanel = tutorials[tutorialIndex].tutorialPanel;

        Debug.Log(tutorialPanel.name);
        tutorialPanel.SetActive(false);
        tutorialIndex++;

        if (tutorialIndex > tutorials.Length - 1)
        {
            setting.hasPlayTutorial = true;
            tutorialIndex = 0;
            gameObject.SetActive(false);
            return;
        }

        if (tutorialIndex == 2)
        {
            gameObject.GetComponent<Image>().enabled = false;
            return;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = true;
        }

        if (tutorialIndex == 11)
            return;

        if (tutorialPanel != null)
        {
            tutorialPanel = tutorials[tutorialIndex].tutorialPanel;
            tutorialPanel.SetActive(true);
        }
    }

    public void ShowTutorial(int index)
    {
        if (tutorials[index].tutorialPanel == null)
            return;

        tutorials[index].tutorialPanel.SetActive(true);
        gameObject.GetComponent<Image>().enabled = true;
        tutorialIndex = index;
    }

    private void OnDisable()
    {
        GameManager.Instance.uiIsOpen = false;
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
