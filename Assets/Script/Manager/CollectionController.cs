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
    [SerializeField] TextMeshProUGUI schoolText;
    [SerializeField] TextMeshProUGUI PHYText;
    [SerializeField] TextMeshProUGUI INTText;
    [SerializeField] TextMeshProUGUI COMText;
    [SerializeField] TextMeshProUGUI STAText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI progression;
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] TextMeshProUGUI skillDescText;
    [SerializeField] Image skillIcon;
    [SerializeField] Image artwork;
    [SerializeField] Image schoolBanner;
    [SerializeField] Image PHYBar;
    [SerializeField] Image INTBar;
    [SerializeField] Image COMBar;
    [SerializeField] Image STABar;
    [SerializeField] Image progressBar;
    [SerializeField] GameObject unknownDetail;
    [SerializeField] GameObject unknownSkill;

    public SceneManager sceneManager;

    void Start()
    {
        UpdateCollectionUI(0);
    }
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Setting")
        {
            if (GameObject.FindGameObjectsWithTag("Gameplay Elements") != null)
            {
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Gameplay Elements"))
                {
                    Destroy(gameObject);
                }
            }
        }
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
        skillIcon.sprite = students[i].skillIcon;
        skillNameText.text = students[i].skillName;
        skillDescText.text = students[i].skillDescription;

        switch(students[i].school){
            case School.RedWinter:
                schoolText.text = "Red Winter";
                break;
            default:
                schoolText.text = students[i].school.ToString();
                break;
        }
        
        if (students[i].artwork != null)
        {
            artwork.sprite = students[i].artwork;
            artwork.gameObject.SetActive(false);
            artwork.gameObject.SetActive(true);
        }
        UpdateStatusBar(i);
        UpdateBanner(i);
        UpdateProgress();
        HideUncollectedStudent(i);
    }

    void UpdateStatusBar(int i)
    {
        PHYBar.fillAmount = (float)students[i].phyStat / 60f;
        INTBar.fillAmount = (float)students[i].intStat / 60f;
        COMBar.fillAmount = (float)students[i].comStat / 60f;
        STABar.fillAmount = (float)students[i].stamina / 150f;
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
        schoolBanner.gameObject.SetActive(false);
        schoolBanner.gameObject.SetActive(true);
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
        progressBar.fillAmount = (float)collected / 50f;
    }

    void HideUncollectedStudent(int i)
    {
        if (!students[i].Collected)
        {
            unknownDetail.SetActive(true);
            unknownSkill.SetActive(true);
            artwork.color = Color.black;
            nameText.text = "???";
            clubText.text = "???";
            descriptionText.text = "Find more in the Gacha!";
        }
        else
        {
            unknownDetail.SetActive(false);
            unknownSkill.SetActive(false);
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

    public void Back(){
        sceneManager.LoadPreviousScene();
    }
}
