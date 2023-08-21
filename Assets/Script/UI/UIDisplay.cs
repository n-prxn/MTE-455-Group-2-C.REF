using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Turn UI")]
    [SerializeField] TMP_Text turnText;

    [Header("Resource UI")]
    [SerializeField] TMP_Text creditAmountText;
    [SerializeField] TMP_Text pyroxeneAmountText;

    [Header("Player Info UI")]
    [SerializeField] TMP_Text rankText;
    [SerializeField] Image xpBar;
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
        //Update Turn
        turnText.text = GameManager.instance.currentTurn.ToString();

        //Update Resource
        creditAmountText.text = GameManager.instance.credits.ToString();
        pyroxeneAmountText.text = GameManager.instance.pyroxenes.ToString();

        //Update Rank
        rankText.text = "RANK " + GameManager.instance.rank.ToString();
        xpBar.fillAmount = (float)GameManager.instance.currentXP/(float)GameManager.instance.maxXP;
        //Debug.Log(GameManager.instance.currentXP/GameManager.instance.maxXP);
    }
}
