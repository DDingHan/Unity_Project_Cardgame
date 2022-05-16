using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public GameObject Monsters;

    // Start is called before the first frame update
    void Start()
    {        
        Invoke("FirstMove", 0.5f);
    }

    void FirstMove()
    {
        for (int i = 0; i < Monsters.transform.childCount; i++)
        {
            Monsters.transform.GetChild(i).gameObject.GetComponent<Slime>().SendMessage("FirstMove");
        }
    }
    
}
