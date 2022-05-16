using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Players;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FirstMove", 0.5f);
        Invoke("Ready_Start", 3.3f);
    }

    void FirstMove()
    {
        for (int i = 0; i < Players.transform.childCount; i++)
        {
            Players.transform.GetChild(i).gameObject.GetComponent<Character>().SendMessage("FirstMove"); ;

        }
    }

    void Ready_Start()
    {
        GameObject.Find("ReadyStart").GetComponent<ReadyStart>().SendMessage("Cor_Ready_Start");
    }

}
