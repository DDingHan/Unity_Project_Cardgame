using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject slime;
    public GameObject character;
    public float firstX;
    Animator slime_animator;

    private void Start()
    {
        slime_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //예시용으로 A,S,D를 눌렀을때 슬라임 공격 발동
        if (Input.GetKeyDown(KeyCode.A) && slime.name == "Slime1")
        {
            firstX = slime.transform.position.x;
            character = GameObject.Find("Soldier");
            StartCoroutine(Slime_Moving());
        }
        else if (Input.GetKeyDown(KeyCode.S) && slime.name == "Slime2")
        {
            firstX = slime.transform.position.x;
            character = GameObject.Find("Archer");
            StartCoroutine(Slime_Moving());
        }
        else if (Input.GetKeyDown(KeyCode.D) && slime.name == "Slime3")
        {
            firstX = slime.transform.position.x;
            character = GameObject.Find("Mage");
            StartCoroutine(Slime_Moving());
        }
    }

    void damaged_start()
    {
        slime_animator.SetBool("Damaged", true);
        Invoke("damaged_end", 0.2f);
    }
    void damaged_end()
    {
        slime_animator.SetBool("Damaged", false);
    }

    IEnumerator Slime_Moving()
    {
        float shortDis = Vector3.Distance(slime.transform.position, character.transform.position);
        while (shortDis >= 0.5f)
        {
            slime.transform.position += Vector3.left * 5.0f * Time.deltaTime;
            yield return new WaitForSecondsRealtime(0.01f);
            shortDis = Vector3.Distance(slime.transform.position, character.transform.position);
        }

        StartCoroutine(Slime_Attacking());
    }
    
    IEnumerator Slime_Attacking()
    {
        int slime_attackCount = 1;
        while (slime_attackCount <= 3)
        {
            Attack_Slime();
            Debug.LogFormat("Slime_Attackcount = {0}", slime_attackCount);
            slime_attackCount += 1;
            yield return new WaitForSecondsRealtime(0.6f);
        }
        StartCoroutine(Slime_Moving_Back());
    }

    IEnumerator Slime_Moving_Back()
    {
        while (slime.transform.position.x <= firstX)
        {
            slime.transform.position -= Vector3.left * 5.0f * Time.deltaTime;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    void Attack_Slime()
    {
        character.GetComponent<Character>().SendMessage("damaged_start");
    }

}
