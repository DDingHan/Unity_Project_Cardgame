using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public Image chr;
    public Text cardName;
    public GameObject character;
    Animator card_animator;

    public Text cardBackName;
    

    public int cardIndex;

    private void Start()
    {
        card_animator = GetComponent<Animator>();
    }
    // 카드의 정보를 초기화
    // 랜덤셀렉터에서 게임 시작 후 카드를 처음 생성할 때, 카드 두 짝을 맞출 때 새로 카드 생성시 사용됨.
    public void CardUISet(Card card)
    {
        chr.sprite = card.cardImage;
        cardName.text = card.cardName;
        character = card.character;

        cardBackName.text = card.cardName;
    }
    // 카드가 클릭되면 뒤집는 애니메이션 재생
    public void OnPointerDown(PointerEventData eventData)
    {
        //처음 카드를 선택했을 때 내 정보를 랜덤 셀렉터의 변수에 집어넣음.
        if (GameObject.Find("Deck").GetComponent<RandomSelect>().Checking_Clicked_Card == false)
        {
            card_animator.SetTrigger("Flip");
            GameObject.Find("Deck").GetComponent<RandomSelect>().Checking_Clicked_Card = true;
            GameObject.Find("Deck").GetComponent<RandomSelect>().CardName = cardName.text;
            GameObject.Find("Deck").GetComponent<RandomSelect>().firstCardIndex = cardIndex;
        }
        //두 번째 카드를 선택했을 때 내 정보와 랜덤 셀렉터에 있는 처음 선택된 카드의 정보를 비교
        else
        {
            //처음 선택한 카드를 다시 선택했을 경우
            if (GameObject.Find("Deck").GetComponent<RandomSelect>().firstCardIndex == cardIndex)
            {
                Debug.Log("중복이야");
                return;
            }

            card_animator.SetTrigger("Flip");
            //카드 두 짝이 맞았을 때 작동. 카드 뒤집고 카드 내용을 바꿈.
            if (GameObject.Find("Deck").GetComponent<RandomSelect>().CardName == cardName.text)
            {
                //완성된 카드셋이 지금 없을때
                if (!GameObject.Find("Deck").GetComponent<RandomSelect>().successCardSet)
                {
                    //카드에 해당되는 캐릭터가 안죽은 경우에만 성공
                    if (!character.GetComponent<Character>().chr_Died)
                    {
                        Debug.Log("성공");
                        GameObject.Find("Deck").GetComponent<RandomSelect>().successCardSet = true;
                        GameObject.Find("Deck").GetComponent<RandomSelect>().Checking_Clicked_Card = false;
                        GameObject.Find("Deck").GetComponent<RandomSelect>().CardName = "";
                        GameObject.Find("Deck").GetComponent<RandomSelect>().secondCardIndex = cardIndex;
                        //#1. 카드 두 짝이 맞았을 때 메세지 전달
                        GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("StartReset_Success");
                    }
                    else
                    {
                        Debug.Log("캐릭터가 죽어서 실행할 수 없어요...ㅜ");
                        GameObject.Find("Deck").GetComponent<RandomSelect>().Checking_Clicked_Card = false;
                        GameObject.Find("Deck").GetComponent<RandomSelect>().CardName = "";
                        GameObject.Find("Deck").GetComponent<RandomSelect>().secondCardIndex = cardIndex;
                        GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("StartReset");
                    }
                    
                }
                else if (GameObject.Find("Deck").GetComponent<SkillSet>().AttackSubject == character.name&& GameObject.Find("Deck").GetComponent<SkillSet>().skill_Tier!=0)
                {
                    GameObject.Find("Deck").GetComponent<RandomSelect>().Checking_Clicked_Card = false;
                    GameObject.Find("Deck").GetComponent<RandomSelect>().CardName = "";
                    GameObject.Find("Deck").GetComponent<RandomSelect>().secondCardIndex = cardIndex;
                    GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("StartReset_Success_Next",character.name);
                }
                //이미 완성된 카드 셋이 발동중일때
                else
                {
                    Debug.Log("다른 카드 실행중");
                    GameObject.Find("Deck").GetComponent<RandomSelect>().Checking_Clicked_Card = false;
                    GameObject.Find("Deck").GetComponent<RandomSelect>().CardName = "";
                    GameObject.Find("Deck").GetComponent<RandomSelect>().secondCardIndex = cardIndex;
                    GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("StartReset");
                }
                     
            }
            //카드 두 짝이 틀렸을 때 작동. 카드 뒤집기만 발동.
            else
            {
                Debug.Log("실패");
                GameObject.Find("Deck").GetComponent<RandomSelect>().Checking_Clicked_Card = false;
                GameObject.Find("Deck").GetComponent<RandomSelect>().CardName = "";
                GameObject.Find("Deck").GetComponent<RandomSelect>().secondCardIndex = cardIndex;
                GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("StartReset");
            }
        }
    }
    
    //카드 짝이 틀렸을 때 
    public void ResetAnimation()
    {
        Invoke("reset", 0.5f);
    }
    
    void reset()
    {
        card_animator.SetTrigger("Return");
    }


    //카드 짝이 맞았을때
    //#3. 뒤집힌 카드 인덱스를 저장하고 캐릭터 공격 실행
    object[] success_card_set = new object[2];        
    void Saving_First(object[] index)           //FirstCardindex에 해당하는 카드일 경우
    {
        success_card_set[0] = index[0];
        success_card_set[1] = index[1];
        /*        if(character.name == "Soldier")
                {
                    character.GetComponent<Character>().SendMessage("Move", success_card_set);
                    Invoke("invoke_Skill", 1.0f);
                }
                else
                {
                    character.GetComponent<Character>().SendMessage("Attack",success_card_set);
                }*/
        //Invoke("invoke_Skill", 1.0f);
        invoke_Skill();
    }
    void Saving_Second(object[] index)         //secondCardindex에 해당하는 카드일 경우
    {
        success_card_set[0] = index[0];
        success_card_set[1] = index[1];
    }

    void StartCharacterAnimation(float skill_Tier)
    {
        Debug.Log("애니메이션 시작");
        object[] success_card_set_and_Skill_Tier = new object[3];
        success_card_set_and_Skill_Tier[0] = success_card_set[0];
        success_card_set_and_Skill_Tier[1] = success_card_set[1];
        success_card_set_and_Skill_Tier[2] = skill_Tier;

        if (character.name == "Soldier")
        {
            character.GetComponent<Character>().SendMessage("Move", success_card_set_and_Skill_Tier);
            //Invoke("invoke_Skill", 1.0f);
        }
        else
        {
            character.GetComponent<Character>().SendMessage("Attack", success_card_set_and_Skill_Tier);
        }
    }

    //#7. 카드 뒤집기 후 카드의 정보를 다시 설정하기 위해 메세지 전달
    void reset_First_Success()                        
    {
        card_animator.SetTrigger("Return");     //FirstCardindex에 해당하는 카드일 경우
    }
    void reset_Second_Success()                       
    {
        card_animator.SetTrigger("Return");     //secondCardindex에 해당하는 카드일 경우
        GameObject.Find("Deck").GetComponent<RandomSelect>().SendMessage("Change", success_card_set);
    }

    void invoke_Skill()
    {
        Debug.Log("스킬 들어갑니다.");
        /*if (character.name == "Soldier")
        {
            GameObject.Find("Deck").GetComponent<SkillSet>().SendMessage("invoke_Skill",character.name);
        }*/
        GameObject.Find("Deck").GetComponent<SkillSet>().SendMessage("invoke_Skill", character.name);
    }
}
