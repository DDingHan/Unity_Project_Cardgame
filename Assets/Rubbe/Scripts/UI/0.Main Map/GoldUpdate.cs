using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUpdate : MonoBehaviour
{
    // Start is called before the first frame update

    public Text GoldText;
    public Text LevelText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = GameObject.Find("GameData").GetComponent<Data>().Gold.ToString();
        LevelText.text = GameObject.Find("GameData").GetComponent<Data>().HP_Level.ToString();
    }
}
