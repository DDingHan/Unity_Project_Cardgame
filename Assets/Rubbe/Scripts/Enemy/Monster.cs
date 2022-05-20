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
        Monster_pos[1] = new Vector3(2.5f, 3.8f, -0.1f);
        Monster_pos[2] = new Vector3(2.5f, 3.0f, -0.1f);
        Monster_pos[3] = new Vector3(2.5f, 2.0f, -0.1f);

        //stage_num = int.Parse(GameObject.Find("GameData").GetComponent<Data>().stageNum);

        //map_num = int.Parse(GameObject.Find("GameData").GetComponent<Data>().mapName);

        init(map_num,stage_num);
        Invoke("FirstMove", 0.5f);
    }

    void init(int mapNum, int stageNum)
    {
        switch (mapNum)
        {
            case 1:
                switch (stageNum)
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
                        Monsters.transform.GetChild(0).GetComponent<BossSlime>().SendMessage("setMaxHP", CalBigHP());
                        Monsters.transform.GetChild(1).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<Goblin>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                }
                break;
            case 2:
                switch (stageNum)
                {
                    case 1:
                        GameObject.Instantiate(Monster_preb[5], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<FlyingEye>().SendMessage("setMaxHP", CalMinimalHP());
                        break;
                    case 2:
                        GameObject.Instantiate(Monster_preb[5], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[5], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<FlyingEye>().SendMessage("setMaxHP", CalMinimalHP());
                        Monsters.transform.GetChild(1).GetComponent<FlyingEye>().SendMessage("setMaxHP", CalMinimalHP());
                        break;
                    case 3:
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[5], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[5], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(1).GetComponent<FlyingEye>().SendMessage("setMaxHP", CalMinimalHP());
                        Monsters.transform.GetChild(2).GetComponent<FlyingEye>().SendMessage("setMaxHP", CalMinimalHP());
                        break;
                    case 4:
                        GameObject.Instantiate(Monster_preb[7], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<Skeleton2>().SendMessage("setMaxHP", CalMidiumHP());
                        Monsters.transform.GetChild(1).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                    case 5:
                        GameObject.Instantiate(Monster_preb[5], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<FlyingEye>().SendMessage("setMaxHP", CalMinimalHP());
                        Monsters.transform.GetChild(1).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                    case 6:
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(1).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                    case 7:
                        GameObject.Instantiate(Monster_preb[7], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<Skeleton2>().SendMessage("setMaxHP", CalMidiumHP());
                        Monsters.transform.GetChild(1).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                    case 8:
                        GameObject.Instantiate(Monster_preb[8], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[6], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<DarkSoldier>().SendMessage("setMaxHP", CalBigHP());
                        Monsters.transform.GetChild(1).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<Skeleton1>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                }
                break;
            case 3:
                switch (stageNum)
                {
                    case 1:
                        GameObject.Instantiate(Monster_preb[9], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<LightBandit>().SendMessage("setMaxHP", CalMinimalHP());
                        break;
                    case 2:
                        GameObject.Instantiate(Monster_preb[9], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[9], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<LightBandit>().SendMessage("setMaxHP", CalMinimalHP());
                        Monsters.transform.GetChild(1).GetComponent<LightBandit>().SendMessage("setMaxHP", CalMinimalHP());
                        break;
                    case 3:
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[9], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[9], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(1).GetComponent<LightBandit>().SendMessage("setMaxHP", CalMinimalHP());
                        Monsters.transform.GetChild(2).GetComponent<LightBandit>().SendMessage("setMaxHP", CalMinimalHP());
                        break;
                    case 4:
                        GameObject.Instantiate(Monster_preb[11], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<Martial>().SendMessage("setMaxHP", CalMidiumHP());
                        Monsters.transform.GetChild(1).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                    case 5:
                        GameObject.Instantiate(Monster_preb[9], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<LightBandit>().SendMessage("setMaxHP", CalMinimalHP());
                        Monsters.transform.GetChild(1).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                    case 6:
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(1).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                    case 7:
                        GameObject.Instantiate(Monster_preb[11], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<Martial>().SendMessage("setMaxHP", CalMidiumHP());
                        Monsters.transform.GetChild(1).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                    case 8:
                        GameObject.Instantiate(Monster_preb[12], Monster_pos[2], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[1], Quaternion.identity).transform.parent = Monsters.transform;
                        GameObject.Instantiate(Monster_preb[10], Monster_pos[3], Quaternion.identity).transform.parent = Monsters.transform;
                        Monsters.transform.GetChild(0).GetComponent<Bringer>().SendMessage("setMaxHP", CalBigHP());
                        Monsters.transform.GetChild(1).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        Monsters.transform.GetChild(2).GetComponent<HeavyBandit>().SendMessage("setMaxHP", CalSmallHP());
                        break;
                }
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
            else if (Monsters.transform.GetChild(i).gameObject.name == "BossSlime(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<BossSlime>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<BossSlime>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "FlyingEye(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<FlyingEye>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<FlyingEye>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "Skeleton1(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Skeleton1>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Skeleton1>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "Skeleton2(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Skeleton2>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Skeleton2>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "DarkSoldier(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<DarkSoldier>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<DarkSoldier>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "LightBandit(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<LightBandit>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<LightBandit>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "HeavyBandit(Clone)") 
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<HeavyBandit>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<HeavyBandit>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "Martial(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Martial>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Martial>().SendMessage("FirstMove", 1.5f);
            }
            else if (Monsters.transform.GetChild(i).gameObject.name == "Bringer(Clone)")
            {
                if (Monsters.transform.GetChild(i).gameObject.transform.position == Monster_pos[2])
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Bringer>().SendMessage("FirstMove", 0.7f);
                else
                    Monsters.transform.GetChild(i).gameObject.GetComponent<Bringer>().SendMessage("FirstMove", 1.5f);
            }
        }
    }

    public GameObject monster;
    public int Monster_index;
    public int Monster_count;
    void Start_Monster_Attack(int M_count)
    {
        Monster_count = M_count;
        Debug.Log(Monster_count);
        Monster_index = -1;
        do //비활성화 몬스터는 공격x 하기 위해 다음 활성화 된 몬스터 찾기
        {
            Monster_index++;
            monster = GameObject.Find("Monster").transform.GetChild(Monster_index).gameObject;
        } while (monster.activeSelf == false && Monster_index < Monster_count);
        Monster_Attack(Monster_index);
    }
    void Monster_Attack(int index)
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
                else if (monster.name == "BossSlime(Clone)")
                    monster.GetComponent<BossSlime>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "FlyingEye(Clone)")
                    monster.GetComponent<FlyingEye>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "Skeleton1(Clone)")
                    monster.GetComponent<Skeleton1>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "Skeleton2(Clone)") 
                    monster.GetComponent<Skeleton2>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "DarkSoldier(Clone)") 
                    monster.GetComponent<DarkSoldier>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "LightBandit(Clone)") 
                    monster.GetComponent<LightBandit>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "HeavyBandit(Clone)")
                    monster.GetComponent<HeavyBandit>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "Martial(Clone)")
                    monster.GetComponent<Martial>().SendMessage("Monster_Start", Monster_index);
                else if (monster.name == "Bringer(Clone)")
                    monster.GetComponent<Bringer>().SendMessage("Monster_Start", Monster_index);
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
