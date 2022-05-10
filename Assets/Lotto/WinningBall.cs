using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningBall : MonoBehaviour
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
        Debug.Log("¿Ï·á");
        Invoke("appearExplosion", 1);
    }
    private void appearExplosion()
    {
        explosion.SetActive(true);
        Invoke("appearSkillIcon", 1);
    }

    private void appearSkillIcon()
    {
        skillIcon.SetActive(true);
    }
}
