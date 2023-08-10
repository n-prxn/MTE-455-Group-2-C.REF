using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        InitializeWeight(studentsPool);
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

    private void InitializeWeight(List<Student> students)
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

    public void PullOne()
    {
        if (GameManager.instance.pyroxenes >= 120)
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
            rollCount++;
        }
    }

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
        }
    }

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

    public void AddPyroxene(int pyroxenes){
        GameManager.instance.pyroxenes += pyroxenes;
    }
}
