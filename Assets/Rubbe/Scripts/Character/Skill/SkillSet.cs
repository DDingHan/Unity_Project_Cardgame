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
                Timer = 0f;
                isDelay = true;
                Debug.Log("스킬 티어 " + skill_Tier + " 발동중");
                GameObject.Find("Deck").GetComponent<RandomSelect>().CardUIList[cardIndex].SendMessage("StartCharacterAnimation");
                GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("Skill_Tier",skill_Tier);
                make_Skill_Effect();
                isDelay = false;
                skill_Tier = 0;
            }
        }
    }

    void invoke_Skill(string CharacterName)
    {
        //Instantiate(Warrior_Skill_Tier1, Warrior_Skill_Tier1.transform.position, Warrior_Skill_Tier1.transform.rotation);
        AttackSubject = CharacterName;
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
                isDelay = true;
                Debug.Log("3티어 스킬 실행~");
                GameObject.Find("Deck").GetComponent<RandomSelect>().CardUIList[cardIndex].SendMessage("StartCharacterAnimation");
                GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("Skill_Tier", skill_Tier);
                make_Skill_Effect();
                isDelay = false;
                skill_Tier = 0;
            }
        }
    }

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
    }
}
