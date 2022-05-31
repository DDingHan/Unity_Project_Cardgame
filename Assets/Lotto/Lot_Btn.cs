using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lot_Btn : MonoBehaviour
{
    public void onClick()
    {
        Debug.Log("Ω√¿€");
        StartCoroutine(FadeFlow1());
    }

    public Image black;
    float time = 0f;
    float F_time = 1f;

    IEnumerator FadeFlow1()
    {
        black.gameObject.SetActive(true);
        Color alpha = black.color;
        while (alpha.a < 0.5f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            black.color = alpha;
            yield return null;
        }
        yield return null;
        StartCoroutine(FadeFlow2());
        StartCoroutine(appearSkillIcon());
    }

    public Image effect;

    IEnumerator FadeFlow2()
    {
        time = 0f;
        F_time = 1f;
        effect.gameObject.SetActive(true);
        Color alpha = effect.color;
        while (alpha.a < 1.0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            effect.color = alpha;
            yield return null;
        }
        yield return null;
    }
    public Image skillIcon;
    IEnumerator appearSkillIcon()
    {
        time = 0f;
        F_time = 1f;
        skillIcon.gameObject.SetActive(true);
        Color alpha = skillIcon.color;
        while (alpha.a < 1.0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            skillIcon.color = alpha;
            yield return null;
        }
        yield return null;
        Debug.Log("≥°");
    }
}
