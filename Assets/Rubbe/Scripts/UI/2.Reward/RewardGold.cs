using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardGold : MonoBehaviour
{
    public Text goldTxt;
    public int stageNum;
    public int mapNum;
    public int[,] clearGold = new int[4, 9]{
        {0,0,0,0,0,0,0,0,0},
        {0,100,130,180,250,300,400,450,600 },
        {0,800,900,1100,1400,1800,2300,2900,4000 },
        {0,4500,5000,5500,7000,7800,9000,10000,15000 }
    };
    // Start is called before the first frame update
    void Start()
    {
        stageNum = int.Parse(GameObject.Find("GameData").GetComponent<Data>().stageNum);
        mapNum = int.Parse(GameObject.Find("GameData").GetComponent<Data>().mapName);
        goldTxt.text = clearGold[mapNum, stageNum].ToString();
        GameObject.Find("RewardData").GetComponent<RewardData>().clearGold = clearGold[mapNum, stageNum];
    }

}
