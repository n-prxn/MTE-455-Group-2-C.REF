using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Database", menuName = "Student/Skill Database")]
public class SkillDatabase : SkillSO
{
    Student currentStudent;
    //Added Your Student Skill Method
    public override void PerformSkill(Student student)
    {
        currentStudent = student;
        switch (student.id)
        {
            case 1:
                SkillYuuka();
                break;
            case 2:
                SkillShiroko();
                break;
            case 3:
                SkillHifumi();
                break;
            case 4:
                SkillChinatsu();
                break;
            case 5:
                SkillShizuko();
                break;
            case 6:
                SkillSaya();
                break;
            case 7:
                SkillMiyu();
                break;
            case 8:
                SkillKirino();
                break;
            case 9:
                SkillNodoka();
                break;
            case 10:
                SkillKoyuki();
                break;
            case 11:
                SkillSerika();
                break;
            case 12:
                SkillSuzumi();
                break;
            case 13:
                SkillFuuka();
                break;
            case 14:
                SkillChise();
                break;
            case 15:
                SkillKokona();
                break;
            case 16:
                SkillSaki();
                break;
            case 17:
                SkillFubuki();
                break;
            case 18:
                SkillMarina();
                break;
            case 19:
                SkillNoa();
                break;
            case 20:
                SkillAsuna();
                break;
            case 21:
                SkillHibiki();
                break;
            case 22:
                SkillNatsu();
                break;
            case 23:
                SkillKoharu();
                break;
            case 24:
                SkillMari();
                break;
            case 25:
                SkillAyane();
                break;
            case 26:
                SkillNonomi();
                break;
            case 27:
                SkillSena();
                break;
            case 28:
                SkillHaruna();
                break;
            case 29:
                SkillMutsuki();
                break;
            case 30:
                SkillIzuna();
                break;
            case 31:
                SkillCherino();
                break;
            case 32:
                SkillRio();
                break;
            case 33:
                SkillHimari();
                break;
            case 34:
                SkillNeru();
                break;
            case 35:
                SkillToki();
                break;
            case 36:
                SkillNagisa();
                break;
            case 37:
                SkillMika();
                break;
            case 38:
                SkillTsurugi();
                break;
            case 39:
                SkillAzusa();
                break;
            case 40:
                SkillHina();
                break;
            case 41:
                SkillAru();
                break;
            case 42:
                SkillIroha();
                break;
            case 43:
                SkillKayoko();
                break;
            case 44:
                SkillHoshino();
                break;
            case 45:
                SkillKizaki();
                break;
            case 46:
                SkillArisu();
                break;
            case 47:
                SkillSeia();
                break;
            case 48:
                SkillAko();
                break;
            case 49:
                SkillMiyako();
                break;
            case 50:
                SkillSaori();
                break;
            default:
                break;
        }
    }

