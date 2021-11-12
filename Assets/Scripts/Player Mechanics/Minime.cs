﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minime : MonoBehaviour
{
    [SerializeField] private float cadence;
    [SerializeField] private Pooler mPool;
    [HideInInspector] public bool catchable;

    private void Awake()
    {
        mPool = FindObjectOfType<Pooler>();
    }

    void Start()
    {
    }

    private void OnEnable()
    {
        catchable = false;
        StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && catchable)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            catchable = true;
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (mPool.bulletsPool.Length != 0)
            {
                Vector3 pos = transform.GetChild(0).position;
                mPool.Spawner(pos, transform.rotation);
                yield return new WaitForSeconds(cadence);
            }
            else
                yield return null;
        }
    }
}