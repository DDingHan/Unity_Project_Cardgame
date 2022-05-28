using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMain : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Main;
    public Image Sub1;
    public Image Sub2;

    public Sprite warrior;
    public Sprite mage;
    public Sprite archer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_Left()
    {
        if (Main.sprite == warrior)
        {
            Main.sprite = mage;
            Sub1.sprite = archer;
            Sub2.sprite = warrior;
        }
        else if (Main.sprite == mage)
        {
            Main.sprite = archer;
            Sub1.sprite = warrior;
            Sub2.sprite = mage;
        }
        else if (Main.sprite == archer)
        {
            Main.sprite = warrior;
            Sub1.sprite = mage;
            Sub2.sprite = archer;
        }
    }

    public void Click_Right()
    {
        if (Main.sprite == warrior)
        {
            Main.sprite = archer;
            Sub1.sprite = warrior;
            Sub2.sprite = mage;
        }
        else if (Main.sprite == mage)
        {
            Main.sprite = warrior;
            Sub1.sprite = mage;
            Sub2.sprite = archer;
        }
        else if (Main.sprite == archer)
        {
            Main.sprite = mage;
            Sub1.sprite = archer;
            Sub2.sprite = warrior;
        }
    }
}