    public void SkillYuuka()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        currentRequest.CurrentCredit += (int)(currentRequest.credit * 0.3f);
    }

    public void SkillShiroko()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        currentRequest.CurrentCredit += (int)(currentRequest.credit * 0.3f);
    }

    public void SkillHifumi()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        currentRequest.CurrentHappiness += 1;
    }

    public void SkillChinatsu()
    {
        if (currentStudent.IsTraining)
        {
            currentStudent.TrainedCOMStat++;
        }
    }

    public void SkillShizuko()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;

        if (currentRequest.happiness > 0)
        {
            currentRequest.CurrentHappiness += 1;
        }
    }

    public void SkillSaya()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;
        RequestManager.instance.StaminaComsumption += Random.Range(0, 1) == 0 ? 10 : -10;
    }

    public void SkillMiyu()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        bool hasMiyako = false, hasSaki = false;

        for (int i = 0; i < 4; i++)
        {
            if (currentRequest.squad[i] == null)
                continue;

            if (currentRequest.squad[i].id == 16)
                hasSaki = true;

            if (currentRequest.squad[i].id == 49)
                hasMiyako = true;
        }

        if (hasMiyako && hasSaki)
        {
            int phyStat = (int)(currentStudent.CurrentPHYStat * 1.5),
            intStat = (int)(currentStudent.CurrentINTStat * 1.5),
            comStat = (int)(currentStudent.CurrentCOMStat * 1.5);
            RequestManager.instance.AddAdditionalStatus(phyStat, intStat, comStat);
        }
    }

    public void SkillKirino()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestManager.instance.TotalCOMStat = (int)(RequestManager.instance.TotalCOMStat * 1.1f);
    }

    public void SkillNodoka()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestManager.instance.TotalPHYStat = (int)(RequestManager.instance.TotalPHYStat * 1.1f);
        RequestManager.instance.TotalINTStat = (int)(RequestManager.instance.TotalINTStat * 1.1f);
        RequestManager.instance.TotalCOMStat = (int)(RequestManager.instance.TotalCOMStat * 1.1f);

        RequestManager.instance.StaminaComsumption -= 5;
    }

    public void SkillKoyuki()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        bool hasYuuka = false, hasNoa = false;

        for (int i = 0; i < 4; i++)
        {
            if (currentRequest.squad[i] == null)
                continue;

            if (currentRequest.squad[i].id == 1)
                hasYuuka = true;

            if (currentRequest.squad[i].id == 19)
                hasNoa = true;
        }

        if (hasYuuka && hasNoa)
        {
            RequestManager.instance.AddAdditionalStatus(10, 10, 10);
        }
    }

    public void SkillSerika()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        currentRequest.CurrentCredit += (int)(currentRequest.credit * 0.75f);

        RequestManager.instance.StaminaComsumption = (int)(RequestManager.instance.StaminaComsumption * 1.2f);
    }

    public void SkillSuzumi()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestManager.instance.TotalPHYStat = (int)(RequestManager.instance.TotalPHYStat * 1.1f);
    }

    public void SkillFuuka()
    {
        if (currentStudent.IsTraining && TrainingManager.instance.isStudentAtBuilding(BuildingType.Dormitory, currentStudent))
        {
            foreach (Student student in TrainingManager.instance.TrainingGroup[BuildingType.Dormitory])
            {
                if (student == null)
                    continue;

                student.RestedStamina++;
            }
        }
    }

    public void SkillChise()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        int happiness = 0;
        for (int i = 0; i < 4; i++)
        {
            if (currentRequest.squad[i] == null)
                continue;

            if (currentRequest.squad[i].school == School.Hyakkiyako)
                happiness++;
        }

        currentRequest.CurrentHappiness += happiness;
    }

    public void SkillKokona()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        bool hasKizaki = false;

        for (int i = 0; i < 4; i++)
        {
            if (currentRequest.squad[i] == null)
                continue;

            if (currentRequest.squad[i].id == 45)
                hasKizaki = true;
        }

        if (hasKizaki)
        {
            RequestManager.instance.AddAdditionalStatus(
                (int)(RequestManager.instance.TotalPHYStat * 0.1f),
                (int)(RequestManager.instance.TotalINTStat * 0.1f),
                (int)(RequestManager.instance.TotalCOMStat * 0.1f));
        }
    }

    public void SkillSaki()
    {
        if (currentStudent.IsTraining && TrainingManager.instance.isStudentAtBuilding(BuildingType.Gym, currentStudent))
        {
            foreach (Student student in TrainingManager.instance.TrainingGroup[BuildingType.Gym])
            {
                if (student == null)
                    continue;

                student.TrainedPHYStat++;
            }
        }
    }

    public void SkillFubuki()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestManager.instance.StaminaComsumption -= 5;
    }

    public void SkillMarina()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        float multiplePHYstat = 0;
        for (int i = 0; i < 4; i++)
        {
            if (currentRequest.squad[i] == null)
                continue;

            if (currentRequest.squad[i].school == School.RedWinter)
                multiplePHYstat += 0.1f;
        }

        RequestManager.instance.AddAdditionalStatus(
            (int)(RequestManager.instance.TotalPHYStat * multiplePHYstat), 0, 0);
    }

    public void SkillNoa()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        currentRequest.CurrentCredit += (int)(currentRequest.credit * 0.3f);
        currentRequest.CurrentXP += (int)(currentRequest.credit * 0.3f);
    }

    public void SkillAsuna()
    {
        // Add your code here for SkillAsuna
    }

    public void SkillHibiki()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestManager.instance.AddAdditionalStatus(
                (int)(RequestManager.instance.TotalPHYStat * 0.1f),
                (int)(RequestManager.instance.TotalINTStat * 0.1f), 0);

        RequestManager.instance.TotalPHYStat -= (int)(currentStudent.CurrentPHYStat * 0.1f);
    }

    public void SkillNatsu()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestManager.instance.AddAdditionalStatus(
                0, (int)(currentStudent.CurrentINTStat * 0.3f), (int)(RequestManager.instance.TotalCOMStat * 0.1f));
    }

    public void SkillKoharu()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestManager.instance.AddAdditionalStatus(
                0, 0, (int)(RequestManager.instance.TotalCOMStat * 0.5f));
    }

    public void SkillMari()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;

        if (currentRequest.happiness > 0)
        {
            currentRequest.CurrentHappiness += 2;
        }
    }

    public void SkillAyane()
    {
        if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestManager.instance.AddAdditionalStatus(0, 0, (int)(RequestManager.instance.TotalCOMStat * 0.1f));
    }

    public void SkillNonomi()
    {
                if (RequestManager.instance.CurrentRequest == null)
            return;

        RequestSO currentRequest = RequestManager.instance.CurrentRequest;
        currentRequest.CurrentCredit += (int)(currentRequest.credit * 0.5f);

    }

    public void SkillSena()
    {
        // Add your code here for SkillSena
    }

    public void SkillHaruna()
    {
        // Add your code here for SkillHaruna
    }

    public void SkillMutsuki()
    {
        // Add your code here for SkillMutsuki
    }

    public void SkillIzuna()
    {
        // Add your code here for SkillIzuna
    }

    public void SkillCherino()
    {
        // Add your code here for SkillCherino
    }

    public void SkillRio()
    {
        // Add your code here for SkillRio
    }

    public void SkillHimari()
    {
        // Add your code here for SkillHimari
    }

    public void SkillNeru()
    {
        // Add your code here for SkillNeru
    }

    public void SkillToki()
    {
        // Add your code here for SkillToki
    }

    public void SkillNagisa()
    {
        // Add your code here for SkillNagisa
    }

    public void SkillMika()
    {
        // Add your code here for SkillMika
    }

    public void SkillTsurugi()
    {
        // Add your code here for SkillTsurugi
    }

    public void SkillAzusa()
    {
        // Add your code here for SkillAzusa
    }

    public void SkillHina()
    {
        // Add your code here for SkillHina
    }

    public void SkillAru()
    {
        // Add your code here for SkillAru
    }

    public void SkillIroha()
    {
        // Add your code here for SkillIroha
    }

    public void SkillKayoko()
    {
        // Add your code here for SkillKayoko
    }

    public void SkillHoshino()
    {
        // Add your code here for SkillHoshino
    }

    public void SkillKizaki()
    {
        // Add your code here for SkillKizaki
    }

    public void SkillArisu()
    {
        // Add your code here for SkillArisu
    }

    public void SkillSeia()
    {
        // Add your code here for SkillSeia
    }

    public void SkillAko()
    {
        // Add your code here for SkillAko
    }

    public void SkillMiyako()
    {
        // Add your code here for SkillMiyako
    }

    public void SkillSaori()
    {
        // Add your code here for SkillSaori
    }

}