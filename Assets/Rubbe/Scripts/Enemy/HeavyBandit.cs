using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBandit : MonoBehaviour
{
    public GameObject heavyBandit;
    public GameObject Player;
    Animator HeavyBandit_animator;
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
        HeavyBandit_animator = GetComponent<Animator>();
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
        HeavyBandit_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        HeavyBandit_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("HeavyBandit_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        HeavyBandit_animator.SetBool("Move", true);
        StartCoroutine(HeavyBandit_Moving());
    }
    IEnumerator HeavyBandit_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = heavyBandit.transform.position.x - Player.transform.position.x;
        while (Distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(heavyBandit.transform.position, Player.transform.position + new Vector3(0.5f, 0.3f, -0.1f), 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = heavyBandit.transform.position.x - Player.transform.position.x;
        }
        HeavyBandit_animator.SetBool("Move", false);
        yield return new WaitForSecondsRealtime(0.1f);
        HeavyBandit_animator.SetBool("Attack", true);
        StartCoroutine(HeavyBandit_Attacking());
    }
    
    IEnumerator HeavyBandit_Attacking()
    {
        int HeavyBandit_attackCount = 1;
        while (HeavyBandit_attackCount <= 1)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            Attack_HeavyBandit();
            Debug.LogFormat("HeavyBandit_Attackcount = {0}", HeavyBandit_attackCount);
            HeavyBandit_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        HeavyBandit_animator.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(0.2f);
        HeavyBandit_animator.SetBool("Move", true);
        StartCoroutine(HeavyBandit_Moving_Back());
    }

    IEnumerator HeavyBandit_Moving_Back()
    {
        float Distance = Vector3.Distance(heavyBandit.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(heavyBandit.transform.position, first_position);
        }
        HeavyBandit_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_HeavyBandit()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void HeavyBandit_Die()
    {
        Debug.Log(heavyBandit.name + " Die");
        HeavyBandit_animator.SetBool("Die", true);
        Invoke("Destory_HeavyBandit", 1.0f);
    }
    
    void Destory_HeavyBandit()
    {
        //������ �����ϴ°� ��ſ� ��Ȱ��ȭ(index����)
        //���ο� ������ �Ѿ���� ���� �����ϰ� �ٽ� ���� ä���
        heavyBandit.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = heavyBandit.transform.position + Vector3.left*x;
        HeavyBandit_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(heavyBandit.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(heavyBandit.transform.position, first_position);
        }
        HeavyBandit_animator.SetBool("Move", false);
    }
}
