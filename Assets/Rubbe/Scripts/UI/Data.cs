using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
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

    public float Gold = 0;

    void HPUpgrade()
    {
        if (HP_Level <= 5)
        {
            if (Gold >= 1000)
            {
                Gold = Gold - 1000;
                HP_Level++;
            }
        }
        else if (HP_Level <= 10)
        {
            if (Gold >= 1500)
            {
                Gold = Gold - 1500;
                HP_Level++;
            }
        }
        else if (HP_Level <= 15)
        {
            if (Gold >= 2000)
            {
                Gold = Gold - 2000;
                HP_Level++;
            }
        }
    }
}
