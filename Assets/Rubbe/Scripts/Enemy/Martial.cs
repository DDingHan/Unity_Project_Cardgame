using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martial : MonoBehaviour
{
    public GameObject martial;
    public GameObject Player;
    Animator Martial_animator;
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
        Martial_animator = GetComponent<Animator>();
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
        Martial_animator.SetBool("Damaged", true);
        HP -= Player_Skill_Damage;
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        Martial_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Invoke("Martial_Die", 1.0f);
        }
    }

    void Monster_Start(int index)
    {
        Monster_index = index;
        Martial_animator.SetBool("Move", true);
        StartCoroutine(Martial_Moving());
    }
    IEnumerator Martial_Moving()
    {
        do
        {
            int Player_index = Random.Range(0, 3);
            Player = GameObject.Find("Player").transform.GetChild(Player_index).gameObject;
        } while (Player.GetComponent<Character>().chr_Died);
        
        Debug.Log(Player.name);
        float Distance = martial.transform.position.x - Player.transform.position.x;
        while (Distance > 0.8f)
        {
            transform.position = Vector3.MoveTowards(martial.transform.position, Player.transform.position + new Vector3(0.8f, 0.5f, -0.1f), 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = martial.transform.position.x - Player.transform.position.x;
        }
        Martial_animator.SetBool("Move", false);
        yield return new WaitForSecondsRealtime(0.1f);
        Martial_animator.SetBool("Attack", true);
        StartCoroutine(Martial_Attacking());
    }
    
    IEnumerator Martial_Attacking()
    {
        int Martial_attackCount = 1;
        while (Martial_attackCount <= 1)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            Attack_Martial();
            Debug.LogFormat("Martial_Attackcount = {0}", Martial_attackCount);
            Martial_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        Martial_animator.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(0.2f);
        Martial_animator.SetBool("Move", true);
        StartCoroutine(Martial_Moving_Back());
    }

    IEnumerator Martial_Moving_Back()
    {
        float Distance = Vector3.Distance(martial.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(martial.transform.position, first_position);
        }
        Martial_animator.SetBool("Move", false);
        Invoke("SendMessage_MonsterIndex", 1.0f);
    }

    void SendMessage_MonsterIndex()
    {
        GameObject.Find("Monster").GetComponent<Monster>().SendMessage("Monster_Attack", Monster_index + 1);
    }

    void Attack_Martial()
    {
        Player.GetComponent<Character>().SendMessage("damaged_start",DAMAGE);
    }

    void Martial_Die()
    {
        Debug.Log(martial.name + " Die");
        Martial_animator.SetBool("Die", true);
        Invoke("Destory_Martial", 1.0f);
    }
    
    void Destory_Martial()
    {
        //?????? ?????????? ?????? ????????(index????)
        //?????? ?????? ?????????? ???? ???????? ???? ?????? ??????
        martial.SetActive(false);
    }

    void FirstMove(float x)
    {
        first_position = martial.transform.position + Vector3.left*x;
        Martial_animator.SetBool("Move", true);
        StartCoroutine(Monster_FirstMoving());
    }

    IEnumerator Monster_FirstMoving()
    {
        float Distance = Vector3.Distance(martial.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(martial.transform.position, first_position);
        }
        Martial_animator.SetBool("Move", false);
    }
}
