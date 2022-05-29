using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSet : MonoBehaviour
{
    public GameObject Warrior_Skill_Tier1;

    public GameObject Warrior_Skill_Tier2;

    public GameObject Warrior_Skill_Tier3;

    public GameObject Archer_Skill_Tier1;

    public GameObject Archer_Skill_Tier2;

    public GameObject Archer_Skill_Tier3;

    public GameObject Magician_Skill_Tier1;

    public GameObject Magician_Skill_Tier2;

    public GameObject Magician_Skill_Tier3;

    public float Timer;
    public float delayTime = 2f;
    public bool isDelay = false;

    public string AttackSubject = "";
    public float skill_Tier = 0;
    public int cardIndex;
    object[] success_card_set = new object[2];
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (skill_Tier!=0)
        {
            //Debug.Log(Timer);
            Timer = Timer + Time.deltaTime;
            if (Timer >= delayTime)
            {
                if (GameObject.Find("Timer").GetComponent<Timer>().time > 1)
                {
                    Timer = 0f;
                    isDelay = true;
                    Debug.Log("스킬 티어 " + skill_Tier + " 발동중");
                    GameObject.Find("Deck").GetComponent<RandomSelect>().CardUIList[cardIndex].SendMessage("StartCharacterAnimation", skill_Tier);
                    GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("Skill_Tier", skill_Tier);
                    make_Skill_Effect();
                    isDelay = false;
                    skill_Tier = 0;
                }
                else
                {
                    Timer = 0f;
                    Debug.Log("스킬 시전 중 타이머 종료");
                    GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("Skill_Tier", skill_Tier);
                    GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("ResetAnimation_Success", success_card_set);
                    isDelay = false;
                    skill_Tier = 0;
                }
            }
        }
    }

    void invoke_Skill(object[] index)
    {
        //Instantiate(Warrior_Skill_Tier1, Warrior_Skill_Tier1.transform.position, Warrior_Skill_Tier1.transform.rotation);
        success_card_set[0] = index[0];
        success_card_set[1] = index[1];
        AttackSubject = (string)index[2];
        if (isDelay == false)
        {
            skill_Tier = 1;
            isDelay = true;
            cardIndex = GameObject.Find("Deck").GetComponent<RandomSelect>().firstCardIndex;
            Debug.Log("코루틴 시작");
            StartCoroutine("AttackDelay");
        }
        else
        {
            Debug.Log("오류발생 SkillSet 스크립트의 invoke_Skill");
        }
    }

    IEnumerator AttackDelay()
    {
        isDelay = false;
        while (!isDelay)
        {
            yield return new WaitForSeconds(delayTime);
        }
    }

    void invoke_Skill_Next(string CharacterName)
    {
        if (AttackSubject == CharacterName)
        {
            if (skill_Tier == 1)
            {
                skill_Tier = 2;
                //Timer = 0;
                //StopCoroutine("AttackDelay");
                Debug.Log("멈추고 다시 실행");
                isDelay = true;
                Timer = 0;
                Debug.Log("코루틴 시작");
                StartCoroutine("AttackDelay");
            }
            else
            {
                skill_Tier = 3;
                //StopCoroutine("AttackDelay");
                if (GameObject.Find("Timer").GetComponent<Timer>().time > 1)
                {
                    isDelay = true;
                    Debug.Log("3티어 스킬 실행~");
                    GameObject.Find("Deck").GetComponent<RandomSelect>().CardUIList[cardIndex].SendMessage("StartCharacterAnimation", skill_Tier);
                    GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("Skill_Tier", skill_Tier);
                    make_Skill_Effect();
                    isDelay = false;
                    skill_Tier = 0;
                }
                else
                {
                    Timer = 0f;
                    Debug.Log("스킬 시전 중 타이머 종료");
                    GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("Skill_Tier", skill_Tier);
                    GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("ResetAnimation_Success", success_card_set);
                    isDelay = false;
                    skill_Tier = 0;
                }
            }
        }
    }

    public GameObject hudDamageText;
    public float save_skill_Tier;

    void make_Skill_Effect()
    {
        if (AttackSubject == "Soldier")
        {
            if (skill_Tier == 1)
            {
                Instantiate(Warrior_Skill_Tier1, Warrior_Skill_Tier1.transform.position, Warrior_Skill_Tier1.transform.rotation);
            }
            else if (skill_Tier == 2)
            {
                Instantiate(Warrior_Skill_Tier2, Warrior_Skill_Tier2.transform.position, Warrior_Skill_Tier2.transform.rotation);
            }
            else if (skill_Tier == 3)
            {
                Instantiate(Warrior_Skill_Tier3, Warrior_Skill_Tier3.transform.position, Warrior_Skill_Tier3.transform.rotation);
            }
        }
        else if (AttackSubject == "Archer")
        {
            if (skill_Tier == 1)
            {
                Instantiate(Archer_Skill_Tier1, Archer_Skill_Tier1.transform.position, Archer_Skill_Tier1.transform.rotation);
            }
            else if (skill_Tier == 2)
            {
                Instantiate(Archer_Skill_Tier2, Archer_Skill_Tier2.transform.position, Archer_Skill_Tier2.transform.rotation);
            }
            else if (skill_Tier == 3)
            {
                Instantiate(Archer_Skill_Tier3, Archer_Skill_Tier3.transform.position, Archer_Skill_Tier3.transform.rotation);
            }
        }
        else if (AttackSubject == "Mage")
        {
            if (skill_Tier == 1)
            {
                Instantiate(Magician_Skill_Tier1, Magician_Skill_Tier1.transform.position, Magician_Skill_Tier1.transform.rotation);
            }
            else if (skill_Tier == 2)
            {
                Instantiate(Magician_Skill_Tier2, Magician_Skill_Tier2.transform.position, Magician_Skill_Tier2.transform.rotation);
            }
            else if (skill_Tier == 3)
            {
                Instantiate(Magician_Skill_Tier3, Magician_Skill_Tier3.transform.position, Magician_Skill_Tier3.transform.rotation);
            }
        }
        //GameObject hudText = Instantiate(hudDamageText);
        //hudText.GetComponent<DamageText>().damage = 5;
        save_skill_Tier = skill_Tier;
        Invoke("TakeDamage", 0.7f);
    }

    public GameObject cursor;
    void TakeDamage()
    {
        GameObject hudText = Instantiate(hudDamageText,cursor.transform.position,hudDamageText.transform.rotation);
        if (save_skill_Tier == 1)
        {
            hudText.GetComponent<DamageText>().damage = GameObject.Find(AttackSubject).GetComponent<Character>().DAMAGE_1_Tier;
        }
        else if (save_skill_Tier == 2)
        {
            hudText.GetComponent<DamageText>().damage = GameObject.Find(AttackSubject).GetComponent<Character>().DAMAGE_2_Tier;
        }
        else if (save_skill_Tier == 3)
        {
            hudText.GetComponent<DamageText>().damage = GameObject.Find(AttackSubject).GetComponent<Character>().DAMAGE_3_Tier;
        }
        
    }
}
