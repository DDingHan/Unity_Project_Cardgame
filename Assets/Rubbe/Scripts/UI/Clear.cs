using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Clear : MonoBehaviour
{
    public Text Clear_Txt;
    float time = 0f;
    float F_time = 1f;

    void Start()
    {
        StartCoroutine(appearText("CLEAR", 2.0f));
    }

    // Update is called once per frame
    IEnumerator appearText(string txt, float t)
    {
        while (true)
        {
            Clear_Txt.text = txt;
            time = 0f;
            F_time = t;
            Color alpha = Clear_Txt.color;
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time);
                Clear_Txt.color = alpha;
                yield return null;
            }
            while (alpha.a > 0f)
            {
                time -= Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time);
                Clear_Txt.color = alpha;
                yield return null;
            }
        }        
    }
}
