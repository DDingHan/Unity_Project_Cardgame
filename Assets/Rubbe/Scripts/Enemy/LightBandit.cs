using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBandit : MonoBehaviour
{
    public GameObject lightBandit;
    public GameObject Player;
    Animator LightBandit_animator;
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
        LightBandit_animator = GetComponent<Animator>();
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
        LightBandit_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        LightBandit_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("LightBandit_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        LightBandit_animator.SetBool("Move", true);
        StartCoroutine(LightBandit_Moving());
    }
    IEnumerator LightBandit_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = lightBandit.transform.position.x - Player.transform.position.x;
        while (Distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(lightBandit.transform.position, Player.transform.position + new Vector3(0.5f, 0.3f, -0.1f), 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = lightBandit.transform.position.x - Player.transform.position.x;
        }
        LightBandit_animator.SetBool("Move", false);
        yield return new WaitForSecondsRealtime(0.1f);
        LightBandit_animator.SetBool("Attack", true);
        StartCoroutine(LightBandit_Attacking());
    }
    
    IEnumerator LightBandit_Attacking()
    {
        int LightBandit_attackCount = 1;
        while (LightBandit_attackCount <= 1)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            Attack_LightBandit();
            Debug.LogFormat("LightBandit_Attackcount = {0}", LightBandit_attackCount);
            LightBandit_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        LightBandit_animator.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(0.2f);
        LightBandit_animator.SetBool("Move", true);
        StartCoroutine(LightBandit_Moving_Back());
    }

    IEnumerator LightBandit_Moving_Back()
    {
        float Distance = Vector3.Distance(lightBandit.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(lightBandit.transform.position, first_position);
        }
        LightBandit_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_LightBandit()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void LightBandit_Die()
    {
        Debug.Log(lightBandit.name + " Die");
        LightBandit_animator.SetBool("Die", true);
        Invoke("Destory_LightBandit", 1.0f);
    }
    
    void Destory_LightBandit()
    {
        //씬에서 삭제하는것 대신에 비활성화(index문제)
        //새로운 맵으로 넘어갈때는 전부 삭제하고 다시 몬스터 채우기
        lightBandit.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = lightBandit.transform.position + Vector3.left*x;
        LightBandit_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(lightBandit.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(lightBandit.transform.position, first_position);
        }
        LightBandit_animator.SetBool("Move", false);
    }
}
