using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Wind_Smash : MonoBehaviour
{
    public float speed = 0.005f;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position+= new Vector3(speed, 0, 0);
    }
}
