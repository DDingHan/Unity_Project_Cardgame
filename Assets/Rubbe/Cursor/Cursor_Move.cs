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
        //�ǽð����� ���� ���� count
        Destroy_count = Count_Destroy_Monster();

        //���Ͱ� ���� ��� Ŀ�� ����
        if(Destroy_count == Monster_count)
        {
            gameObject.SetActive(false);
            GameObject.Find("Turn").GetComponent<Turn>().clear = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //�Ʒ� ȭ��ǥ�� ������ ��� �Ʒ��� �̵�
            do
            {
                now_index++;
                if (now_index > 2) now_index = 0;
            } while (Monster_child[now_index].gameObject.activeSelf == false); //�ش� �ڸ��� ���Ͱ� ������� ���ö����� �Ʒ��� �̵�
        
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //�� ȭ��ǥ�� ������ ��� ���� �̵�
            do
            {
                now_index--;
                if (now_index < 0) now_index = 2;
            } while (Monster_child[now_index].gameObject.activeSelf == false); //�ش� �ڸ��� ���Ͱ� ������� ���ö����� ���� �̵�
        }

        //�ʵ����� ���Ͱ� 1���� �̻� ����ְ� Ŀ���� �ִ� �ڸ� ���Ͱ� ������� ���Ͱ� ���ö����� �Ʒ��� �̵�
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
