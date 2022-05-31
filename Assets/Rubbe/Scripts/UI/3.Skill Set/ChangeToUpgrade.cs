using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToUpgrade : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject SKill_Setting_UI;
    public GameObject Skill_Upgrade_UI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToUpgrade()
    {
        SKill_Setting_UI.SetActive(false);
        Skill_Upgrade_UI.SetActive(true);
    }
}
