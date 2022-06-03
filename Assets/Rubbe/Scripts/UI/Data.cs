using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public GameObject datacontroller;
    public int nowGold;
    public int[] nowGems;
    private void Awake()
    {
        var obj = FindObjectsOfType<Data>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string mapName = "";
    void setMapName(string a)
    {
        mapName = a;
    }

    public string stageNum = "";
    void setStageNum(string a)
    {
        stageNum = a;
    }

    public bool[] stageClearCheck = { false, false, false, false, false, false, false, false };

    public float HP_Level=1f;

    //public float Gold = 0;

    void HPUpgrade()
    {
        if (HP_Level <= 5)
        {
            if (nowGold >= 1000)
            {
                nowGold = nowGold - 1000;
                HP_Level++;
            }
        }
        else if (HP_Level <= 10)
        {
            if (nowGold >= 1500)
            {
                nowGold = nowGold - 1500;
                HP_Level++;
            }
        }
        else if (HP_Level <= 15)
        {
            if (nowGold >= 2000)
            {
                nowGold = nowGold - 2000;
                HP_Level++;
            }
        }
    }
}
