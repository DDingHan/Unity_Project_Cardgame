using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSelect : MonoBehaviour
{
    public List<Card> deck = new List<Card>();  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합

    void Start()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            // 스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줍니다.
            total += deck[i].weight;
        }
        // 실행
        ResultSelect();
    }

    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트

    public Transform parent;
    public GameObject cardprefab;

    public bool Checking_Clicked_Card = false; // 카드가 처음 선택된 건지 아닌지 판단하는 변수.
    public bool successCardSet = false;         //완성된 카드셋이 있는지 판단하는 변수
    public string CardName = ""; // 처음 카드가 선택 됐을때의 카드 이름을 임시저장하는 변수.

    public int firstCardIndex; //밑의 CardUIList에 접근하기 위한 변수.
    public int secondCardIndex; //밑의 CardUIList에 접근하기 위한 변수.

    public List<CardUI> CardUIList = new List<CardUI>(); //화면에 나오는 각각의 CardUI에 접근하기 위해 리스트를 생성함.

    public Sprite img_live;
    public Sprite img_die;

    public void ResultSelect()
    {
        for (int i = 0; i < 24; i++)
        {
            // 가중치 랜덤을 돌리면서 결과 리스트에 넣어줍니다.
            result.Add(RandomCard()); 
            // 비어 있는 카드를 생성하고
            CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
            // 생성 된 카드에 결과 리스트의 정보를 넣어줍니다.
            cardUI.CardUISet(result[i]);
            cardUI.cardIndex = i;
            CardUIList.Add(cardUI);
        }
    }
    // 가중치 랜덤의 설명은 영상을 참고.
    // 가중치를 통해 랜덤한 카드를 생성하는 함수. 
    public Card RandomCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            if (selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                return temp;
            }
        }
        return null;
    }

    public void StartReset()
    {
        CardUIList[firstCardIndex].SendMessage("ResetAnimation");
        CardUIList[secondCardIndex].SendMessage("ResetAnimation");
    }

    //#2. 현재 뒤집은 카드 인덱스를 저장하기
    public object[] success_card_set = new object[2];
    public Slider slider;
    public void StartReset_Success()
    {
        success_card_set[0] = firstCardIndex;
        success_card_set[1] = secondCardIndex;
        CardUIList[firstCardIndex].SendMessage("Saving_First",success_card_set);
        CardUIList[secondCardIndex].SendMessage("Saving_Second", success_card_set);
        slider.value += 0.1f;
        if (slider.value == 1)
        {
            GameObject.Find("Screen_Effect").GetComponent<FeverTime>().SendMessage("Screen_Effect");
           // slider.value = 0;
        }
    }

    object[] card_set_1 = new object[2];
    object[] card_set_2 = new object[2];
    public void StartReset_Success_Next(string name)
    {
        //object[] card_set = new object[2];
        if (GameObject.Find("Deck").GetComponent<SkillSet>().skill_Tier == 1)
        {
            card_set_1[0] = firstCardIndex;
            card_set_1[1] = secondCardIndex;
            GameObject.Find("Deck").GetComponent<SkillSet>().SendMessage("invoke_Skill_Next", name);
        }
        else if(GameObject.Find("Deck").GetComponent<SkillSet>().skill_Tier == 2)
        {
            Debug.Log("3콤보 완성");
            card_set_2[0] = firstCardIndex;
            card_set_2[1] = secondCardIndex;
            GameObject.Find("Deck").GetComponent<SkillSet>().SendMessage("invoke_Skill_Next", name);
        }
        if (slider.value != 0)
        {
            slider.value += 0.1f;
            if (slider.value == 1)
            {
                GameObject.Find("Screen_Effect").GetComponent<FeverTime>().SendMessage("Screen_Effect");
                //slider.value = 0;
            }
        }
    }

    float skill_Tier;
    public void Skill_Tier(int tier)
    {
        skill_Tier = tier;
    }
    
    //#6. 뒤집힌 카드 2개를 뒤집기 위해 각각 메세지 전달
    public void ResetAnimation_Success(object[] index)   
    {
        CardUIList[(int)index[0]].SendMessage("reset_First_Success");
        CardUIList[(int)index[1]].SendMessage("reset_Second_Success");
        if (skill_Tier >= 2)
        {
            //Debug.Log("스킬 2 리셋해야해");
            CardUIList[(int)card_set_1[0]].SendMessage("reset_First_Success");
            CardUIList[(int)card_set_1[1]].SendMessage("reset_First_Success");
            Change(card_set_1);
            if (skill_Tier == 3)
            {
                CardUIList[(int)card_set_2[0]].SendMessage("reset_First_Success");
                CardUIList[(int)card_set_2[1]].SendMessage("reset_First_Success");
                Change(card_set_2);
            }
        }
    }

    //#8. 카드 정보 다시 설정
    int[] cardIndex = new int[2];
    public void Change(object[] index)                          
    {
        cardIndex[0] = (int)index[0];
        cardIndex[1] = (int)index[1];
        //Invoke("ChangeCard", 1.0f);
        ChangeCard();
    }

    public void ChangeCard()                                   
    {
        CardUIList[cardIndex[0]].CardUISet(RandomCard());
        CardUIList[cardIndex[1]].CardUISet(RandomCard());
        successCardSet = false;
        Debug.Log("변환 완료");
    }

    public void ChangeCard_Chracter_Die(string name)
    {
        for (int i = 0; i < CardUIList.Count; i++)
        {
            if (CardUIList[i].character.name == name)
            {
                CardUIList[i].chr.sprite = img_die;
            }
        }
        Debug.Log("죽은 캐릭터의 카드를 회색으로 변환 완료");
    }
}
