using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaSceneUI : MonoBehaviour
{
    [SerializeField] TMP_Text currentElephs;
    [SerializeField] RadioManager radioManager;

    // Update is called once per frame
    void Update()
    {
        currentElephs.text = GameManager.Instance.elephs.ToString();
    }

    private void OnDisable() {
        radioManager.ShuffleMusic();
    }
}
