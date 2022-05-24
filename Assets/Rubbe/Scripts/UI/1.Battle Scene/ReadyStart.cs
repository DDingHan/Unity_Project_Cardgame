using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ReadyStart : MonoBehaviour
{
    public Text Ready_Start_Txt;
    float time = 0f;
    float F_time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Ready_Start_Txt.gameObject.SetActive(false);
    }

    void Cor_Ready_Start()
    {
        StartCoroutine(Ready_Start());
    }

    IEnumerator Ready_Start()
    {
        Ready_Start_Txt.gameObject.SetActive(true);
        StartCoroutine(appearText("~Ready~", 1.0f));
        yield return new WaitForSecondsRealtime(2.0f);
        StartCoroutine(appearText("!start!", 1.0f));
        yield return new WaitForSecondsRealtime(2.0f);
        Ready_Start_Txt.gameObject.SetActive(false);
        GameObject.Find("Turn").GetComponent<Turn>().turnTxt.gameObject.SetActive(true);
        GameObject.Find("Timer").GetComponent<Timer>().timer = true;
    }    

    void Victory()
    {
        Ready_Start_Txt.gameObject.SetActive(true);
        StartCoroutine(appearText("Victory", 2.0f));
        Invoke("SceneChange", 4.0f);
    }
    IEnumerator appearText(string txt,float t)
    {
        Ready_Start_Txt.text = txt;
        time = 0f;
        F_time = t;
        Color alpha = Ready_Start_Txt.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Ready_Start_Txt.color = alpha;
            yield return null;
        }
        while (alpha.a > 0f)
        {
            time -= Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Ready_Start_Txt.color = alpha;
            yield return null;
        }
    }
    public GameObject fadeOut;
    void SceneChange()
    {
        Ready_Start_Txt.gameObject.SetActive(false);
        fadeOut.SetActive(true);
        GameObject.Find("FadeOut").GetComponent<FadeOut>().SendMessage("SceneChange");
    }
}
