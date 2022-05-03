using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    /*public void SceneChange()
    {
        SceneManager.LoadScene("Main Scene");
    }*/

    public Image Panel;
    float time = 0f;
    float F_time = 1f;


    public void SceneChange()
    {
        //SceneManager.LoadScene("Main Scene");
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
            //Debug.Log("³¡");
        }
        yield return null;
        Debug.Log("³¡");
        SceneManager.LoadScene("Main Scene");
    }

    public void SceneChange_WorldMap()
    {
        SceneManager.LoadScene("World Map");
    }
}
