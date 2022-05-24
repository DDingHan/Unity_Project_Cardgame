using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HP_Button_Down()
    {
        GameObject.Find("GameData").SendMessage("HPUpgrade");
    }
}
