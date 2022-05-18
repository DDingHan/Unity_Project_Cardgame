using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI stageText;
    public GameObject Map;
    public string stageNum;

    public bool sceneChageStart = false;


    public void SceneChange()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        stageText.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
            //Debug.Log("끝");
        }
        yield return null;
        Debug.Log("끝");
        //Invoke("appearStageText", 1);
        Invoke("afterDelay", 1);
        //SceneManager.LoadScene("Main Scene");
    }

    private void afterDelay()
    {
        stageText.text = Map.name.Substring(4);
        stageText.text = stageText.text + " - " + stageNum;

        GameObject.Find("GameData").GetComponent<Data>().SendMessage("setMapName", Map.name.Substring(4));
        GameObject.Find("GameData").GetComponent<Data>().SendMessage("setStageNum", stageNum);

        StartCoroutine(appearStageText());
    }

    IEnumerator appearStageText()
    {
        Debug.Log("스테이지 생성 시작");
        time = 0f;
        F_time = 1f;
        Color alpha = stageText.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            stageText.color = alpha;
            yield return null;
        }
        while (alpha.a > 0f)
        {
            time -= Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            stageText.color = alpha;
            yield return null;
        }
        Invoke("SceneChange_Stage", 0.5f);
    }

    //public void 
    public void SceneChange_Stage()
    {
        SceneManager.LoadScene("1.Battle scene");
    }


    public void SceneChange_WorldMap()
    {
        SceneManager.LoadScene("World Map");
    }
}
