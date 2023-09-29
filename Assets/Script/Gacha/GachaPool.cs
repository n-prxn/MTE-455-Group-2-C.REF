using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using Unity.VisualScripting;

[System.Serializable]
public class GachaPool : MonoBehaviour, IData
{
    [Header("UI")]
    [SerializeField] TMP_Text rollCountText;
    [SerializeField] GameObject gachaCard;
    [SerializeField] GameObject gachaCardParent;

    [Header("Setting")]
    private int rollCost; //Move to GameManeger
    [SerializeField] List<Student> studentsPool;
    public List<Student> StudentsPool { get => studentsPool; set => studentsPool = value; }

    [SerializeField] float commonRate = 78.5f;
    [SerializeField] float uncommonRate = 18.5f;
    [SerializeField] float rareRate = 3f;

    [Header("UI Panel")]
    [SerializeField] GameObject gachaScene;
    [SerializeField] GameObject warningBox;

    float common = 0, uncommon = 0, rare = 0;
    int rollCount = 0;
    private List<Student> PulledStudents = new List<Student>();

    public static GachaPool instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        CountRarity(StudentsPool);
        InitializeGachaRate(StudentsPool);
    }

    void Start()
    {
        rollCost = GameManager.Instance.rollCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        rollCount = data.rollCount;
    }

    public void SaveData(ref GameData data)
    {
        data.rollCount = rollCount;
    }

    //Define how many of students in each rarity
    private void CountRarity(List<Student> students)
    {
        foreach (Student student in students)
        {
            switch (student.rarity)
            {
                case Rarity.Common:
                    common++;
                    break;
                case Rarity.Uncommon:
                    uncommon++;
                    break;
                case Rarity.Rare:
                    rare++;
                    break;
            }
        }
    }

    //Define the weight of gacha by calculating from Rate/Amount
    private void InitializeGachaRate(List<Student> students)
    {
        foreach (Student student in students)
        {
            switch (student.rarity)
            {
                case Rarity.Common:
                    student.GachaRate = commonRate / common;
                    break;
                case Rarity.Uncommon:
                    student.GachaRate = uncommonRate / uncommon;
                    break;
                case Rarity.Rare:
                    student.GachaRate = rareRate / rare;
                    break;
            }
        }
    }

    //Gacha Method to Finding what Player will get (Only 1)
    private int PullStudentIndex(List<Student> students)
    {
        float weightSum = 0f;
        for (int i = 0; i < students.Count; i++)
            weightSum += students[i].GachaRate;

        int lastIndex = students.Count - 1;
        int index = Random.Range(0, students.Count);
        while (index <= lastIndex)
        {
            if (Random.Range(0, weightSum) < students[index].GachaRate)
            {
                break;
            }
            // weightSum -= students[index].GachaRate;
            index = Random.Range(0, students.Count);
        }
        return index;
    }

    //Pull by Amount
    public void Pull(int pullAmount)
    {
        if (GameManager.Instance.pyroxenes >= rollCost * pullAmount)
        {
            ToggleGachaScene();
            GameManager.Instance.pyroxenes -= rollCost * pullAmount;
            DeleteAllGachaResult();
            PulledStudents.Clear();
            for (int i = 0; i < pullAmount; i++)
            {
                Student pulledStudent = StudentsPool[PullStudentIndex(StudentsPool)];
                PulledStudents.Add(pulledStudent);

                GameObject card = Instantiate(gachaCard, gachaCardParent.transform);
                card.GetComponent<GachaCardDisplay>().student = pulledStudent;
                card.GetComponent<GachaCardDisplay>().UpdateGachaCard();

                if (!pulledStudent.SquadCollect)
                {
                    SquadController.instance.Receive(pulledStudent);
                    pulledStudent.SquadCollect = true;
                }
                else
                {
                    GameManager.Instance.pyroxenes += Mathf.FloorToInt(GameManager.Instance.rollCost / 2f);
                }
                pulledStudent.Collected = true;

                rollCount++;
            }

            //DataManager.instance.SaveGame();
        }
    }

    public void GauranteePull()
    {
        if (GameManager.Instance.pyroxenes >= rollCost)
        {
            ToggleGachaScene();
            GameManager.Instance.pyroxenes -= rollCost;
            DeleteAllGachaResult();
            PulledStudents.Clear();

            Student pulledStudent;
            do{
                pulledStudent = StudentsPool[PullStudentIndex(StudentsPool)];
            }while(pulledStudent.rarity != Rarity.Rare);

            PulledStudents.Add(pulledStudent);

            GameObject card = Instantiate(gachaCard, gachaCardParent.transform);
            card.GetComponent<GachaCardDisplay>().student = pulledStudent;
            card.GetComponent<GachaCardDisplay>().UpdateGachaCard();

            if (!pulledStudent.SquadCollect)
            {
                SquadController.instance.Receive(pulledStudent);
                pulledStudent.SquadCollect = true;
            }
            else
            {
                GameManager.Instance.pyroxenes += Mathf.FloorToInt(GameManager.Instance.rollCost / 2f);
            }
            pulledStudent.Collected = true;
            rollCount++;

            GameManager.Instance.setting.isGuaranteePull = true;

            //DataManager.instance.SaveGame();
        }
    }

    //Delete Gacha Result
    public void DeleteAllGachaResult()
    {
        if (gachaCardParent.transform.childCount > 0)
        {
            foreach (Transform card in gachaCardParent.transform)
            {
                Destroy(card.gameObject);
            }
        }
    }

    //Dev tool
    public void AddPyroxene(int pyroxenes)
    {
        GameManager.Instance.pyroxenes += pyroxenes;
    }

    public void SaveBTN()
    {
        DataManager.instance.SaveGame();
    }

    public void ClearBTN()
    {
        this.rollCount = 0;
        foreach (Student student in StudentsPool)
        {
            student.SquadCollect = false;
        }
    }
    public void ClearColletedBTN()
    {
        foreach (Student student in StudentsPool)
        {
            student.Collected = false;
        }
        DataManager.instance.ClearColleted();
    }

    public void ToggleGachaScene()
    {
        if (!gachaScene.activeSelf)
        {
            gachaScene.SetActive(true);
        }
    }
}
