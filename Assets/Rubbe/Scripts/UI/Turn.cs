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
            GameObject.Find("Timer").GetComponent<Timer>().SendMessage("TimerStart"); //타이머 시작
            GameObject.Find("Count").GetComponent<Count>().now_count = 0;             //횟수 다시 0으로 설정
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
        do //비활성화 몬스터는 공격x 하기 위해 다음 활성화 된 몬스터 찾기
        {
            Monster_index++;
            Monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;
        } while (Monster.activeSelf==false && Monster_index < Monster_count);
        Monster_Attack(Monster_index); 
    }
    void Monster_Attack(int index)
    {
        Monster_index = index;
        //마지막 몬스터 까지만 공격
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
                Debug.LogFormat("{0} 공격", Monster.name);
                Monster.GetComponent<Slime>().SendMessage("Monster_Start", Monster_index);
            }                
        }
        //마지막 몬스터 공격이 끝나면 플레이어 턴으로 전환
        if (Monster_index >= Monster_count)
        {
            Invoke("ChangeTurn", 1.0f);
        }
    }
}
