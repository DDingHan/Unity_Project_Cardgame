using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningBallDemo : MonoBehaviour
{
    public GameObject target;
    public float Speed = 1f;
    public GameObject explosion;
    public GameObject skillIcon;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //GetComponent<Rigidbody>().useGravity = false;
        Invoke("cameraCloseMove", 5);
    }
    private void cameraCloseMove()
    {
        while (transform.position != target.transform.position)
        {
            GetComponent<Rigidbody>().useGravity = false;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        }
        Debug.Log("완료");
        Invoke("appearExplosion", 1);
    }
    private void appearExplosion()
    {
        //explosion.SetActive(true);
        //Invoke("appearSkillIcon", 1);
        StartCoroutine(FadeFlow());
    }

    public Image Panel;
    float time = 0f;
    float F_time = 1f;
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1.0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
            //Debug.Log("끝");
        }
        while (alpha.a > 0f)
        {
            time -= Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
        Debug.Log("끝");
        Invoke("appearSkillIcon", 1);
        //Balls.SetActive(true);
        //Reseult.SetActive(true);
        //SceneManager.LoadScene("Main Scene");
    }

    private void appearSkillIcon()
    {
        skillIcon.SetActive(true);
    }
}
