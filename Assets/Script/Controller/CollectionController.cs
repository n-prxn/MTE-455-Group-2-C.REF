using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionController : MonoBehaviour
{
    [Header("Students")]
    [SerializeField] List<Student> students;
    private int currentStudentID = 0;

    [Header("Banner")]
    [SerializeField] List<Sprite> banners;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI clubText;
    [SerializeField] TextMeshProUGUI PHYText;
    [SerializeField] TextMeshProUGUI INTText;
    [SerializeField] TextMeshProUGUI COMText;
    [SerializeField] TextMeshProUGUI STAText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI progression;
    [SerializeField] Image artwork;
    [SerializeField] Image schoolBanner;
    [SerializeField] Image PHYBar;
    [SerializeField] Image INTBar;
    [SerializeField] Image COMBar;
    [SerializeField] Image STABar;
    [SerializeField] Image progressBar;

    void Start()
    {
        UpdateCollectionUI(0);
    }
    void Update()
    {

    }

    void UpdateCollectionUI(int i)
    {
        nameText.text = students[i].name;
        clubText.text = students[i].club;
        PHYText.text = students[i].phyStat.ToString();
        INTText.text = students[i].intStat.ToString();
        COMText.text = students[i].comStat.ToString();
        STAText.text = students[i].stamina.ToString();
        descriptionText.text = students[i].detail;
        if (students[i].artwork != null)
        {
            artwork.sprite = students[i].artwork;
        }
        UpdateStatusBar(i);
        UpdateBanner(i);
        UpdateProgress();
        HideUncollectedStudent(i);
    }

    void UpdateStatusBar(int i)
    {
        PHYBar.GetComponent<RectTransform>().sizeDelta = new Vector2(students[i].phyStat * 350f / 60f, 25);
        INTBar.GetComponent<RectTransform>().sizeDelta = new Vector2(students[i].intStat * 350f / 60f, 25);
        COMBar.GetComponent<RectTransform>().sizeDelta = new Vector2(students[i].comStat * 350f / 60f, 25);
        STABar.GetComponent<RectTransform>().sizeDelta = new Vector2(students[i].stamina * 350f / 150f, 25);
    }

    void UpdateBanner(int i)
    {
        switch (students[i].school)
        {
            case School.Abydos:
                schoolBanner.sprite = banners[0];
                break;
            case School.Arius:
                schoolBanner.sprite = banners[1];
                break;
            case School.Gehenna:
                schoolBanner.sprite = banners[2];
                break;
            case School.Hyakkiyako:
                schoolBanner.sprite = banners[3];
                break;
            case School.Millennium:
                schoolBanner.sprite = banners[4];
                break;
            case School.RedWinter:
                schoolBanner.sprite = banners[5];
                break;
            case School.Shanhaijin:
                schoolBanner.sprite = banners[6];
                break;
            case School.SRT:
                schoolBanner.sprite = banners[7];
                break;
            case School.Trinity:
                schoolBanner.sprite = banners[8];
                break;
            case School.Valkyrie:
                schoolBanner.sprite = banners[9];
                break;
            default:
                break;
        }
    }

    void UpdateProgress()
    {
        int collected = 0;
        foreach (Student student in students)
        {
            if (student.Collected)
                collected++;
        }
        progression.text = collected.ToString() + "/" + students.Count.ToString();
        progressBar.rectTransform.sizeDelta = new Vector2(collected * 350f / 50f, 25);
    }

    void HideUncollectedStudent(int i)
    {
        if(!students[i].Collected)
        {
            artwork.color = Color.black;
            nameText.text = "???";
            clubText.text = "???";
            descriptionText.text = "Find more in the Gacha!";
        }else{
            artwork.color = Color.white;
        }
    }

    public void NextStudentCollection()
    {
        if (currentStudentID < students.Count - 1)
        {
            currentStudentID++;
        }
        else
        {
            currentStudentID = 0;
        }
        UpdateCollectionUI(currentStudentID);
    }

    public void PreviousStudentCollection()
    {
        if (currentStudentID > 0)
        {
            currentStudentID--;
        }
        else
        {
            currentStudentID = students.Count - 1;
        }
        UpdateCollectionUI(currentStudentID);
    }

    /*public void LoadData(GameData data)
    {
        foreach (Student student in students)
        {
            bool studentCollected;
            data.studentCollected.TryGetValue(student.id, out studentCollected);
            student.Collected = studentCollected;
        }
    }

    public void SaveData(ref GameData data)
    {

    }*/
}
