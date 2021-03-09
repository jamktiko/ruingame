using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillsUI : MonoBehaviour
{
    private SkillUser skillUser;
    public GameObject[] skillIconPrefabs;
    public Image[] skillImages;
    public GameObject skillIconPrefab;
    void Start()
    {
        skillUser = PlayerManager.Instance._playerSkills;
        skillUser.skillUI = this;
        skillIconPrefabs = new GameObject[skillUser.skillList.Length];
        skillImages = new Image[skillUser.skillList.Length];
        for (int i = 0; i < skillUser.skillList.Length; i++)
        {
            skillIconPrefabs[i] = Instantiate(skillIconPrefab);
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

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < skillUser.skillList.Length; i++)
        {
            try
            {
                SkillUpdate(skillUser.skillList[i], i);
            }
            catch
            {
            }
        }
    }
    

    void SkillUpdate(SkillExecute sk, int index)
    {
        if (sk != null)
        {
            if (sk.onCooldown)
            {
                try
                {
                    skillImages[index].fillAmount -= 1 / sk.skillCooldown * Time.deltaTime;
                    if (skillImages[index].fillAmount <= 0)
                    {
                        skillImages[index].fillAmount = 0;
                    }
                    
                }
                catch {Debug.Log("No UI element assigned!");}
            }
        }

    }
    public void OnSkillUse(int index)
    {
        skillImages[index].fillAmount = 1;
    }
    
}
