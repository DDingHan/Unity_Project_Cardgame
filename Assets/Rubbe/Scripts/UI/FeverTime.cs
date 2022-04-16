using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeverTime : MonoBehaviour
{

    //public Image BloodScreen;
    public GameObject feverText;
    public GameObject feverTime_Screen;
    public bool isFeverTime;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        isFeverTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(ShowScreenEffect());
            //GameObject hudFeverText = Instantiate(feverText);
            //Screen_Effect();
            int z = 1;
            Text Temp = GameObject.Find("Deck").GetComponent<RandomSelect>().CardUIList[z].cardBackName;
            Color alpha = Temp.color;
            alpha.a = 1;
            Temp.color = alpha;
        }
        if (isFeverTime)
        {
            if (slider.value > 0)
            {
                slider.value -= 0.0004f;
            }
            else
            {
                isFeverTime = false;
            }
        }
    }

    void Screen_Effect()
    {
        isFeverTime = true;
        StartCoroutine(ShowScreenEffect());
        StartCoroutine(ShowFeverText());
        GetRandomNum_6();
        Debug.Log("피버타임 시작");
    }

    IEnumerator ShowScreenEffect()
    {
        //BloodScreen.color = new Color(0.9339623f, 0.1013261f, 0.1013261f, UnityEngine.Random.Range(0.8f, 1f));
        //GameObject hudFeverText = Instantiate(feverText);
        GameObject hudFeverEffect = Instantiate(feverTime_Screen);

        while (isFeverTime)
        {
            yield return new WaitForSeconds(0.3f);
        }
        //yield return new WaitForSeconds(2f);
        //hudFeverText.GetComponent<TextMeshPro>().color = Color.clear;
        //Destroy(hudFeverText);
        Destroy(hudFeverEffect);
        DelRandomNum_6();
    }

    IEnumerator ShowFeverText()
    {
        GameObject hudFeverText = Instantiate(feverText);
        yield return new WaitForSeconds(2f);
        Destroy(hudFeverText);
    }

    int[] numSave = new int[6];
    void GetRandomNum_6()
    {
        numSave[0] = Random.Range(0, 4);
        numSave[1] = Random.Range(4, 8);
        numSave[2] = Random.Range(8, 12);
        numSave[3] = Random.Range(12, 16);
        numSave[4] = Random.Range(16, 20);
        numSave[5] = Random.Range(20, 24);

        for(int i = 0; i < 6; i++)
        {
            Text Temp = GameObject.Find("Deck").GetComponent<RandomSelect>().CardUIList[numSave[i]].cardBackName;
            Color alpha = Temp.color;
            alpha.a = 1;
            Temp.color = alpha;
        }
    }

    void DelRandomNum_6()
    {
        for (int i = 0; i < 6; i++)
        {
            Text Temp = GameObject.Find("Deck").GetComponent<RandomSelect>().CardUIList[numSave[i]].cardBackName;
            Color alpha = Temp.color;
            alpha.a = 0;
            Temp.color = alpha;
        }
    }
}
