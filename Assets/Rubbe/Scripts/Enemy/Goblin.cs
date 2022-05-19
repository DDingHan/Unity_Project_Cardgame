using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public GameObject goblin;
    public GameObject Player;
    Animator Goblin_animator;
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
        Goblin_animator = GetComponent<Animator>();
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
        Goblin_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        Goblin_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("Goblin_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        Goblin_animator.SetBool("Move", true);
        StartCoroutine(Goblin_Moving());
    }
    IEnumerator Goblin_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = goblin.transform.position.x - Player.transform.position.x;
        while (Distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(goblin.transform.position, Player.transform.position + Vector3.up * 0.2f + Vector3.right * 0.5f, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = goblin.transform.position.x - Player.transform.position.x;
        }
        Goblin_animator.SetBool("Move", false);
        Goblin_animator.SetBool("Attack", true);
        StartCoroutine(Goblin_Attacking());
    }
    
    IEnumerator Goblin_Attacking()
    {
        int Goblin_attackCount = 1;
        while (Goblin_attackCount <= 1)
        {
            Attack_Goblin();
            Debug.LogFormat("Goblin_Attackcount = {0}", Goblin_attackCount);
            Goblin_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.6f);
        }
        Goblin_animator.SetBool("Attack", false);
        Goblin_animator.SetBool("Move", true);
        StartCoroutine(Goblin_Moving_Back());
    }

    IEnumerator Goblin_Moving_Back()
    {
        float Distance = Vector3.Distance(goblin.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(goblin.transform.position, first_position);
        }
        Goblin_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Turn").GetComponent<Turn>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_Goblin()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void Goblin_Die()
    {
        Debug.Log(goblin.name + " Die");
        Goblin_animator.SetBool("Die", true);
        Invoke("Destory_Goblin", 1.0f);
    }
    
    void Destory_Goblin()
    {
        //������ �����ϴ°� ��ſ� ��Ȱ��ȭ(index����)
        //���ο� ������ �Ѿ���� ���� �����ϰ� �ٽ� ���� ä���
        goblin.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = goblin.transform.position + Vector3.left*x;
        Goblin_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(goblin.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(goblin.transform.position, first_position);
        }
        Goblin_animator.SetBool("Move", false);
    }
}
