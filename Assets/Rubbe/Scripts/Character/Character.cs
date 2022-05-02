using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject character;    
    Animator chr_animator;
    object[] success_card_set = new object[2];
    List<GameObject> slimes;
    float firstX;
    bool isSolider = false;
    public bool chr_Died = false;

    public int HP = 100;
    public int DAMAGE_1_Tier = 10;
    public int DAMAGE_2_Tier = 20;
    public int DAMAGE_3_Tier = 30;
    public float skill_Tier;

    int damaged_count = 0;

    private void Start()
    {
        chr_animator = GetComponent<Animator>();
    }
    //#4-1. solider�϶��� �����̰� ������ �ٽ� ����ġ�� ����
    void Move(object[] index)
    {
        firstX = character.transform.position.x;
        isSolider = true;
        success_card_set[0] = index[0];
        success_card_set[1] = index[1];
        skill_Tier = (float)index[2];
        StartCoroutine(Character_Moving());
    }

    //#4 0.6�ʿ� �ѹ��� ���� �ݺ� (������ ���� Ƚ����ŭ �ݺ�)
    void Attack(object[] index)    
    {
        success_card_set[0] = index[0];
        success_card_set[1] = index[1];
        skill_Tier = (float)index[2];
        StartCoroutine(Character_Attacking());
    }
    IEnumerator Character_Moving()
    {
        chr_animator.SetBool("Move", true);
        GameObject enemy = GameObject.Find("Slime1");
        float shortDis = Vector3.Distance(character.transform.position, enemy.transform.position);
        while (shortDis >= 0.5f)
        {
            character.transform.position += Vector3.right * 5.0f * Time.deltaTime;
            yield return new WaitForSecondsRealtime(0.01f);
            shortDis = Vector3.Distance(character.transform.position, enemy.transform.position);
        }      
        chr_animator.SetBool("Move", false);
        //#5. ���� �Ϸ� �� ī�带 �ٽ� ������ ���� �޼��� ����
        StartCoroutine(Character_Attacking());
    }
    IEnumerator Character_Attacking()
    {
        chr_animator.SetBool("Attack", true);
        int attackCount = 1;
        while (attackCount <= 1)
        {            
            Debug.LogFormat("{0} Attacking",character.name);
            attackCount += 1;
            yield return new WaitForSecondsRealtime(0.6f);
            Attack_Slime();
        }
        chr_animator.SetBool("Attack", false);
        //#5. ���� �Ϸ� �� ī�带 �ٽ� ������ ���� �޼��� ����, solider�� ��� �ڷ� ������ �Ŀ� �޼��� ����
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
        while (character.transform.position.x >= firstX)
        {
            character.transform.position -= Vector3.right * 5.0f * Time.deltaTime;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        chr_animator.SetBool("Move", false);
        //#5. ���� �Ϸ� �� ī�带 �ٽ� ������ ���� �޼��� ����
        GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("ResetAnimation_Success", success_card_set);
    }

    void Attack_Slime()
    {
        object[] skill_info = new object[4];
        skill_info[0] = DAMAGE_1_Tier;
        skill_info[1] = DAMAGE_2_Tier;
        skill_info[2] = DAMAGE_3_Tier;
        skill_info[3] = skill_Tier;
        if (character.name == "Soldier")
        {
            GameObject.Find("Slime1").GetComponent<Slime>().SendMessage("damaged_start",skill_info);
        }
        else if(character.name == "Archer") 
        {
            GameObject.Find("Slime2").GetComponent<Slime>().SendMessage("damaged_start", skill_info);
        }
        else if (character.name == "Mage")
        {
            GameObject.Find("Slime3").GetComponent<Slime>().SendMessage("damaged_start", skill_info);
        }



        //�Ÿ��� ���� ����� �����ӿ��� ������ ����, ���� ����

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

    void damaged_start(int Slime_DAMAGE)
    {
        Debug.Log(Slime_DAMAGE);
        chr_animator.SetBool("Damaged", true);
        HP -= Slime_DAMAGE;
        Invoke("damaged_end", 0.4f);
    }
    void damaged_end()
    {
        chr_animator.SetBool("Damaged", false);
        if(HP <= 0)
        {
            Character_Die();
        }
    }

    void Character_Die()
    {
        Debug.Log(character.name + " Die");
        chr_animator.SetBool("Die", true);
        chr_Died = true;
        //���� ������ �ش� ĳ������ ī��� ȸ������ ����
        GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("ChangeCard_Chracter_Die", character.name);
    }
}
