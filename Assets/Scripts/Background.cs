﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    public bool instanceBackgrounds;
    public float velocity;
    private int actualObj = 0;
    private int previousObj;

    public void Instancer(Vector3 pos)
    {
        if (instanceBackgrounds)
        {
            actualObj = actualObj == previousObj ? Random.Range(0, prefabs.Count) : actualObj;
            GameObject temp = Instantiate(prefabs[actualObj], pos, Quaternion.identity);
            previousObj = actualObj;
        }
    }
}
