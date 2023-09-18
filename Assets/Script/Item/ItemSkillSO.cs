using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSkillSO : ScriptableObject
{
   public abstract void PerformSkill(ItemSO itemSO);
}
