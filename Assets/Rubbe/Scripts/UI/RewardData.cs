using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardData : MonoBehaviour
{
    public int clearGold;
    public int[] clearGem;

    private void Awake()
    {
        var obj = FindObjectsOfType<RewardData>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
