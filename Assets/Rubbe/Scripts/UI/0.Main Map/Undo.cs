using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ReadyUI;
    public GameObject ReadyObjects;
    public GameObject Map;
    public GameObject MapNum;

    //UI button
    public GameObject Shop;
    public GameObject Worldmap;
    public GameObject Special;
    public GameObject undo;

    public void Undo_Scene()
    {
        Map.SetActive(true);
        ReadyUI.SetActive(false);
        ReadyObjects.SetActive(false);
        //UI button on/off
        Shop.SetActive(true);
        Worldmap.SetActive(true);
        Special.SetActive(true);
        undo.SetActive(false);
    }
}
