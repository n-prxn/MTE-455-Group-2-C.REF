using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialDialogueBox : MonoBehaviour
{
    [SerializeField] int letterPerSeconds;
    [SerializeField] TextMeshProUGUI messageText;
    [TextArea][SerializeField] string[] message;
    [SerializeField] GameObject nextSign;
    bool isTyping = false;
    bool isEnded = false;
    int currentMessageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentMessageIndex = 0;
        messageText.text = "";
        //nextSign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
         if (!isTyping)
        {
            isTyping = true;
            StartCoroutine(TypeDialogue(message[currentMessageIndex]));
        }

        if(isEnded){
            nextSign.SetActive(true);
            if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)){
                currentMessageIndex++;
                if(currentMessageIndex > message.Length - 1){
                    nextSign.SetActive(false);
                    gameObject.SetActive(false);
                }else{
                    isTyping = false;
                    isEnded = false;
                    messageText.text = message[currentMessageIndex];
                }
            }
        }else{
            nextSign.SetActive(false);
            if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)){
                isTyping = true;
                isEnded = true;
                messageText.text = message[currentMessageIndex];
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
