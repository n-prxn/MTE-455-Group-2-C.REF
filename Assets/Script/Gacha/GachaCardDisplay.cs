using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaCardDisplay : MonoBehaviour
{
    [SerializeField] private Image gachaPortrait;
    [SerializeField] private Image gachaBackground;

    [SerializeField] private Sprite[] BG;
    public Student student;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateGachaCard();
    }

    private void UpdateGachaCard()
    {
        GachaBGSelect();
        gachaPortrait.sprite = student.portrait;
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
}
