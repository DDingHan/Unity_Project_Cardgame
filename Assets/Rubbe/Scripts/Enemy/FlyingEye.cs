using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public GameObject flyingEye;
    public GameObject Player;
    Animator FlyingEye_animator;
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
        FlyingEye_animator = GetComponent<Animator>();
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
        FlyingEye_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        FlyingEye_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("FlyingEye_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        FlyingEye_animator.SetBool("Move", true);
        StartCoroutine(FlyingEye_Moving());
    }
    IEnumerator FlyingEye_Moving()
    {
        //살아있는 플레이어 중에 랜덤으로 선택
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        //움직이기 전의 좌표 저장
        float Distance = flyingEye.transform.position.x - Player.transform.position.x;
        while (Distance > 0.5f)
        {            
            transform.position = Vector3.MoveTowards(flyingEye.transform.position, Player.transform.position + new Vector3(0f, 0.5f, -0.1f), 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = flyingEye.transform.position.x - Player.transform.position.x;
            Debug.Log(Distance);
        }
        FlyingEye_animator.SetBool("Move", false);
        yield return new WaitForSecondsRealtime(0.1f);
        FlyingEye_animator.SetBool("Attack", true);
        StartCoroutine(FlyingEye_Attacking());
    }
    
    IEnumerator FlyingEye_Attacking()
    {
        int FlyingEye_attackCount = 1;
        while (FlyingEye_attackCount <= 1)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            Attack_FlyingEye();
            Debug.LogFormat("FlyingEye_Attackcount = {0}", FlyingEye_attackCount);
            FlyingEye_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        FlyingEye_animator.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(0.2f);
        FlyingEye_animator.SetBool("Move", true);
        StartCoroutine(FlyingEye_Moving_Back());
    }

    IEnumerator FlyingEye_Moving_Back()
    {
        float Distance = Vector3.Distance(flyingEye.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(flyingEye.transform.position, first_position);
        }
        FlyingEye_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_FlyingEye()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void FlyingEye_Die()
    {
        Debug.Log(flyingEye.name + " Die");
        FlyingEye_animator.SetBool("Die", true);
        Invoke("Destory_FlyingEye", 1.0f);
    }
    
    void Destory_FlyingEye()
    {
        //씬에서 삭제하는것 대신에 비활성화(index문제)
        //새로운 맵으로 넘어갈때는 전부 삭제하고 다시 몬스터 채우기
        flyingEye.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = flyingEye.transform.position + Vector3.left*x;
        FlyingEye_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(flyingEye.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(flyingEye.transform.position, first_position);
        }
        FlyingEye_animator.SetBool("Move", false);
    }
}
