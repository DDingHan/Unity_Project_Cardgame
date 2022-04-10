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
        Debug.Log("피버타임 시작");
    }

    IEnumerator ShowScreenEffect()
    {
        //BloodScreen.color = new Color(0.9339623f, 0.1013261f, 0.1013261f, UnityEngine.Random.Range(0.8f, 1f));
        GameObject hudFeverText = Instantiate(feverText);
        GameObject hudFeverEffect = Instantiate(feverTime_Screen);

        while (isFeverTime)
        {
            yield return new WaitForSeconds(0.3f);
        }
        //yield return new WaitForSeconds(2f);
        hudFeverText.GetComponent<TextMeshPro>().color = Color.clear;
        Destroy(hudFeverText);
        Destroy(hudFeverEffect);
    }
}
