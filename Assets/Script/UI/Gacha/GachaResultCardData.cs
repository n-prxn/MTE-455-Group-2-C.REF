using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaResultCardData : MonoBehaviour
{
    public int resultIndex;
    [Header("General Info")]
    [SerializeField] TextMeshProUGUI studentNameText;
    [SerializeField] TextMeshProUGUI schoolNameText;
    [SerializeField] TextMeshProUGUI clubNameText;
    [Header("Status Bar")]
    [SerializeField] Image PHYBar;
    [SerializeField] Image INTBar;
    [SerializeField] Image COMBar;
    [SerializeField] Image staminaBar;

    [Header("Status Text")]
    [SerializeField] TextMeshProUGUI PHYText;
    [SerializeField] TextMeshProUGUI INTText;
    [SerializeField] TextMeshProUGUI COMText;
    [SerializeField] TextMeshProUGUI staminaText;

    [Header("Skill")]
    [SerializeField] Image skillIcon;
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] TextMeshProUGUI skillDescText;

    [Header("Image")]
    [SerializeField] GameObject newIcon;
    [SerializeField] Image artworkImage;
    [SerializeField] Image bannerImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] Image rarityAuraImage;

    [Header("Datas")]
    [SerializeField] Sprite[] banners;
    [SerializeField] Sprite[] backgrounds;

    private GameObject gachaAllResultScene;
    //[SerializeField] TextMeshProUGUI 
    // Start is called before the first frame update
    void Awake()
    {
        gachaAllResultScene = GameObject.Find("Gacha").GetComponent<GachaPool>().gachaAllResultScene;
    }

    public void SetDetail(Student s, bool isNew, int index)
    {
        artworkImage.sprite = s.artwork;
        studentNameText.text = s.name;
        schoolNameText.text = s.school.ToString();
        PHYBar.fillAmount = s.phyStat / 60f;
        INTBar.fillAmount = s.intStat / 60f;
        COMBar.fillAmount = s.comStat / 60f;
        staminaBar.fillAmount = s.stamina / 150f;

        PHYText.text = s.phyStat.ToString();
        INTText.text = s.intStat.ToString();
        COMText.text = s.comStat.ToString();

        skillIcon.sprite = s.skillIcon;
        skillNameText.text = s.skillName;
        skillDescText.text = s.skillDescription;

        SetBannerAndBackground(s);
        newIcon.SetActive(isNew);

        resultIndex = index;
        switch (s.rarity)
        {
            case Rarity.Common:
                rarityAuraImage.color = new Color32(120, 229, 255, 200);
                break;
            case Rarity.Uncommon:
                rarityAuraImage.color = new Color32(255, 175, 16, 200);
                break;
            case Rarity.Rare:
                rarityAuraImage.color = new Color32(236, 119, 255, 200);
                break;
        }
    }

    public void SetBannerAndBackground(Student s)
    {
        switch (s.school)
        {
            case School.Abydos:
                bannerImage.sprite = banners[0];
                backgroundImage.sprite = backgrounds[0];
                break;
            case School.Arius:
                bannerImage.sprite = banners[1];
                backgroundImage.sprite = backgrounds[1];
                break;
            case School.Gehenna:
                bannerImage.sprite = banners[2];
                backgroundImage.sprite = backgrounds[2];
                break;
            case School.Hyakkiyako:
                bannerImage.sprite = banners[3];
                backgroundImage.sprite = backgrounds[3];
                break;
            case School.Millennium:
                bannerImage.sprite = banners[4];
                backgroundImage.sprite = backgrounds[4];
                break;
            case School.RedWinter:
                bannerImage.sprite = banners[5];
                backgroundImage.sprite = backgrounds[5];
                break;
            case School.Shanhaijin:
                bannerImage.sprite = banners[6];
                backgroundImage.sprite = backgrounds[6];
                break;
            case School.SRT:
                bannerImage.sprite = banners[7];
                backgroundImage.sprite = backgrounds[7];
                break;
            case School.Trinity:
                bannerImage.sprite = banners[8];
                backgroundImage.sprite = backgrounds[8];
                break;
            case School.Valkyrie:
                bannerImage.sprite = banners[9];
                backgroundImage.sprite = backgrounds[9];
                break;
            default:
                break;
        }
    }

    public void nextResult()
    {
        if (resultIndex + 1 >= gameObject.transform.parent.childCount)
        {
            gachaAllResultScene.SetActive(true);
            gameObject.SetActive(false);
            return;
        }

        Transform nextResult = gameObject.transform.parent.GetChild(resultIndex + 1);
        nextResult.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
