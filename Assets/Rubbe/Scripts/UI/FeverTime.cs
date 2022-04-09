using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverTime : MonoBehaviour
{

    public Image BloodScreen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShowScreenEffect());
        }
    }

    void Screen_Effect()
    {
        StartCoroutine(ShowScreenEffect());
    }

    IEnumerator ShowScreenEffect()
    {
        BloodScreen.color = new Color(0, 0.003508208f, 0.8207547f, UnityEngine.Random.Range(0.8f, 1f));
        yield return new WaitForSeconds(3f);
        BloodScreen.color = Color.clear;
    }
}
