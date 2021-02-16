using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillsUI : MonoBehaviour
{
    public Image abilityImage1;
    public SkillUser skillUser;
    public Image[] skillImages = new Image[10];
    
    /*
    void Start()
    {
        
        abilityImage1.fillAmount = 0;
       Debug.Log(skillUser.skillList.Length);
        for (int i = 0; i < skillUser.skillList.Length; i++)
        {
            skillImages[i] = abilityImage1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < skillUser.skillList.Length; i++)
        {
            SkillUpdate(skillUser.skillList[i], i);
        }
    }
    */

    void SkillUpdate(SkillExecute sk, int index)
    {
        /*Debug.Log(sk);
        if (sk.onCooldown)
        {
            try
            {
                skillImages[index].fillAmount -= 1 / sk.skillCooldown * Time.deltaTime;
                if (abilityImage1.fillAmount <= 0)
                {
                    abilityImage1.fillAmount = 0;
                }
            }
            catch {Debug.Log("No UI element assigned!");}
        }
        */
    }
    public void OnSkillUse1()
    {
        abilityImage1.fillAmount = 1;
    }
    
}
