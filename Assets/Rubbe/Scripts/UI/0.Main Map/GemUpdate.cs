using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GemUpdate : MonoBehaviour
{
    public Text[] GemTexts;
    public int[,] nowGems;


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                GemTexts[i*3+j].text = nowGems[i,j].ToString();
            }            
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Ŭ���� ���� ȭ�鿡�� ���� ������ �Ѿ�� �� Ŭ���� �������� �������� ���ϱ�
        if (GameObject.Find("RewardData") != null)
        {
            nowGems = GameObject.Find("GameData").GetComponent<Data>().nowGems;
            int[,] clearGems = GameObject.Find("RewardData").GetComponent<RewardData>().clearGem;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (clearGems[i, j] != 0)
                    {
                        nowGems[i,j] += clearGems[i,j];
                    }
                }
            }
        }
        //���� �� ó�� ȣ�� �� ��
        else
        {
            nowGems = new int[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
        }
        GameObject.Find("GameData").GetComponent<Data>().nowGems = nowGems;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
