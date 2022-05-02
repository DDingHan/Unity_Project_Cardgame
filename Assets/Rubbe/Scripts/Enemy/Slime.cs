using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject slime;
    public GameObject Player;
    Vector3 first_position;
    Animator slime_animator;
    public int HP = 50;
    public int DAMAGE = 10;
    public float Player_Skill_Tier;
    public int Player_Skill_Damage;

    private void Start()
    {
        slime_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //예시용으로 A,S,D를 눌렀을때 슬라임 공격 발동
        if (Input.GetKeyDown(KeyCode.A) && slime.name == "Slime1")
        {
            StartCoroutine(Slime_Moving());
        }
        else if (Input.GetKeyDown(KeyCode.S) && slime.name == "Slime2")
        {
            StartCoroutine(Slime_Moving());
        }
        else if (Input.GetKeyDown(KeyCode.D) && slime.name == "Slime3")
        {
            StartCoroutine(Slime_Moving());
        }
    }

    void damaged_start(object[] skill_info)
    {
        Player_Skill_Tier = (float)skill_info[3];
        Player_Skill_Damage = (int)skill_info[(int)Player_Skill_Tier - 1];
        Debug.LogFormat("{0} Tier Damaged! ({1})", Player_Skill_Tier, Player_Skill_Damage);
        slime_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        slime_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("Slime_Die", 1.0f);
        }
    }

    IEnumerator Slime_Moving()
    {
        first_position = slime.transform.position;
        int Player_index = Random.Range(0,3);
        Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        Debug.Log(Player.name);
        float Distance = slime.transform.position.x - Player.transform.position.x;
        while (Distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(slime.transform.position, Player.transform.position + Vector3.up * 0.2f + Vector3.right * 0.5f, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = slime.transform.position.x - Player.transform.position.x;
        }

        StartCoroutine(Slime_Attacking());
    }
    
    IEnumerator Slime_Attacking()
    {
        int slime_attackCount = 1;
        while (slime_attackCount <= 1)
        {
            Attack_Slime();
            Debug.LogFormat("Slime_Attackcount = {0}", slime_attackCount);
            slime_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.6f);
        }
        StartCoroutine(Slime_Moving_Back());
    }

    IEnumerator Slime_Moving_Back()
    {
        float Distance = Vector3.Distance(slime.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(slime.transform.position, first_position);
        }
    }

    void Attack_Slime()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void Slime_Die()
    {
        Debug.Log(slime.name + " Die");
        slime_animator.SetBool("Die", true);
        Invoke("Destory_Slime", 1.0f);
    }
    
    void Destory_Slime()
    {
        //씬에서 삭제하는것 대신에 비활성화(index문제)
        //새로운 맵으로 넘어갈때는 전부 삭제하고 다시 몬스터 채우기
        slime.SetActive(false);
        GameObject.Find("Cursor").GetComponent<Cursor_Move>().Destroy_count++;
    }
}
