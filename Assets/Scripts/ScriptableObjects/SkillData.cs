
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillData : MonoBehaviour
{
    [System.Serializable]
    public class SkillListTemplate
    {
        public List<NonMonoSkill> skill;
        public SkillListTemplate()
        {
            skill = new List<NonMonoSkill>();
        }
    }

    [System.Serializable]
    public class SkillFields
    {
        [SerializeField] private SprintSkill _sprintSkill = new SprintSkill();
        
    }

    public SkillFields skillfields = new SkillFields();
    
    public SkillListTemplate skillList;
    
    public NonMonoSkill[] nonSkillList = new NonMonoSkill[5];
    
    
    public void AddSkillExecute(NonMonoSkill sk)
    {
        skillList.skill.Add(sk);
    }

    public NonMonoSkill GetRandomSkillExecute()
    {
        var rng = new Random();
        rng.Shuffle(skillList.skill);
        NonMonoSkill returnSkill = null;
        try
        {
            returnSkill = skillList.skill.First();
            skillList.skill.RemoveAt(0);
        }
        catch
        {
            Debug.Log("No skills remaining!");
        }
        return returnSkill;
    }
}

static class Extensions
{
    public static void Shuffle<T> (this Random rnd, List<T> list)
    {
        T[] array = list.ToArray();
        int n = array.Length;
        while (n > 1) 
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
        list = array.ToList();
    }
}

