using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public GameObject Monsters;
    public GameObject[] Monster_preb;
    public Vector3[] Monster_pos;
    public int stage_num;
    public int map_num;

    // Start is called before the first frame update
    void Start()
    {
        //몬스터 위치 설정
        Monster_pos = new Vector3[4];
        Monster_pos[1] = new Vector3(2.5f,3.8f,0f);
        Monster_pos[2] = new Vector3(2.5f,2.9f,0f);
        Monster_pos[3] = new Vector3(2.5f,2.0f,0f);

        stage_num = int.Parse(GameObject.Find("GameData").GetComponent<Data>().stageNum);

        map_num = int.Parse(GameObject.Find("GameData").GetComponent<Data>().mapName);

        init(stage_num);
        Invoke("FirstMove", 0.5f);
    }

    void init(int Num)
    {
        switch (Num)
        {
            case 1:          
                GameObject.Instantiate(Monster_preb[1], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                Monsters.transform.GetChild(0).GetComponent<Slime>().SendMessage("setMaxHP", CalMinimalHP());
                break;
            case 2:
                GameObject.Instantiate(Monster_preb[1], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[1], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                Monsters.transform.GetChild(0).GetComponent<Slime>().SendMessage("setMaxHP", CalMinimalHP());
                Monsters.transform.GetChild(1).GetComponent<Slime>().SendMessage("setMaxHP", CalMinimalHP());
                break;
            case 3:
                GameObject.Instantiate(Monster_preb[2], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[1], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[1], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                Monsters.transform.GetChild(0).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                Monsters.transform.GetChild(1).GetComponent<Slime>().SendMessage("setMaxHP", CalMinimalHP());
                Monsters.transform.GetChild(2).GetComponent<Slime>().SendMessage("setMaxHP", CalMinimalHP());
                break;
            case 4:
                GameObject.Instantiate(Monster_preb[3], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                Monsters.transform.GetChild(0).GetComponent<Mushroom>().SendMessage("setMaxHP", CalMidiumHP());
                Monsters.transform.GetChild(1).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                Monsters.transform.GetChild(2).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                break;
            case 5:
                GameObject.Instantiate(Monster_preb[1], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                Monsters.transform.GetChild(0).GetComponent<Slime>().SendMessage("setMaxHP", CalMinimalHP());
                Monsters.transform.GetChild(1).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                Monsters.transform.GetChild(2).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                break;
            case 6:
                GameObject.Instantiate(Monster_preb[2], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                Monsters.transform.GetChild(0).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                Monsters.transform.GetChild(1).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                Monsters.transform.GetChild(2).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                break;
            case 7:
                GameObject.Instantiate(Monster_preb[3], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                Monsters.transform.GetChild(0).GetComponent<Mushroom>().SendMessage("setMaxHP", CalMidiumHP());
                Monsters.transform.GetChild(1).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                break;
            case 8:
                GameObject.Instantiate(Monster_preb[4], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                GameObject.Instantiate(Monster_preb[2], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                Monsters.transform.GetChild(0).GetComponent<Skeleton>().SendMessage("setMaxHP", CalBigHP());
                Monsters.transform.GetChild(1).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                Monsters.transform.GetChild(2).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                break;
        }
    }

    void FirstMove()
    {
        for (int i = 0; i < Monsters.transform.childCount; i++)
        {
            if(Monsters.transform.GetChild(i).gameObject.name == "Slime(Clone)"){
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Slime>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Slime>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "Goblin(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Goblin>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Goblin>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "Mushroom(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Mushroom>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Mushroom>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "Skeleton(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Skeleton>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Skeleton>().SendMessage("FirstMove", 1.5f);
            }
        }
    }

    public GameObject monster;
    public int Monster_index;
    void Start_Monster_Attack(int Monster_count)
    {
        Monster_index = -1;
        do //비활성화 몬스터는 공격x 하기 위해 다음 활성화 된 몬스터 찾기
        {
            Monster_index++;
            monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;
        } while (monster.activeSelf == false && Monster_index < Monster_count);
        Monster_Attack(Monster_index,Monster_count);
    }
    void Monster_Attack(int index, int Monster_count)
    {
        Monster_index = index;
        //마지막 몬스터 까지만 공격
        if (Monster_index < Monster_count)
        {
            monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;

            while (monster.activeSelf == false)
            {
                Monster_index++;
                if (Monster_index >= Monster_count) break;
                monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;
            }
            if (Monster_index < Monster_count)
            {
                Debug.LogFormat("{0} 공격", monster.name);
                if(monster.name == "Slime(Clone)")
                    monster.GetComponent<Slime>().SendMessage("Monster_Start", Monster_index);
                else if(monster.name == "Goblin(Clone)")
                    monster.GetComponent<Goblin>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "Mushroom(Clone)")
                    monster.GetComponent<Mushroom>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "Skeleton(Clone)")
                    monster.GetComponent<Skeleton>().SendMessage("Monster_Start", Monster_index);
            }
        }
        //마지막 몬스터 공격이 끝나면 플레이어 턴으로 전환
        if (Monster_index >= Monster_count)
        {
            Invoke("ChangeTurn", 1.0f);
        }
    }

    void ChangeTurn()
    {
        GameObject.Find("Turn").GetComponent<Turn>().SendMessage("ChangeTurn");
    }

    float CalMinimalHP()
    {
        if (map_num == 1)
        {
            return 20f;
        }
        else if(map_num == 2){
            return 35f;
        }
        else if (map_num == 3)
        {
            return 50f;
        }
        return 1f;
    }

    float CalSmallHP()
    {
        if (map_num == 1)
        {
            return 30f;
        }
        else if (map_num == 2)
        {
            return 45f;
        }
        else if (map_num == 3)
        {
            return 70f;
        }
        return 1f;
    }

    float CalMidiumHP()
    {
        if (map_num == 1)
        {
            return 50f;
        }
        else if (map_num == 2)
        {
            return 70f;
        }
        else if (map_num == 3)
        {
            return 90f;
        }
        return 1f;
    }

    float CalBigHP()
    {
        if (map_num == 1)
        {
            return 70f;
        }
        else if (map_num == 2)
        {
            return 90f;
        }
        else if (map_num == 3)
        {
            return 110f;
        }
        return 1f;
    }
}
