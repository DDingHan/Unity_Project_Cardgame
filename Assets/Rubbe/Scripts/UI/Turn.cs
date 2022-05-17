using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public Image Panel;
    public Text turnTxt;
    public bool myTurn;
    public bool clear;
    public GameObject Monsters;
    public int Monster_count;
    void Start()
    {
        turnTxt.gameObject.SetActive(false);
        Monster_count = Monsters.transform.childCount;
        myTurn = true;
        clear = false;
    }

    void Update()
    {
        if(myTurn == true)
        {
            turnTxt.text = "My Turn";
        }
        else
        {
            turnTxt.text = "Enemy Turn";
        }
        //��� ���Ͱ� �׾��� ��
        if (Count_Active_Monster() == 0 && clear == true)
        {
            Debug.Log("��� ���� �Ҹ� & Ÿ�̸� ����");
            GameObject.Find("Timer").GetComponent<Timer>().timer = false;
            turnTxt.gameObject.SetActive(false);
            GameObject.Find("ReadyStart").GetComponent<ReadyStart>().SendMessage("Victory");
            clear = false;
        }       

    }

    int Count_Active_Monster()
    {
        int count = 0;
        for (int i = 0; i < Monsters.transform.childCount; i++)
        {
            if (Monsters.transform.GetChild(i).gameObject.activeSelf == true) count++;
        }
        return count;
    }

    void ChangeTurn()
    {
        myTurn = !myTurn;
        if (myTurn)
        {
            GameObject.Find("Timer").GetComponent<Timer>().SendMessage("TimerStart"); //Ÿ�̸� ����
            GameObject.Find("Count").GetComponent<Count>().now_count = 0;             //Ƚ�� �ٽ� 0���� ����
        }
        else
        {
            Invoke("Start_Monster_Attack", 1.0f);
        }
    }
    void Start_Monster_Attack()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Start_Monster_Attack", Monster_count);
    }
}
