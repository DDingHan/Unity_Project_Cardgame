using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Move : MonoBehaviour
{
    public int now_index;
    public GameObject Monsters;
    public GameObject[] Monster_child;
    public int Monster_count;
    public int Destroy_count=0;
    // Start is called before the first frame update
    void Start()
    {
        now_index = 0;
        Monster_child = GetChildren(Monsters);
        Monster_count = Monster_child.Length;

        Move(now_index);

    }

    // Update is called once per frame
    void Update()
    {
        //실시간으로 죽은 몬스터 count
        Destroy_count = Count_Destroy_Monster();

        //몬스터가 없을 경우 커서 삭제
        if(Destroy_count == Monster_count)
        {
            gameObject.SetActive(false);
            GameObject.Find("Turn").GetComponent<Turn>().clear = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //아래 화살표를 눌렀을 경우 아래로 이동
            do
            {
                now_index++;
                if (now_index > 2) now_index = 0;
            } while (Monster_child[now_index].gameObject.activeSelf == false); //해당 자리에 몬스터가 없을경우 나올때까지 아래로 이동
        
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //위 화살표를 눌렀을 경우 위로 이동
            do
            {
                now_index--;
                if (now_index < 0) now_index = 2;
            } while (Monster_child[now_index].gameObject.activeSelf == false); //해당 자리에 몬스터가 없을경우 나올때까지 위로 이동
        }

        //필드위에 몬스터가 1마리 이상 살아있고 커서가 있는 자리 몬스터가 죽을경우 몬스터가 나올때까지 아래로 이동
        if (Destroy_count != Monster_count && Monster_child[now_index].gameObject.activeSelf == false)
        {
            do
            {
                now_index++;
                if (now_index > 2) now_index = 0;
            } while (Monster_child[now_index].gameObject.activeSelf == false);
        }
        Move(now_index);
    }

    int Count_Destroy_Monster()
    {
        int count = 0;
        for (int i = 0; i < Monsters.transform.childCount; i++)
        {
            if (Monsters.transform.GetChild(i).gameObject.activeSelf == false) count++;
        }
        return count;
    }

    void Move(int index)
    {
        if (Monster_child[index].name == "Slime(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.68f;
        }
        else if (Monster_child[index].name == "Goblin(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.67f;
        }
        else if (Monster_child[index].name == "Mushroom(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.82f + Vector3.left * 0.02f;
        }
        else if (Monster_child[index].name == "BossSlime(Clone)" || Monster_child[index].name == "Skeleton2(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.82f;
        }
        else if (Monster_child[index].name == "FlyingEye(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.72f;
        }
        else if (Monster_child[index].name == "Skeleton1(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.64f + Vector3.right * 0.07f;
        }
        else if (Monster_child[index].name == "DarkSoldier(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.76f + Vector3.left * 0.05f;
        }
        else if (Monster_child[index].name == "LightBandit(Clone)" || Monster_child[index].name == "HeavyBandit(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.6f;
        }
        else if (Monster_child[index].name == "Martial(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.82f + Vector3.left * 0.1f;
        }
        else if (Monster_child[index].name == "Bringer(Clone)")
        {
            transform.position = Monster_child[index].transform.position + Vector3.up * 0.86f;
        }
    }

    GameObject[] GetChildren(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
}
