using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSkill : MonoBehaviour
{
    public Image Icon;

    public Image Now_Tier1;
    public Image Now_Tier2;
    public Image Now_Tier3;

    // Start is called before the first frame update
    void Start()
    {
        Icon = this.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_Skill()
    {
        string temp = GameObject.Find("Change").GetComponent<ChangeMain>().Now_Tier;

        if (temp == "1")
        {
            Now_Tier1.sprite = Icon.sprite;
            Click_Skill_Update_ChangeMain(temp);
        }
        else if (temp == "2")
        {
            Now_Tier2.sprite = Icon.sprite;
            Click_Skill_Update_ChangeMain(temp);
        }
        else if (temp == "3")
        {
            Now_Tier3.sprite = Icon.sprite;
            Click_Skill_Update_ChangeMain(temp);
        }
    }

    void Click_Skill_Update_ChangeMain(string temp)
    {
        if(GameObject.Find("Change").GetComponent<ChangeMain>().Main.sprite== GameObject.Find("Change").GetComponent<ChangeMain>().warrior)
        {
            if (temp == "1")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Warrior_Main_1 = Icon.sprite;
            }
            else if (temp == "2")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Warrior_Main_2 = Icon.sprite;
            }
            else if (temp == "3")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Warrior_Main_3 = Icon.sprite;
            }
        }
        else if (GameObject.Find("Change").GetComponent<ChangeMain>().Main.sprite == GameObject.Find("Change").GetComponent<ChangeMain>().mage)
        {
            if (temp == "1")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Mage_Main_1 = Icon.sprite;
            }
            else if (temp == "2")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Mage_Main_2 = Icon.sprite;
            }
            else if (temp == "3")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Mage_Main_3 = Icon.sprite;
            }
        }
        else if (GameObject.Find("Change").GetComponent<ChangeMain>().Main.sprite == GameObject.Find("Change").GetComponent<ChangeMain>().archer)
        {
            if (temp == "1")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Archer_Main_1 = Icon.sprite;
            }
            else if (temp == "2")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Archer_Main_2 = Icon.sprite;
            }
            else if (temp == "3")
            {
                GameObject.Find("Change").GetComponent<ChangeMain>().Archer_Main_3 = Icon.sprite;
            }
        }
    }
}
