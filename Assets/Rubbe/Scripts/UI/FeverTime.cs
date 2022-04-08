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

    IEnumerator ShowScreenEffect()
    {
        BloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        BloodScreen.color = Color.clear;
    }
}
