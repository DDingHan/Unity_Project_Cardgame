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

    public GameObject Warrior_Tier_1;
    public GameObject Warrior_Tier_2;
    public GameObject Warrior_Tier_3;

    public GameObject Mage_Tier_1;
    public GameObject Mage_Tier_2;
    public GameObject Mage_Tier_3;

    public GameObject Archer_Tier_1;
    public GameObject Archer_Tier_2;
    public GameObject Archer_Tier_3;

    public Sprite Warrior_Main_1;
    public Sprite Warrior_Main_2;
    public Sprite Warrior_Main_3;

    public Sprite Mage_Main_1;
    public Sprite Mage_Main_2;
    public Sprite Mage_Main_3;

    public Sprite Archer_Main_1;
    public Sprite Archer_Main_2;
    public Sprite Archer_Main_3;

    public Image Now_Tier1;
    public Image Now_Tier2;
    public Image Now_Tier3;

    public string Now_Tier;

    void Start()
    {
        Now_Tier = "1";

        Now_Tier1.sprite = Warrior_Main_1;
        Now_Tier2.sprite = Warrior_Main_2;
        Now_Tier3.sprite = Warrior_Main_3;
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
            Warrior_Tier_1.SetActive(false);
            Warrior_Tier_2.SetActive(false);
            Warrior_Tier_3.SetActive(false);
            Mage_Tier_1.SetActive(true);

            Now_Tier = "1";

            Now_Tier1.sprite = Mage_Main_1;
            Now_Tier2.sprite = Mage_Main_2;
            Now_Tier3.sprite = Mage_Main_3;
        }
        else if (Main.sprite == mage)
        {
            Main.sprite = archer;
            Sub1.sprite = warrior;
            Sub2.sprite = mage;
            Mage_Tier_1.SetActive(false);
            Mage_Tier_2.SetActive(false);
            Mage_Tier_3.SetActive(false);
            Archer_Tier_1.SetActive(true);

            Now_Tier = "1";

            Now_Tier1.sprite = Archer_Main_1;
            Now_Tier2.sprite = Archer_Main_2;
            Now_Tier3.sprite = Archer_Main_3;
        }
        else if (Main.sprite == archer)
        {
            Main.sprite = warrior;
            Sub1.sprite = mage;
            Sub2.sprite = archer;
            Archer_Tier_1.SetActive(false);
            Archer_Tier_2.SetActive(false);
            Archer_Tier_3.SetActive(false);
            Warrior_Tier_1.SetActive(true);

            Now_Tier = "1";

            Now_Tier1.sprite = Warrior_Main_1;
            Now_Tier2.sprite = Warrior_Main_2;
            Now_Tier3.sprite = Warrior_Main_3;
        }
    }

    public void Click_Right()
    {
        if (Main.sprite == warrior)
        {
            Main.sprite = archer;
            Sub1.sprite = warrior;
            Sub2.sprite = mage;
            Warrior_Tier_1.SetActive(false);
            Warrior_Tier_2.SetActive(false);
            Warrior_Tier_3.SetActive(false);
            Archer_Tier_1.SetActive(true);

            Now_Tier = "1";

            Now_Tier1.sprite = Archer_Main_1;
            Now_Tier2.sprite = Archer_Main_2;
            Now_Tier3.sprite = Archer_Main_3;
        }
        else if (Main.sprite == mage)
        {
            Main.sprite = warrior;
            Sub1.sprite = mage;
            Sub2.sprite = archer;
            Mage_Tier_1.SetActive(false);
            Mage_Tier_2.SetActive(false);
            Mage_Tier_3.SetActive(false);
            Warrior_Tier_1.SetActive(true);

            Now_Tier = "1";

            Now_Tier1.sprite = Warrior_Main_1;
            Now_Tier2.sprite = Warrior_Main_2;
            Now_Tier3.sprite = Warrior_Main_3;
        }
        else if (Main.sprite == archer)
        {
            Main.sprite = mage;
            Sub1.sprite = archer;
            Sub2.sprite = warrior;
            Archer_Tier_1.SetActive(false);
            Archer_Tier_2.SetActive(false);
            Archer_Tier_3.SetActive(false);
            Mage_Tier_1.SetActive(true);

            Now_Tier = "1";

            Now_Tier1.sprite = Mage_Main_1;
            Now_Tier2.sprite = Mage_Main_2;
            Now_Tier3.sprite = Mage_Main_3;
        }
    }

    public void Click_Tier1()
    {
        if (Main.sprite == warrior)
        {
            Warrior_Tier_1.SetActive(true);
            Warrior_Tier_2.SetActive(false);
            Warrior_Tier_3.SetActive(false);

            Now_Tier = "1";
        }
        else if (Main.sprite == mage)
        {
            Mage_Tier_1.SetActive(true);
            Mage_Tier_2.SetActive(false);
            Mage_Tier_3.SetActive(false);

            Now_Tier = "1";
        }
        else if (Main.sprite == archer)
        {
            Archer_Tier_1.SetActive(true);
            Archer_Tier_2.SetActive(false);
            Archer_Tier_3.SetActive(false);

            Now_Tier = "1";
        }
    }

    public void Click_Tier2()
    {
        if (Main.sprite == warrior)
        {
            Warrior_Tier_1.SetActive(false);
            Warrior_Tier_2.SetActive(true);
            Warrior_Tier_3.SetActive(false);

            Now_Tier = "2";
        }
        else if (Main.sprite == mage)
        {
            Mage_Tier_1.SetActive(false);
            Mage_Tier_2.SetActive(true);
            Mage_Tier_3.SetActive(false);

            Now_Tier = "2";
        }
        else if (Main.sprite == archer)
        {
            Archer_Tier_1.SetActive(false);
            Archer_Tier_2.SetActive(true);
            Archer_Tier_3.SetActive(false);

            Now_Tier = "2";
        }
    }

    public void Click_Tier3()
    {
        if (Main.sprite == warrior)
        {
            Warrior_Tier_1.SetActive(false);
            Warrior_Tier_2.SetActive(false);
            Warrior_Tier_3.SetActive(true);

            Now_Tier = "3";
        }
        else if (Main.sprite == mage)
        {
            Mage_Tier_1.SetActive(false);
            Mage_Tier_2.SetActive(false);
            Mage_Tier_3.SetActive(true);

            Now_Tier = "3";
        }
        else if (Main.sprite == archer)
        {
            Archer_Tier_1.SetActive(false);
            Archer_Tier_2.SetActive(false);
            Archer_Tier_3.SetActive(true);

            Now_Tier = "3";
        }
    }
}
