using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaCardDisplay : MonoBehaviour
{
    [SerializeField] private Image gachaPortrait;
    [SerializeField] private Image gachaBackground;
    [SerializeField] private Image gachaBackgroundPyroxenes;
    [SerializeField] private TMP_Text pyroxenesText;
    [SerializeField] private GameObject pyroxenesPanel;

    [SerializeField] private Sprite[] BG;
    public Student student;
    // Start is called before the first frame update
    void Start()
    {
        UpdateGachaCard();
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateGachaCard();
    }

    private void UpdateGachaCard()
    {
        GachaBGSelect();
        gachaPortrait.sprite = student.portrait;
        if (student.Collected)
        {
            GachaPyroxenesBGSelect();
            pyroxenesText.text = "×" + Mathf.FloorToInt(GameManager.instance.rollCost / 2f);
            pyroxenesPanel.SetActive(true);
            GameManager.instance.pyroxenes += Mathf.FloorToInt(GameManager.instance.rollCost / 2f);
        }else{
            SquadController.instance.Receive(student);
        }
        student.Collected = true;
    }

    private void GachaBGSelect()
    {
        switch (student.rarity)
        {
            case Rarity.Common:
                gachaBackground.sprite = BG[0];
                break;
            case Rarity.Uncommon:
                gachaBackground.sprite = BG[1];
                break;
            case Rarity.Rare:
                gachaBackground.sprite = BG[2];
                break;
        }
    }

    private void GachaPyroxenesBGSelect()
    {
        switch (student.rarity)
        {
            case Rarity.Common:
                gachaBackgroundPyroxenes.sprite = BG[0];
                break;
            case Rarity.Uncommon:
                gachaBackgroundPyroxenes.sprite = BG[1];
                break;
            case Rarity.Rare:
                gachaBackgroundPyroxenes.sprite = BG[2];
                break;
        }
    }
}
