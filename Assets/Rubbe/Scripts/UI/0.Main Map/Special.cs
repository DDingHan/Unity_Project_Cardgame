using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Special : MonoBehaviour
{
    public void special()
    {
        Debug.Log("special");
        SceneManager.LoadScene("4.Lot Scene demo");
        //SceneManager.LoadScene("4.Lot Scene");
    }
}
