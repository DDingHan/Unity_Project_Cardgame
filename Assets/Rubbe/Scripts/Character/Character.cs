using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject character;
    public GameObject Monster;
    Animator chr_animator;
    object[] success_card_set = new object[2];
    public Vector3 first_position;
    bool isSolider = false;
    public bool chr_Died = false;

    public int HP = 100;
    public int DAMAGE_1_Tier = 10;
    public int DAMAGE_2_Tier = 20;
    public int DAMAGE_3_Tier = 30;
    public float skill_Tier;

    //int damaged_count = 0;

    private void Start()
    {
        chr_animator = GetComponent<Animator>();
    }
    //#4-1. solider일때만 움직이고 공격후 다시 원위치로 오기
    void Move(object[] index)
    {
        //first_position = character.transform.position;
        isSolider = true;
        success_card_set[0] = index[0];
        success_card_set[1] = index[1];
        skill_Tier = (float)index[2];
        StartCoroutine(Character_Moving());
    }

    //#4 0.6초에 한번씩 공격 반복 (설정한 공격 횟수만큼 반복)
    void Attack(object[] index)
    {
        success_card_set[0] = index[0];
        success_card_set[1] = index[1];
        skill_Tier = (float)index[2];
        int Monster_index = GameObject.Find("Cursor").GetComponent<Cursor_Move>().now_index;
        Monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;
        StartCoroutine(Character_Attacking());
    }
    IEnumerator Character_Moving()
    {
        chr_animator.SetBool("Move", true);
        int Monster_index = GameObject.Find("Cursor").GetComponent<Cursor_Move>().now_index;
        Monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;
        float Distance = Monster.transform.position.x - character.transform.position.x;
        while (Distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Monster.transform.position + Vector3.down * 0.2f + Vector3.left * 0.5f, 3.0f * Time.deltaTime);

            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Monster.transform.position.x - character.transform.position.x;
        }
        chr_animator.SetBool("Move", false);
        //#5. 공격 완료 후 카드를 다시 뒤집기 위해 메세지 전달
        StartCoroutine(Character_Attacking());
    }
    IEnumerator Character_Attacking()
    {
        chr_animator.SetBool("Attack", true);
        int attackCount = 1;
        while (attackCount <= 1)
        {
            Debug.LogFormat("{0} Attacking", character.name);
            attackCount += 1;
            yield return new WaitForSecondsRealtime(0.6f);
            Attack_Monster();
        }
        chr_animator.SetBool("Attack", false);
        //#5. 공격 완료 후 카드를 다시 뒤집기 위해 메세지 전달, solider인 경우 뒤로 움직인 후에 메세지 전달
        if (isSolider)
        {
            StartCoroutine(Character_Moving_Back());
        }
        else
        {
            GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("ResetAnimation_Success", success_card_set);
        }
    }
    IEnumerator Character_Moving_Back()
    {
        chr_animator.SetBool("Move", true);
        float Distance = Vector3.Distance(character.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(character.transform.position, first_position);
        }
        chr_animator.SetBool("Move", false);
        //#5. 공격 완료 후 카드를 다시 뒤집기 위해 메세지 전달
        GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("ResetAnimation_Success", success_card_set);
    }

    void Attack_Monster()
    {
        object[] skill_info = new object[4];
        skill_info[0] = DAMAGE_1_Tier;
        skill_info[1] = DAMAGE_2_Tier;
        skill_info[2] = DAMAGE_3_Tier;
        skill_info[3] = skill_Tier;
        Monster.GetComponent<Slime>().SendMessage("damaged_start", skill_info);



        //거리가 가장 가까운 슬라임에게 데미지 전달, 추후 수정

        /*slimes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Slime"));
        GameObject firstslime = slimes[0];
        if (slimes.Count != 0)
        {
            float shortDis = Vector3.Distance(transform.position, slimes[0].transform.position);
            foreach (GameObject slime in slimes)
            {
                float Distance = Vector3.Distance(transform.position, slime.transform.position);

                if (Distance < shortDis)
                {
                    shortDis = Distance;
                    firstslime = slime;
                }
            }
        }
        firstslime.GetComponent<Slime>().SendMessage("damaged_start");*/
    }

    void damaged_start(int DAMAGE)
    {
        Debug.Log(DAMAGE);
        chr_animator.SetBool("Damaged", true);
        HP -= DAMAGE;
        Invoke("damaged_end", 0.4f);
    }
    void damaged_end()
    {
        chr_animator.SetBool("Damaged", false);
        if (HP <= 0)
        {
            Character_Die();
        }
    }

    void Character_Die()
    {
        Debug.Log(character.name + " Die");
        chr_animator.SetBool("Die", true);
        chr_Died = true;
        //죽은 다음에 해당 캐릭터의 카드는 회색으로 변경
        GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("ChangeCard_Chracter_Die", character.name);
    }

    void FirstMove()
    {
        StartCoroutine(Character_FirstMoving());
    }

    IEnumerator Character_FirstMoving()
    {
        float Distance = Vector3.Distance(character.transform.position, first_position);
        while (Distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, first_position, 3.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
            Distance = Vector3.Distance(character.transform.position, first_position);
        }
    }
}
