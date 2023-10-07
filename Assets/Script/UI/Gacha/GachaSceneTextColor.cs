using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaSceneTextColor : MonoBehaviour
{
    [SerializeField] private TMP_Text pyroxenesText;
    [SerializeField] private TMP_Text elephsText;
    [SerializeField] private int cost;

    // Update is called once per frame
    void Update()
    {
        if (pyroxenesText != null)
        {
            if (GameManager.Instance.pyroxenes < cost)
            {
                pyroxenesText.color = Color.red;
            }
            else
            {
                pyroxenesText.color = Color.black;
            }
        }
        if (elephsText != null)
        {
            if (GameManager.Instance.elephs < cost)
            {
                elephsText.color = Color.red;
            }
            else
            {
                elephsText.color = new Color(255,242,0);
            }
        }
    }
}
