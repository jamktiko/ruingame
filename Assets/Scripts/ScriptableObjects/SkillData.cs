
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/SkillData")]
public class SkillData : ScriptableObject
{
    public SkillExecute skillScript;
}
static class Extensions
{
    public static void Shuffle<T>(this Random rnd, List<T> list)
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

