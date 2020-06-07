﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public FoodOrAmmo m_fOA;
    AmmoSelection m_ammoSelection;

    private void Awake()
    {

    }

    void Start()
    {
        if (m_fOA == FoodOrAmmo.Ammo)
            m_ammoSelection = (AmmoSelection)Random.Range(0, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && m_fOA == FoodOrAmmo.Ammo)
        {
            if (m_ammoSelection == AmmoSelection.SecondAmmo)
                collision.gameObject.GetComponent<Player>().secondAmmo += 50;
            else
                collision.gameObject.GetComponent<Player>().thirdAmmo += 75;
            Destroy(gameObject);//necesito un pull de objetos :,c y me da pereza hacerlo
        }

        if (collision.gameObject.GetComponent<Player>() != null && m_fOA == FoodOrAmmo.Food)
        {
            collision.gameObject.GetComponent<Player>().foodMeter += 0.34f;
            Destroy(gameObject);//necesito un pull de objetos :,c y me da pereza hacerlo
        }
    }

    public void GoForward()
    {
        if (!GameManager.instance.pause)
            transform.position -= transform.up * Time.fixedDeltaTime * GameManager.instance.ammoFallVelocity;
    }

    void Update()
    {
        GoForward();
        if (transform.position.y <= -16)
            Destroy(gameObject);
    }
}

public enum AmmoSelection {SecondAmmo, ThirdAmmo}
public enum FoodOrAmmo {Food, Ammo}