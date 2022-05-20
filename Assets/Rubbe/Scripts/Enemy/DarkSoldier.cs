using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSoldier : MonoBehaviour
{
    public GameObject darkSoldier;
    public GameObject Player;
    Animator DarkSoldier_animator;
    public float HP = 50;
    public int DAMAGE = 10;
    public float Player_Skill_Tier;
    public int Player_Skill_Damage;
    public int Monster_index;
    
    Vector3 first_position;

    public GameObject HPImage;
    public float MaxHP = 0;

    private void Start()
    {
        DarkSoldier_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (HP <= 0)
        {
            MaxHP = 0;
            HPImage.SetActive(false);
        }
        if (MaxHP != 0)
        {
            HPImage.transform.localScale = new Vector3((HP / MaxHP) / 10f, 0.02f, 0.1f);
        }
    }

    void setMaxHP(int a)
    {
        HP = a;
        MaxHP = a;
        Debug.Log(HP);
    }


    void damaged_start(object[] skill_info)
    {
        Player_Skill_Tier = (float)skill_info[3];
        Player_Skill_Damage = (int)skill_info[(int)Player_Skill_Tier - 1];
        Debug.LogFormat("{0} Tier Damaged! ({1})", Player_Skill_Tier, Player_Skill_Damage);
        DarkSoldier_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        DarkSoldier_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("DarkSoldier_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        DarkSoldier_animator.SetBool("Move", true);
        StartCoroutine(DarkSoldier_Moving());
    }
    IEnumerator DarkSoldier_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = darkSoldier.transform.position.x - Player.transform.position.x;
        while (Distance > 0.8f)
        {
            transform.position = Vector3.MoveTowards(darkSoldier.transform.position, Player.transform.position + new Vector3(0.8f, 0.5f, -0.1f), 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = darkSoldier.transform.position.x - Player.transform.position.x;
        }
        DarkSoldier_animator.SetBool("Move", false);
        yield return new WaitForSecondsRealtime(0.1f);
        DarkSoldier_animator.SetBool("Attack", true);
        StartCoroutine(DarkSoldier_Attacking());
    }
    
    IEnumerator DarkSoldier_Attacking()
    {
        int DarkSoldier_attackCount = 1;
        while (DarkSoldier_attackCount <= 1)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            Attack_DarkSoldier();
            Debug.LogFormat("DarkSoldier_Attackcount = {0}", DarkSoldier_attackCount);
            DarkSoldier_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        DarkSoldier_animator.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(0.2f);
        DarkSoldier_animator.SetBool("Move", true);
        StartCoroutine(DarkSoldier_Moving_Back());
    }

    IEnumerator DarkSoldier_Moving_Back()
    {
        float Distance = Vector3.Distance(darkSoldier.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(darkSoldier.transform.position, first_position);
        }
        DarkSoldier_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_DarkSoldier()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void DarkSoldier_Die()
    {
        Debug.Log(darkSoldier.name + " Die");
        DarkSoldier_animator.SetBool("Die", true);
        Invoke("Destory_DarkSoldier", 1.0f);
    }
    
    void Destory_DarkSoldier()
    {
        //씬에서 삭제하는것 대신에 비활성화(index문제)
        //새로운 맵으로 넘어갈때는 전부 삭제하고 다시 몬스터 채우기
        darkSoldier.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = darkSoldier.transform.position + Vector3.left*x;
        DarkSoldier_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(darkSoldier.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(darkSoldier.transform.position, first_position);
        }
        DarkSoldier_animator.SetBool("Move", false);
    }
}
