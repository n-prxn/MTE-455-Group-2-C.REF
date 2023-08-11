using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class GachaPool : MonoBehaviour
{
    [SerializeField] TMP_Text rollCountText;
    [SerializeField] int rollCost = 120;
    [SerializeField] GameObject gachaCard;
    [SerializeField] GameObject gachaCardParent;
    [SerializeField] List<Student> studentsPool;

    [SerializeField] float commonRate = 78.5f;
    [SerializeField] float uncommonRate = 18.5f;
    [SerializeField] float rareRate = 3f;

    float common = 0, uncommon = 0, rare = 0;
    int rollCount = 0;

    private List<Student> PulledStudents = new List<Student>();

    // Start is called before the first frame update
    void Awake()
    {
        CountRarity(studentsPool);
        InitializeGachaRate(studentsPool);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rollCountText.text = "Roll : " + rollCount.ToString();
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

        int index = 0;
        int lastIndex = students.Count - 1;
        while (index < lastIndex)
        {
            if (Random.Range(0, weightSum) < students[index].GachaRate)
            {
                return index;
            }
            weightSum -= students[index++].GachaRate;
        }

        return index;
    }

    //Pull 1 Roll
    public void PullOne()
    {
        if (GameManager.instance.pyroxenes >= 120)
        {
            GameManager.instance.pyroxenes -= rollCost; //Edit Amount
            DeleteAllGachaResult();
            PulledStudents.Clear();

            //Edit roll count
            PulledStudents.Add(studentsPool[PullStudentIndex(studentsPool)]);
            GameObject card = Instantiate(gachaCard, gachaCardParent.transform);
            card.GetComponent<GachaCardDisplay>().student = PulledStudents[0];

            rollCount++;
            SaveIntoJson();
        }
    }

    //Pull 10 Roll
    public void PullTen()
    {
        if (GameManager.instance.pyroxenes >= 1200)
        {
            GameManager.instance.pyroxenes -= rollCost * 10;
            DeleteAllGachaResult();
            PulledStudents.Clear();
            for (int i = 0; i < 10; i++)
            {
                PulledStudents.Add(studentsPool[PullStudentIndex(studentsPool)]);
            }
            foreach (Student pulledStudent in PulledStudents)
            {
                GameObject card = Instantiate(gachaCard, gachaCardParent.transform);
                card.GetComponent<GachaCardDisplay>().student = pulledStudent;
            }
            rollCount += 10;
            SaveIntoJson();
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
        GameManager.instance.pyroxenes += pyroxenes;
    }

    //Save Gacha History To Log
    public void SaveIntoJson()
    {
        string documentName = Application.dataPath + "/gachaLog.txt";
        if (!File.Exists(documentName))
        {
            File.WriteAllText(documentName, "Gacha Log\n\n");
        }

        string strOuput = JsonConvert.SerializeObject(PulledStudents, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        File.AppendAllText(documentName, strOuput);

    }
}
