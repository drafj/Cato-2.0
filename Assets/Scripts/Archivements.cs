using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Archivements : MonoBehaviour
{
    [SerializeField] private List<GameObject> trophies = new List<GameObject>();

    void Start()
    {
        CheckTrophies();
    }

    void CheckTrophies()
    {
        if (PlayerPrefs.GetInt("Heart", 0) == 1)
        {
            trophies[0].GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetInt("Poly", 0) == 1)
        {
            trophies[1].GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetInt("Harpy", 0) == 1)
        {
            trophies[2].GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetInt("Laser", 0) == 100)
        {
            trophies[3].GetComponent<Image>().color = Color.white;
        }
        if (PlayerPrefs.GetInt("Win", 0) == 1)
        {
            trophies[4].GetComponent<Image>().color = Color.white;
        }
    }
}
