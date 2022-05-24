using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public GameObject mushroom;
    public GameObject Player;
    Animator Mushroom_animator;
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
        Mushroom_animator = GetComponent<Animator>();
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
        Mushroom_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        Mushroom_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("Mushroom_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        Mushroom_animator.SetBool("Move", true);
        StartCoroutine(Mushroom_Moving());
    }
    IEnumerator Mushroom_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = mushroom.transform.position.x - Player.transform.position.x;
        while (Distance > 0.8f)
        {
            transform.position = Vector3.MoveTowards(mushroom.transform.position, Player.transform.position + new Vector3(0.8f, 0.5f, -0.1f), 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = mushroom.transform.position.x - Player.transform.position.x;
        }
        Mushroom_animator.SetBool("Move", false);
        yield return new WaitForSecondsRealtime(0.1f);
        Mushroom_animator.SetBool("Attack", true);
        StartCoroutine(Mushroom_Attacking());
    }
    
    IEnumerator Mushroom_Attacking()
    {
        int Mushroom_attackCount = 1;
        while (Mushroom_attackCount <= 1)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            Attack_Mushroom();
            Debug.LogFormat("Mushroom_Attackcount = {0}", Mushroom_attackCount);
            Mushroom_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.4f);
        }
        Mushroom_animator.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(0.2f);
        Mushroom_animator.SetBool("Move", true);
        StartCoroutine(Mushroom_Moving_Back());
    }

    IEnumerator Mushroom_Moving_Back()
    {
        float Distance = Vector3.Distance(mushroom.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(mushroom.transform.position, first_position);
        }
        Mushroom_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_Mushroom()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void Mushroom_Die()
    {
        Debug.Log(mushroom.name + " Die");
        Mushroom_animator.SetBool("Die", true);
        Invoke("Destory_Mushroom", 1.0f);
    }
    
    void Destory_Mushroom()
    {
        //씬에서 삭제하는것 대신에 비활성화(index문제)
        //새로운 맵으로 넘어갈때는 전부 삭제하고 다시 몬스터 채우기
        mushroom.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = mushroom.transform.position + Vector3.left*x;
        Mushroom_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(mushroom.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(mushroom.transform.position, first_position);
        }
        Mushroom_animator.SetBool("Move", false);
    }
}
