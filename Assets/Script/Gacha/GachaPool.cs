using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class GachaPool : MonoBehaviour, IData
{
    [Header("UI")]
    [SerializeField] TMP_Text rollCountText;
    [SerializeField] GameObject gachaCard;
    [SerializeField] GameObject gachaCardParent;

    [Header("UI BTN")]
    [SerializeField] GameObject normalGacha;
    [SerializeField] GameObject elephsGacha;

    [Header("Setting")]
    private int rollCost; //Move to GameManeger
    private int elephsCost;
    [SerializeField] List<Student> studentsPool;
    public List<Student> StudentsPool { get => studentsPool; set => studentsPool = value; }

    [Header("Gacha Rate")]
    [SerializeField] float commonRate = 78.5f;
    [SerializeField] float uncommonRate = 18.5f;
    [SerializeField] float rareRate = 3f;

    [Header("Return Elephs")]
    [SerializeField] int commonElephs;
    public int CommonElephs { get => commonElephs; }
    [SerializeField] int unCommonElephs;
    public int UncommonElephs { get => unCommonElephs; }
    [SerializeField] int rareElephs;
    public int RareElephs { get => rareElephs; }


    [Header("UI Panel")]
    [SerializeField] GameObject gachaScene;
    [SerializeField] GameObject gachaSplashScene;
    [SerializeField] GameObject warningBox;
    [SerializeField] GachaResultUI gachaResultPanel;
    public GameObject gachaAllResultScene;

    float common = 0, uncommon = 0, rare = 0;
    int rollCount = 0;
    private List<Student> pulledStudents = new List<Student>();
    public List<Student> PulledStudents { get => pulledStudents; set => pulledStudents = value; }
    private List<bool> isNewList = new();
    public List<bool> IsNewList { get => isNewList; set => isNewList = value; }

    bool hasRare = false;
    public bool HasRare { get => hasRare; set => hasRare = value; }

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
        elephsCost = GameManager.Instance.elephsCost;
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
        normalGacha.SetActive(true);
        elephsGacha.SetActive(false);

        if (GameManager.Instance.pyroxenes >= rollCost * pullAmount)
        {
            GameManager.Instance.pyroxenes -= rollCost * pullAmount;
            pulledStudents.Clear();
            for (int i = 0; i < pullAmount; i++)
            {
                Student pulledStudent = StudentsPool[PullStudentIndex(StudentsPool)];
                pulledStudents.Add(pulledStudent);

                if (!pulledStudent.SquadCollect)
                {
                    SquadController.instance.Receive(pulledStudent);
                    pulledStudent.SquadCollect = true;
                    isNewList.Add(true);
                }
                else
                {
                    switch (pulledStudent.rarity)
                    {
                        case Rarity.Common:
                            GameManager.Instance.elephs += commonElephs;
                            break;
                        case Rarity.Uncommon:
                            GameManager.Instance.elephs += unCommonElephs;
                            break;
                        case Rarity.Rare:
                            GameManager.Instance.elephs += rareElephs;
                            break;
                    }
                    isNewList.Add(false);
                }
                pulledStudent.Collected = true;

                if (pulledStudent.rarity == Rarity.Rare)
                    hasRare = true;

                rollCount++;
            }

            ToggleGachaScene();
            StartCoroutine(CreateGachaResultCards());
            GameObject.FindWithTag("Student Parent").GetComponent<StudentSpawner>().InitializeStudents();
            gachaResultPanel.SetResultButton(pullAmount);
            //DataManager.instance.SaveGame();
        }
    }

    public void GauranteePull()
    {
        normalGacha.SetActive(false);
        elephsGacha.SetActive(false);

        if (GameManager.Instance.pyroxenes >= rollCost)
        {
            GameManager.Instance.pyroxenes -= rollCost;
            pulledStudents.Clear();

            Student pulledStudent;
            do
            {
                pulledStudent = StudentsPool[PullStudentIndex(StudentsPool)];
            } while (pulledStudent.rarity != Rarity.Rare);

            pulledStudents.Add(pulledStudent);

            if (!pulledStudent.SquadCollect)
            {
                SquadController.instance.Receive(pulledStudent);
                pulledStudent.SquadCollect = true;
                isNewList.Add(true);
            }
            else
            {
                switch (pulledStudent.rarity)
                {
                    case Rarity.Common:
                        GameManager.Instance.elephs += commonElephs;
                        break;
                    case Rarity.Uncommon:
                        GameManager.Instance.elephs += unCommonElephs;
                        break;
                    case Rarity.Rare:
                        GameManager.Instance.elephs += rareElephs;
                        break;
                }
                isNewList.Add(false);
            }
            pulledStudent.Collected = true;
            rollCount++;

            hasRare = true;
            GameManager.Instance.setting.isGuaranteePull = true;

            ToggleGachaScene();
            StartCoroutine(CreateGachaResultCards());
            GameObject.FindWithTag("Student Parent").GetComponent<StudentSpawner>().InitializeStudents();
            gachaResultPanel.SetResultButton(1);
            //DataManager.instance.SaveGame();
        }
    }

    public void ElephsPull()
    {
        normalGacha.SetActive(false);
        elephsGacha.SetActive(true);

        if (GameManager.Instance.elephs >= elephsCost)
        {
            GameManager.Instance.elephs -= elephsCost;
            pulledStudents.Clear();

            Student pulledStudent;

            if (Random.Range(1, 101) % 2 == Random.Range(0, 2))
            {
                do
                {
                    pulledStudent = StudentsPool[PullStudentIndex(StudentsPool)];
                } while (pulledStudent.rarity != Rarity.Rare);
            }
            else
            {
                do
                {
                    pulledStudent = StudentsPool[PullStudentIndex(StudentsPool)];
                } while (pulledStudent.rarity != Rarity.Uncommon);
            }

            pulledStudents.Add(pulledStudent);

            if (!pulledStudent.SquadCollect)
            {
                SquadController.instance.Receive(pulledStudent);
                pulledStudent.SquadCollect = true;
                isNewList.Add(true);
            }
            else
            {
                switch (pulledStudent.rarity)
                {
                    case Rarity.Common:
                        GameManager.Instance.elephs += commonElephs;
                        break;
                    case Rarity.Uncommon:
                        GameManager.Instance.elephs += unCommonElephs;
                        break;
                    case Rarity.Rare:
                        GameManager.Instance.elephs += rareElephs;
                        break;
                }
                isNewList.Add(false);
            }
            pulledStudent.Collected = true;
            if (pulledStudent.rarity == Rarity.Rare)
                hasRare = true;

            ToggleGachaScene();
            StartCoroutine(CreateGachaResultCards());
            GameObject.FindWithTag("Student Parent").GetComponent<StudentSpawner>().InitializeStudents();
            gachaResultPanel.SetResultButton(1);
            //DataManager.instance.SaveGame();
        }
    }

    public IEnumerator CreateGachaResultCards()
    {
        yield return new WaitForSeconds(1f);
        DeleteAllGachaResult();
        for (int i = 0; i < pulledStudents.Count; i++)
        {
            GameObject card = Instantiate(gachaCard, gachaCardParent.transform);
            card.GetComponent<GachaCardDisplay>().student = pulledStudents[i];
            card.GetComponent<GachaCardDisplay>().UpdateGachaCard(isNewList[i]);
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
        // DataManager.instance.ClearColleted();
    }

    public void ToggleGachaScene()
    {
        gachaResultPanel.SetGachaHint(hasRare);
        gachaSplashScene.SetActive(true);
    }
}
