using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GemUpdate : MonoBehaviour
{
    public Text[] GemTexts;
    //public int[] nowGems;


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 6; i++)
        {
            GemTexts[i].text = GameObject.Find("GameData").GetComponent<Data>().nowGems[i].ToString();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //클리어 보상 화면에서 메인 씬으로 넘어올 때 클리어 보상젬을 원래젬에 더하기
        if (GameObject.Find("RewardData") != null)
        {
            int[] clearGems = GameObject.Find("RewardData").GetComponent<RewardData>().clearGem;
            for (int i = 0; i < 6; i++)
            {
                if (clearGems[i] != 0)
                {
                    GameObject.Find("GameData").GetComponent<Data>().nowGems[i] += clearGems[i];
                }
            }
        }
        //씬이 맨 처음 호출 될 때
        else
        {
            GameObject.Find("GameData").GetComponent<Data>().nowGems = new int[] { 0, 0, 0, 0, 0, 0 };
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
