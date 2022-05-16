using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject messageUI;
    public GameObject settingUI;
    public GameObject menuUI;
    bool menuUIActive = false;

    void Start()
    {
        if (GameObject.Find("GameData").GetComponent<Data>().mapName != "" && GameObject.Find("GameData").GetComponent<Data>().stageNum != "")
        {
            GameObject[] stages = GameObject.FindGameObjectsWithTag("Stage");

            foreach(GameObject a in stages)
            {
                if(a.name == GameObject.Find("GameData").GetComponent<Data>().stageNum)
                {
                    a.GetComponent<SpriteRenderer>().color = Color.black;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMessageUI()
    {
        messageUI.SetActive(true);
    }

    public void hiddenMessageUI()
    {
        messageUI.SetActive(false);
    }

    public void showsettingUI()
    {
        settingUI.SetActive(true);
    }

    public void hiddensettingUI()
    {
        settingUI.SetActive(false);
    }

    public void clickMenuUI()
    {
        if (!menuUIActive)
        {
            menuUI.SetActive(true);
        }
        else
        {
            menuUI.SetActive(false);
        }
        menuUIActive = !menuUIActive;
    }
}
