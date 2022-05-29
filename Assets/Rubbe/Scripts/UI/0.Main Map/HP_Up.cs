using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Up : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ReadyUI;
    public GameObject ReadyObjects;
    public GameObject Map;

    public GameObject MapNum;
    public string stageNum;
    public GameObject GameStart;

    //UI button
    public GameObject Shop;
    public GameObject Worldmap;
    public GameObject Special;
    public GameObject Undo;
    
    public Text Explain;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Explain.IsActive())
        {
            if (GameObject.Find("GameData").GetComponent<Data>().HP_Level <= 5)
            {
                Explain.text = "1~5 1000";
            }
            else if (GameObject.Find("GameData").GetComponent<Data>().HP_Level <= 10)
            {
                Explain.text = "6~10 1500";
            }
            else if (GameObject.Find("GameData").GetComponent<Data>().HP_Level <= 15)
            {
                Explain.text = "11~15 1500";
            }
        }
    }

    public void ChangeToReady()
    {
        Map.SetActive(false);
        ReadyUI.SetActive(true);
        ReadyObjects.SetActive(true);
        //UI button on/off
        Shop.SetActive(false);
        Worldmap.SetActive(false);
        Special.SetActive(false);
        Undo.SetActive(true);

        GameObject.Find("Start").GetComponent<ChangeScene>().SendMessage("setMap", MapNum);
        GameObject.Find("Start").GetComponent<ChangeScene>().SendMessage("setStageNum", stageNum);
    }
}
