using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject messageUI;
    public GameObject settingUI;
    public GameObject menuUI;
    bool menuUIActive = false;

    //public GameObject[] stages;

    public GameObject Map1;
    public GameObject Stages_1;

    void Start()
    {
        if (GameObject.Find("GameData").GetComponent<Data>().mapName == "1")
        {
            Map1.SetActive(true);
            Stages_1.SetActive(true);
        }
        if (GameObject.Find("GameData").GetComponent<Data>().mapName != "" && GameObject.Find("GameData").GetComponent<Data>().stageNum != "")
        {
            /*GameObject[] stages = GameObject.FindGameObjectsWithTag("Stage");

            foreach(GameObject a in stages)
            {
                if(a.name == GameObject.Find("GameData").GetComponent<Data>().stageNum)
                {
                    a.GetComponent<SpriteRenderer>().color = Color.black;
                }
            }*/
            GameObject.Find("GameData").GetComponent<Data>().stageClearCheck[int.Parse(GameObject.Find("GameData").GetComponent<Data>().stageNum) - 1] = true;
            GameObject[] stages = GameObject.FindGameObjectsWithTag("Stage");

            for(int i=0; i < stages.Length; i++)
            {
                if (GameObject.Find("GameData").GetComponent<Data>().stageClearCheck[i] == true)
                {
                    stages[i].GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] stages = GameObject.FindGameObjectsWithTag("Stage");

        for (int i = 0; i < stages.Length; i++)
        {
            if (GameObject.Find("GameData").GetComponent<Data>().stageClearCheck[i] == true)
            {
                stages[i].GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

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
