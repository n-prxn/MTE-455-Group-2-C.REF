using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] int letterPerSeconds;
    [SerializeField] TextMeshProUGUI messageText;
    [TextArea][SerializeField] string[] message;
    [SerializeField] GameObject nextSign;
    [SerializeField] PlayableDirector playableDirector;
    private int currentMessageIndex = 0;

    bool isTyping = false;
    bool isEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        currentMessageIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTyping)
        {
            isTyping = true;
            StartCoroutine(TypeDialogue(message[currentMessageIndex]));
        }

        if (isEnded)
        {
            nextSign.SetActive(true);
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                currentMessageIndex++;
                if (currentMessageIndex > message.Length - 1)
                {
                    nextSign.SetActive(false);
                    playableDirector.Play();
                }
                else
                {
                    isTyping = false;
                    isEnded = false;
                    messageText.text = message[currentMessageIndex];
                }
            }
        }
        else
        {
            nextSign.SetActive(false);
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                if (currentMessageIndex < message.Length - 1)
                {
                    isTyping = true;
                    isEnded = true;
                    messageText.text = message[currentMessageIndex];
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
