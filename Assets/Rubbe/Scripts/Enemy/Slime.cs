using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject slime;
    public GameObject Player;
    Animator slime_animator;
    public float HP;
    public int DAMAGE;
    public float Player_Skill_Tier;
    public int Player_Skill_Damage;
    public int Monster_index;
    public float x_scale;
    
    Vector3 first_position;

    public GameObject HPImage;
    public float MaxHP = 0;

    private void Start()
    {
        slime_animator = GetComponent<Animator>();
        x_scale = HPImage.transform.localScale.x;
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
            HPImage.transform.localScale = new Vector3((HP / MaxHP) * x_scale, HPImage.transform.localScale.y, HPImage.transform.localScale.z);
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
        slime_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        //HP = HP - 10; //1티어 스킬 쓰는데 50뎀 달아서 일단 10으로 바꿔놈음 테스트용으로
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

    void Monster_Start(int index)
    {
        Monster_index = index;
        StartCoroutine(Slime_Moving());
    }
    IEnumerator Slime_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = slime.transform.position.x - Player.transform.position.x; 
        while (Distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(slime.transform.position, Player.transform.position + new Vector3(0.5f, 0.2f, -0.1f), 3.0f * Time.deltaTime);
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
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Monster_Attack", Monster_index + 1);
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
    }

    void FirstMove(float x)
    {
        first_position = slime.transform.position + Vector3.left*x;
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(slime.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(slime.transform.position, first_position);
        }
    }
}
