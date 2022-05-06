using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    public Text countTxt;
    public int total_count;
    public int now_count;

    void Start()
    {
        total_count = 5;
        now_count = 0;
    }

    void Update()
    {
        countTxt.text = now_count.ToString() + " / " + total_count.ToString();
    }
}
