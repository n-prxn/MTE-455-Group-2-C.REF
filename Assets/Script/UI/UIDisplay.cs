using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text creditAmountText;
    [SerializeField] TMP_Text pyroxeneAmountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIResource();
    }

    void UpdateUIResource(){
        creditAmountText.text = GameManager.instance.credits.ToString();
        pyroxeneAmountText.text = GameManager.instance.pyroxenes.ToString();
    }
}
