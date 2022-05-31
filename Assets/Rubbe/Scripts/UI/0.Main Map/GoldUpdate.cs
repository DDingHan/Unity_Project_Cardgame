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

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Ŭ���� ���� ȭ�鿡�� ���� ������ �Ѿ�� �� Ŭ���� ��带 ������忡 ���ϱ�
        if (GameObject.Find("RewardData") != null)
        {
            nowGold = GameObject.Find("GameData").GetComponent<Data>().nowGold;
            int clearGold = GameObject.Find("RewardData").GetComponent<RewardData>().clearGold;
            nowGold += clearGold;
        }
        //���� �� ó�� ȣ�� �� ��
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
