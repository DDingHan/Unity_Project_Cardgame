using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour
{
    public float coolTime = 10.0f;
    //float leftTime = 10.0f; 
    float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime * speed; 
            if (coolTime < 0)
            {
                coolTime = 0;
                Destroy(gameObject);
            }
        }
    }
}
