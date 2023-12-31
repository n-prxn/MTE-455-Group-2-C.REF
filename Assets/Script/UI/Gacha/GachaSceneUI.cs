using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaSceneUI : MonoBehaviour
{
    [SerializeField] TMP_Text currentElephs;
    [SerializeField] RadioManager radioManager;
    [SerializeField] GachaPool gachaPool;

    // Update is called once per frame
    private void OnEnable() {
        gachaPool.isPullingGacha = false;
    }

    void Update()
    {
        currentElephs.text = GameManager.Instance.elephs.ToString();
    }

    private void OnDisable() {
        radioManager.ShuffleMusic();
    }
}
