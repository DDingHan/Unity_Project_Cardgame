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
}
