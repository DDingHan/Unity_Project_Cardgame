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

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Ŭ���� ���� ȭ�鿡�� ���� ������ �Ѿ�� �� Ŭ���� �������� �������� ���ϱ�
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
        //���� �� ó�� ȣ�� �� ��
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
