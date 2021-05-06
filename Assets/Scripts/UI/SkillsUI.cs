using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.UI;
public class SkillsUI : MonoBehaviour
{
    private SkillUser skillUser;
    public GameObject[] skillIconPrefabs;
    public Image[] skillImages;
    public GameObject[] skillIconPrefab;
    public float UIUpdateRate = 0.05f;
    void Start()
    {
        skillUser = PlayerManager.Instance._playerSkills;
        skillUser.SkillActivated += OnSkillUse;
        skillUser.skillUI = this;
        skillIconPrefabs = new GameObject[skillUser.skillList.Length];
        skillImages = new Image[skillUser.skillList.Length];
        for (int i = 0; i < skillUser.skillList.Length; i++)
        {
            skillIconPrefabs[i] = Instantiate(skillIconPrefab[i]);
            skillIconPrefabs[i].transform.SetParent(gameObject.transform);
            var skI = skillIconPrefabs[i].GetComponent<SkillImage>();
            //skI.skillImage = skillUser.skillList[i]
            //skI.skillImageGrayScale = skillUser.skillList[i]
            skillImages[i] = skI.skillImageGrayScale;
        }

        foreach (Image skillImage in skillImages)
        {
            skillImage.fillAmount = 0;
        }
    }

    public IEnumerator StartCooldown(int index)
    {
        var sk = skillUser.skillList[index];
        var cd = sk.skillCooldown;
        var ui = UIUpdateRate / sk.skillCooldown;
        while (cd > 0)
        {
            skillImages[index].fillAmount -= ui;
            cd -= UIUpdateRate;
            yield return new WaitForSeconds(UIUpdateRate);
        }
        if (skillImages[index].fillAmount <= 0)
        {
            skillImages[index].fillAmount = 0;
        }
    }
    public void OnSkillUse(SkillActivatedEventArgs e)
    {
        skillImages[e.SkillIndex].fillAmount = 1;
        StartCoroutine(StartCooldown(e.SkillIndex));
    }
    
}
