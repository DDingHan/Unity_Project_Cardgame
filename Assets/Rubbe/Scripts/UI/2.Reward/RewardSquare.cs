using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSquare : MonoBehaviour
{
    //public Text goldTxt;
    public int mapNum;
    public int stageNum;
    public GameObject Rewards;
    public TextMeshPro Reward_Count;
    public TextMeshPro Reward_Name;
    public GameObject[] Reward_Squares;
    public GameObject[] Gems;
    public GameObject[] Pieces;
    public string[,] Names= new string[2, 3] {
        {"붉은 보석","푸른 보석","노란 보석"},
        {"붉은 조각","푸른 조각","노란 조각"}
    };
    public Vector3[] Image_pos;
    public Vector3[] Count_pos;
    public Vector3[] Name_pos;

    
    
    // Start is called before the first frame update
    void Start()
    {
        stageNum = int.Parse(GameObject.Find("GameData").GetComponent<Data>().stageNum);
        mapNum = int.Parse(GameObject.Find("GameData").GetComponent<Data>().mapName);
        if (mapNum == 1)
        {
            if (stageNum == 1 || stageNum == 3 || stageNum == 5 || stageNum == 7 )
                {
                    GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 0, 0, 0 };
                }
            else if (stageNum == 2)
            {
                makeSquare(Pieces[1], "X1", Names[1, 0],1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 1, 0, 0 };
            }
            else if (stageNum == 4)
            {
                makeSquare(Pieces[2], "X1", Names[1, 1], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 0, 1, 0 };
            }
            else if (stageNum == 6)
            {
                makeSquare(Pieces[3], "X1", Names[1, 2], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 0, 0, 1 };
            }
            else if (stageNum == 8)
            {
                makeSquare(Gems[1], "X1", Names[0, 0], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 1, 0, 0, 0, 0, 0 };
            }
        }
        if (mapNum == 2)
        {
            if (stageNum == 1)
            {
                makeSquare( Pieces[1], "X2", Names[1, 0], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 2, 0, 0 };
            }
            else if (stageNum == 2)
            {
                makeSquare(Pieces[2], "X2", Names[1, 1], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 0, 2, 0 };
            }
            else if (stageNum == 3)
            {
                makeSquare(Pieces[3], "X2", Names[1, 2], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 0, 0, 2 };
            }
            else if (stageNum == 4)
            {
                makeSquare(Gems[2], "X1", Names[0, 1], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 1, 0, 0, 0, 0 };
            }
            else if (stageNum == 5)
            {
                makeSquare(Pieces[1], "X2", Names[1, 0], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 2, 0, 0 };
            }
            else if (stageNum == 6)
            {
                makeSquare(Pieces[2], "X2", Names[1, 1], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 0, 2, 0 };
            }
            else if (stageNum == 7)
            {
                makeSquare(Pieces[3], "X2", Names[1, 2], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 0, 0, 0, 2 };
            }
            else if (stageNum == 8)
            {
                makeSquare(Gems[3], "X1", Names[0, 2], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 1, 0, 0, 0 };
            }
        }

        if (mapNum == 3)
        {
            if (stageNum == 1)
            {
                makeSquare(Gems[1], "X1", Names[0, 0], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 1, 0, 0, 0, 0, 0 };
            }
            else if (stageNum == 2)
            {
                makeSquare(Gems[2], "X1", Names[0, 1], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 1, 0, 0, 0, 0 };
            }
            else if (stageNum == 3)
            {
                makeSquare(Gems[3], "X1", Names[0, 2], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 1, 0, 0, 0 };
            }
            else if (stageNum == 4)
            {
                makeSquare(Gems[1], "X2", Names[0, 0], 1);
                makeSquare(Gems[2], "X2", Names[0, 1], 2);
                makeSquare(Gems[3], "X2", Names[0, 2], 3);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 2, 2, 2, 0, 0, 0 };
            }
            else if (stageNum == 5)
            {
                makeSquare(Gems[1], "X3", Names[0, 0], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 3, 0, 0, 0, 0, 0 };
            }
            else if (stageNum == 6)
            {
                makeSquare(Gems[2], "X3", Names[0, 1], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 3, 0, 0, 0, 0 };
            }
            else if (stageNum == 7)
            {
                makeSquare(Gems[3], "X3", Names[0, 2], 1);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 0, 0, 3, 0, 0, 0 };
            }
            else if (stageNum == 8)
            {
                makeSquare(Gems[1], "X3", Names[0, 0], 1);
                makeSquare(Gems[2], "X3", Names[0, 1], 2);
                makeSquare(Gems[3], "X3", Names[0, 2], 3);
                GameObject.Find("RewardData").GetComponent<RewardData>().clearGem = new int[] { 3, 3, 3, 0, 0, 0 };
            }
        }
    }

    void makeSquare(GameObject Gem, string Count, string Name, int index)
    {
        GameObject.Instantiate(Gem, Image_pos[index], Quaternion.identity).transform.parent = Reward_Squares[index].transform;
        Reward_Count.text = Count;
        GameObject.Instantiate(Reward_Count, Count_pos[index], Quaternion.identity).transform.parent = Reward_Squares[index].transform;
        Reward_Name.text = Name;
        GameObject.Instantiate(Reward_Name, Name_pos[index], Quaternion.identity).transform.parent = Reward_Squares[index].transform;
        Reward_Squares[index].SetActive(true);
    }

}
