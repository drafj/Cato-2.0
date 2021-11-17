using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPooler : MonoBehaviour
{
    [SerializeField] private GameObject pursuerBulletPref;
    [SerializeField] private int pursuerBulletLenth;
    [SerializeField] private GameObject[] pursuerBullets;
    [SerializeField] private GameObject shapeBulletPref;
    [SerializeField] private int shapeBulletLenth;
    [SerializeField] private GameObject[] shapeBullets;
    void Awake()
    {
        pursuerBullets = new GameObject[pursuerBulletLenth + 1];
        shapeBullets = new GameObject[shapeBulletLenth + 1];

        for (int i = 0; i < pursuerBullets.Length; i++)
        {
            GameObject temporal = Instantiate(pursuerBulletPref, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            pursuerBullets[i] = temporal;
        }

        for (int i = 0; i < shapeBullets.Length; i++)
        {
            GameObject temporal = Instantiate(shapeBulletPref, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            shapeBullets[i] = temporal;
        }
    }

    public void SpawnPursuerB(Vector3 pos, Quaternion rot)
    {
        if (pursuerBullets[0].gameObject.activeSelf)
            pursuerBullets[0].SetActive(false);

        pursuerBullets[0].transform.position = pos;
        pursuerBullets[0].transform.rotation = rot;
        pursuerBullets[0].SetActive(true);
        pursuerBullets[pursuerBulletLenth] = pursuerBullets[0];

        for (int i = 0; i < pursuerBulletLenth; i++)
        {
            pursuerBullets[i] = pursuerBullets[i + 1];
        }
    }

    public void SpawnShapeB(Vector3 pos, Quaternion rot)
    {
        if (shapeBullets[0].gameObject.activeSelf)
            shapeBullets[0].SetActive(false);

        shapeBullets[0].transform.position = pos;
        shapeBullets[0].transform.rotation = rot;
        shapeBullets[0].SetActive(true);
        shapeBullets[shapeBulletLenth] = shapeBullets[0];

        for (int i = 0; i < shapeBulletLenth; i++)
        {
            shapeBullets[i] = shapeBullets[i + 1];
        }
    }
}
