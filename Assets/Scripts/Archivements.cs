using System.Collections;
using System.Collections.Generic;
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
            trophies[0].SetActive(true);
        if (PlayerPrefs.GetInt("Poly", 0) == 1)
            trophies[1].SetActive(true);
        if (PlayerPrefs.GetInt("Harpy", 0) == 1)
            trophies[2].SetActive(true);
        if (PlayerPrefs.GetInt("Laser", 0) == 1000)
            trophies[3].SetActive(true);
        if (PlayerPrefs.GetInt("Win", 0) == 1)
            trophies[4].SetActive(true);

    }
}
