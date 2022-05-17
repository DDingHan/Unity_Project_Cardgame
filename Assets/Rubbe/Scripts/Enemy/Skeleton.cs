using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject skeleton;
    public GameObject Player;
    Animator Skeleton_animator;
    public int HP = 50;
    public int DAMAGE = 10;
    public float Player_Skill_Tier;
    public int Player_Skill_Damage;
    public int Monster_index;
    
    Vector3 first_position;

    private void Start()
    {
        Skeleton_animator = GetComponent<Animator>();
    }


    void damaged_start(object[] skill_info)
    {
        Player_Skill_Tier = (float)skill_info[3];
        Player_Skill_Damage = (int)skill_info[(int)Player_Skill_Tier - 1];
        Debug.LogFormat("{0} Tier Damaged! ({1})", Player_Skill_Tier, Player_Skill_Damage);
        Skeleton_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        Skeleton_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("Skeleton_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        Skeleton_animator.SetBool("Move", true);
        StartCoroutine(Skeleton_Moving());
    }
    IEnumerator Skeleton_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = skeleton.transform.position.x - Player.transform.position.x;
        while (Distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(skeleton.transform.position, Player.transform.position + Vector3.up * 0.2f + Vector3.right * 0.5f, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = skeleton.transform.position.x - Player.transform.position.x;
        }
        Skeleton_animator.SetBool("Move", false);
        Skeleton_animator.SetBool("Attack", true);
        StartCoroutine(Skeleton_Attacking());
    }
    
    IEnumerator Skeleton_Attacking()
    {
        int Skeleton_attackCount = 1;
        while (Skeleton_attackCount <= 1)
        {
            Attack_Skeleton();
            Debug.LogFormat("Skeleton_Attackcount = {0}", Skeleton_attackCount);
            Skeleton_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.6f);
        }
        Skeleton_animator.SetBool("Attack", false);
        Skeleton_animator.SetBool("Move", true);
        StartCoroutine(Skeleton_Moving_Back());
    }

    IEnumerator Skeleton_Moving_Back()
    {
        float Distance = Vector3.Distance(skeleton.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(skeleton.transform.position, first_position);
        }
        Skeleton_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Turn").GetComponent<Turn>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_Skeleton()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void Skeleton_Die()
    {
        Debug.Log(skeleton.name + " Die");
        Skeleton_animator.SetBool("Die", true);
        Invoke("Destory_Skeleton", 1.0f);
    }
    
    void Destory_Skeleton()
    {
        //������ �����ϴ°� ��ſ� ��Ȱ��ȭ(index����)
        //���ο� ������ �Ѿ���� ���� �����ϰ� �ٽ� ���� ä���
        skeleton.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = skeleton.transform.position + Vector3.left*x;
        Skeleton_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(skeleton.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(skeleton.transform.position, first_position);
        }
        Skeleton_animator.SetBool("Move", false);
    }
}