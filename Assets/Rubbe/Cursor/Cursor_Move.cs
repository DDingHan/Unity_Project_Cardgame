using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Move : MonoBehaviour
{
    int now_index;
    public GameObject Monster;
    public GameObject[] Monster_child;
    int Monster_count;
    int Destroy_count=0;
    // Start is called before the first frame update
    void Start()
    {
        now_index = 0;
        Monster_child = GetChildren(Monster);
        Monster_count = Monster_child.Length;

        Move(now_index);

    }

    // Update is called once per frame
    void Update()
    {
        //몬스터가 없을 경우 커서 삭제
        if(Destroy_count == Monster_count)
        {
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //아래 화살표를 눌렀을 경우 아래로 이동
            do
            {
                now_index++;
                if (now_index > 2) now_index = 0;
            } while (Monster_child[now_index] == null); //해당 자리에 몬스터가 없을경우 나올때까지 아래로 이동
        
            Move(now_index);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //위 화살표를 눌렀을 경우 위로 이동
            Debug.Log(now_index);
            do
            {
                now_index--;
                if (now_index < 0) now_index = 2;
            } while (Monster_child[now_index] == null); //해당 자리에 몬스터가 없을경우 나올때까지 위로 이동
            Move(now_index);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Destroy(Monster_child[0]);
            Destroy_count++;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            Destroy(Monster_child[1]);
            Destroy_count++;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Destroy(Monster_child[2]);
            Destroy_count++;
        }

        //필드위에 몬스터가 1마리 이상 살아있고 커서가 있는 자리 몬스터가 죽을경우 몬스터가 나올때까지 아래로 이동
        if (Destroy_count != Monster_count && Monster_child[now_index] == null)
        {
            do
            {
                now_index++;
                if (now_index > 2) now_index = 0;
            } while (Monster_child[now_index] == null);
            Move(now_index);
        }
    }

    void Move(int index)
    {
        transform.position = Monster_child[index].transform.position + Vector3.up * 0.55f;
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
