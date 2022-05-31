using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardData : MonoBehaviour
{
    public int clearGold;
    public int[,] clearGem;

    private void Awake()
    {
        var obj = FindObjectsOfType<RewardData>();

        //stage_num = GameObject.Find("Monster").GetComponent<Monster>().stage_num;
        //map_num = GameObject.Find("Monster").GetComponent<Monster>().map_num;


        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
