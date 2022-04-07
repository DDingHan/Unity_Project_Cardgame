using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTestController : MonoBehaviour
{
    public GameObject Skill_1;
    public GameObject Skill_2;
    public GameObject Skill_3;
    public GameObject Skill_4;

    Transform Skill_Transform;

    // Start is called before the first frame update
    void Start()
    {
        //CharacterTransform = Character.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(Skill_1, Skill_1.transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(Skill_2, Skill_2.transform.position, Skill_2.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Instantiate(Skill_3, Skill_3.transform.position, Skill_3.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(Skill_4, Skill_4.transform.position, Skill_4.transform.rotation);
        }
    }
}
