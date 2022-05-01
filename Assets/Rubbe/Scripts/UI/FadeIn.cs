using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update

    public Image Panel;
    float time = 0f;
    float F_time = 2f;


    void Start()
    {
        StartCoroutine(FadeInImage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeInImage()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
            //Debug.Log("³¡");
        }
        yield return null;
        Debug.Log("³¡");
        Panel.gameObject.SetActive(false);
    }
}
