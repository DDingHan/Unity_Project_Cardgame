using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerTxt;
    public float first_time = 10f;
    public float time;
    public bool timer;

    void Start()
    {
        timer = false;
        time = first_time;
    }

    void Update()
    {
        timerTxt.text = Mathf.Floor(time).ToString();
        if (Mathf.Floor(time) > 0 && timer == true)
        {
            time -= Time.deltaTime;
        }
        else if(Mathf.Floor(time) <= 0 && timer == true)
        {
            timer = false;
            GameObject.Find("Turn").GetComponent<Turn>().SendMessage("ChangeTurn");
        }
    }

    void TimerStart()
    {
        time = first_time+1;
        timer = true;
    }


}
