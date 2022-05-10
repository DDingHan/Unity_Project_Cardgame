using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Get_A_Ball : MonoBehaviour
{

    public GameObject Balls;
    public GameObject blackScreen;
    public GameObject Reseult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 0.5f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
            //Debug.Log("끝");
        }
        yield return null;
        Debug.Log("끝");
        Balls.SetActive(true);
        Reseult.SetActive(true);
        //SceneManager.LoadScene("Main Scene");
    }

    public void Get_One_Ball()
    {
        //Balls.SetActive(true);
        Debug.Log("시작");
        StartCoroutine(FadeFlow());
    }
}
