using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardToMenu : MonoBehaviour
{ 
    public void SceneChange_Menu()
    {
        SceneManager.LoadScene("0.Main Map Scene");
    }
}
