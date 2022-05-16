using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FadeOut : MonoBehaviour
{
    // Start is called before the first frame update

    public Image Panel;
    float time = 0f;
    float F_time = 2f;

    void Start()
    {
        Color alpha = Panel.color;
        alpha.a = 0f;
        Panel.color = alpha;
        Panel.gameObject.SetActive(false);
    }

    public void SceneChange()
    {
        StartCoroutine(FadeOutImage());
    }
    IEnumerator FadeOutImage()
    {
        //Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
        Invoke("SceneChange_Reward", 0.5f);
    }

    public void SceneChange_Reward()
    {
        SceneManager.LoadScene("2.Reward Scene");
    }
}
