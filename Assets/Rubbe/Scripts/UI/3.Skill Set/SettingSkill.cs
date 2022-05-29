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
        }
        else if (temp == "2")
        {
            Now_Tier2.sprite = Icon.sprite;
        }
        else if (temp == "3")
        {
            Now_Tier3.sprite = Icon.sprite;
        }
    }
}
