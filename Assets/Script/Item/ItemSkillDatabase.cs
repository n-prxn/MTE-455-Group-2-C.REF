using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Skill Database", menuName = "Item/Skill Database")]
public class ItemSkillDatabase : ItemSkillSO
{
    ItemSO currentItem;
    public override void PerformSkill(ItemSO item)
    {
        currentItem = item;
        switch (currentItem.id)
        {
            case 1:
                ItemSkill01();
                break;
            case 2:
                ItemSkill02();
                break;
            case 3:
                ItemSkill03();
                break;
            case 4:
                ItemSkill04();
                break;
            case 5:
                ItemSkill05();
                break;
            case 6:
                ItemSkill06();
                break;
            case 7:
                ItemSkill07();
                break;
            case 8:
                ItemSkill08();
                break;
            case 9:
                ItemSkill09();
                break;
            case 10:
                ItemSkill10();
                break;
            case 11:
                ItemSkill11();
                break;
            case 12:
                ItemSkill12();
                break;
            case 13:
                ItemSkill13();
                break;
            case 14:
                ItemSkill14();
                break;
            case 15:
                ItemSkill15();
                break;
            default:
                // Handle the case when the itemID is not in the list
                break;
        }
    }

    public void ItemSkill01()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.CurrentStamina += 100;
        }
    }

    public void ItemSkill02()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.CurrentStamina += 50;
        }
    }

    public void ItemSkill03()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.CurrentStamina += 20;
        }
    }

    public void ItemSkill04()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0.3f, 0f, 0f);
        }
    }

    public void ItemSkill05()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0.2f, 0f, 0f);
        }
    }

    public void ItemSkill06()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0.1f, 0f, 0f);
        }
    }

    public void ItemSkill07()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0f, 0.3f, 0f);
        }
    }

    public void ItemSkill08()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0f, 0.2f, 0f);
        }
    }

    public void ItemSkill09()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0f, 0.1f, 0f);
        }
    }

    public void ItemSkill10()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0f, 0f, 0.3f);
        }
    }

    public void ItemSkill11()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0f, 0f, 0.2f);
        }
    }

    public void ItemSkill12()
    {
        foreach (Student student in SquadController.instance.Students)
        {
            student.IsBuff = true;
            student.BuffDuration = 1;
            student.BuffStudentStats(0f, 0f, 0.1f);
        }
    }

    public void ItemSkill13()
    {
        
    }

    public void ItemSkill14()
    {
        // Add your code here for ItemSkill14
    }

    public void ItemSkill15()
    {
        // Add your code here for ItemSkill15
    }

}
