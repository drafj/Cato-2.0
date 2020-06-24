using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public GameObject old;
    public GameObject newText;
    public MenuController setPointer;
    public MyUpgrade m_upgrade;

    void Start()
    {
        //PlayerPrefs.SetInt("Points", 1500);
        //PlayerPrefs.SetInt("flashInv", 1);
        //PlayerPrefs.SetInt("shieldTime", 5);

        if (m_upgrade == MyUpgrade.Flash && PlayerPrefs.GetInt("flashInv", 1) == 2 || m_upgrade == MyUpgrade.Shield && PlayerPrefs.GetInt("shieldTime", 5) == 10)
        {
            old.gameObject.SetActive(false);
            newText.gameObject.SetActive(true);
        }
    }

    public void FlashUpgrade()
    {
        if (PlayerPrefs.GetInt("Points") >= 500)
        {
            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") - 500);
            PlayerPrefs.SetInt("flashInv", 2);
            setPointer.GetComponent<MenuController>().SetPoints();
            old.gameObject.SetActive(false);
            newText.gameObject.SetActive(true);
        }
    }

    public void ShieldUpgrade()
    {
        if (PlayerPrefs.GetInt("Points") >= 1000)
        {
            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") - 1000);
            PlayerPrefs.SetInt("shieldTime", 10);
            setPointer.GetComponent<MenuController>().SetPoints();
            old.gameObject.SetActive(false);
            newText.gameObject.SetActive(true);
        }
    }
}

public enum MyUpgrade {Flash, Shield}