using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoldUpdate : MonoBehaviour
{
    // Start is called before the first frame update

    public Text GoldText;
    public Text LevelText;
    public int nowGold;


    // Update is called once per frame
    void Update()
    {
        GoldText.text = nowGold.ToString();
        LevelText.text = GameObject.Find("GameData").GetComponent<Data>().HP_Level.ToString();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //클리어 보상 화면에서 메인 씬으로 넘어올 때 클리어 골드를 원래골드에 더하기
        if (GameObject.Find("RewardData") != null)
        {
            nowGold = GameObject.Find("GameData").GetComponent<Data>().nowGold;
            int clearGold = GameObject.Find("RewardData").GetComponent<RewardData>().clearGold;
            nowGold += clearGold;
        }
        //씬이 맨 처음 호출 될 때
        else
        {
            nowGold = 0;
        }
        GameObject.Find("GameData").GetComponent<Data>().nowGold = nowGold;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
