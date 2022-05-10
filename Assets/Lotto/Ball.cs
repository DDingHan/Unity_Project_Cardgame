using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("BallMove");
    }
    IEnumerator BallMove()
    {
        while (true)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(
                Random.Range(-1f, 1f), Random.Range(-0.8f, 1f), Random.Range(-1f, 1f)) * 10, 
                ForceMode.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
