using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

[System.Serializable]
public struct Dialogue
{
    public string name;
    [TextArea] public string message;
}

public class EndingDialogue : MonoBehaviour
{
    [SerializeField] int letterPerSeconds = 50;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Dialogue[] dialogues;
    [SerializeField] GameObject nextSign;
    [SerializeField] PlayableDirector playableDirector;
    public int currentMessageIndex = 0;
    bool isTyping = false;
    bool isEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        currentMessageIndex = 0;
        messageText.text = "";
        nameText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // if (BackgroundMove.instant != null)
        // {
        //     BackgroundMove.instant.myIndex = currentMessageIndex;
        // }

        if (!isTyping)
        {
            isTyping = true;
            nameText.text = dialogues[currentMessageIndex].name;
            StartCoroutine(TypeDialogue(dialogues[currentMessageIndex].message));
        }

        if (isEnded)
        {
            if (nextSign != null)
                nextSign.SetActive(true);
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                currentMessageIndex++;
                if (currentMessageIndex > dialogues.Length - 1)
                {
                    nextSign.SetActive(false);
                    playableDirector.Play();
                    Destroy(this);
                }
                else
                {
                    isTyping = false;
                    isEnded = false;
                    nameText.text = dialogues[currentMessageIndex].name;
                    messageText.text = dialogues[currentMessageIndex].message;
                }
            }
        }
        else
        {
            nextSign.SetActive(false);
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                if (currentMessageIndex < dialogues.Length - 1)
                {
                    isTyping = true;
                    isEnded = true;
                    nameText.text = dialogues[currentMessageIndex].name;
                    messageText.text = dialogues[currentMessageIndex].message;
                }

            }
        }
    }

    public IEnumerator TypeDialogue(string message)
    {
        messageText.text = "";
        foreach (var letter in message.ToCharArray())
        {
            if (isEnded)
                break;
            messageText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSeconds);
        }
        isEnded = true;
    }
}
