using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public Text turnTxt;
    public bool myTurn;
    public GameObject Monsters;
    public int Monster_count;
    void Start()
    {
        myTurn = true;
        Monsters = GameObject.Find("Monster");
        Monster_count = Monsters.transform.childCount;
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

    public GameObject Monster;
    public int Monster_index;
    void Start_Monster_Attack()
    {
        Monster_index = -1;
        do //��Ȱ��ȭ ���ʹ� ����x �ϱ� ���� ���� Ȱ��ȭ �� ���� ã��
        {
            Monster_index++;
            Monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;
        } while (Monster.activeSelf==false && Monster_index < Monster_count);
        Monster_Attack(Monster_index); 
    }
    void Monster_Attack(int index)
    {
        Monster_index = index;
        //������ ���� ������ ����
        if (Monster_index < Monster_count)
        {
            Monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;

            while (Monster.activeSelf == false)
            {
                Monster_index++;
                if (Monster_index >= Monster_count) break;
                Monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;
            }
            if (Monster_index < Monster_count)
            {
                Debug.LogFormat("{0} ����", Monster.name);
                Monster.GetComponent<Slime>().SendMessage("Monster_Start", Monster_index);
            }                
        }
        //������ ���� ������ ������ �÷��̾� ������ ��ȯ
        if (Monster_index >= Monster_count)
        {
            Invoke("ChangeTurn", 1.0f);
        }
    }
}
