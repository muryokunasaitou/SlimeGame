using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyAssetMenu/SkillTable",fileName ="Skill_Table")]
public class Skill_Table : ScriptableObject
{
    public List<Skill> skill=new List<Skill>();
}

[System.Serializable]
public class Skill 
{
    public enum Skills 
    {
        Bite,
        UltraSounds,
        JumpEnhance,
        SmallSlime,
        Fire,
        Impact,
    }
    public Skills skill_type;
    public GameObject skill_effect;
}