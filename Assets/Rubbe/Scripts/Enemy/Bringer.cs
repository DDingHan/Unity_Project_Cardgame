using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bringer : MonoBehaviour
{
    public GameObject bringer;
    public GameObject Player;
    Animator Bringer_animator;
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
        Bringer_animator = GetComponent<Animator>();
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
        Bringer_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        Bringer_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("Bringer_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        Bringer_animator.SetBool("Move", true);
        StartCoroutine(Bringer_Moving());
    }
    IEnumerator Bringer_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = bringer.transform.position.x - Player.transform.position.x;
        while (Distance > 0.8f)
        {
            transform.position = Vector3.MoveTowards(bringer.transform.position, Player.transform.position + new Vector3(0.8f, 0.5f, -0.1f), 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = bringer.transform.position.x - Player.transform.position.x;
        }
        Bringer_animator.SetBool("Move", false);
        yield return new WaitForSecondsRealtime(0.1f);
        Bringer_animator.SetBool("Attack", true);
        StartCoroutine(Bringer_Attacking());
    }
    
    IEnumerator Bringer_Attacking()
    {
        int Bringer_attackCount = 1;
        while (Bringer_attackCount <= 1)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            Attack_Bringer();
            Debug.LogFormat("Bringer_Attackcount = {0}", Bringer_attackCount);
            Bringer_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        Bringer_animator.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(0.2f);
        Bringer_animator.SetBool("Move", true);
        StartCoroutine(Bringer_Moving_Back());
    }

    IEnumerator Bringer_Moving_Back()
    {
        float Distance = Vector3.Distance(bringer.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(bringer.transform.position, first_position);
        }
        Bringer_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_Bringer()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void Bringer_Die()
    {
        Debug.Log(bringer.name + " Die");
        Bringer_animator.SetBool("Die", true);
        Invoke("Destory_Bringer", 1.0f);
    }
    
    void Destory_Bringer()
    {
        //씬에서 삭제하는것 대신에 비활성화(index문제)
        //새로운 맵으로 넘어갈때는 전부 삭제하고 다시 몬스터 채우기
        bringer.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = bringer.transform.position + Vector3.left*x;
        Bringer_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(bringer.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(bringer.transform.position, first_position);
        }
        Bringer_animator.SetBool("Move", false);
    }
}
