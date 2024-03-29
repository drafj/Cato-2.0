﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float velReference,
        lifeReference;
    [HideInInspector] public float velocity;
    [HideInInspector] public float life;
    [HideInInspector] public int poisonMeter;
    [HideInInspector] public bool poisonCD,
        armor,
        shield;

    public virtual void LifeAndVelocityAsigner()
    {
        life = lifeReference;
        velocity = velReference;
    }

    public void TakeDamage(BulletController bullet)
    {
        switch (bullet.gunz)
        {
            case Gunz.PIERCING:
                if (!armor && !shield)
                life -= 7;
                else if (!armor && shield)
                life -= 14;
                else
                life -= 9;
                break;
            case Gunz.LASER:
                if (shield && !armor)
                life -= 9;
                else if (armor)
                life -= 14;
                else
                life -= 7;
                break;
            case Gunz.PLASMA:
                if (armor && !shield)
                life -= 20;
                else if (shield)
                life -= 10;
                else
                life -= 17;
                break;
            case Gunz.VENOM:
                if (!shield && !armor)
                {
                    life -= 4f;
                    Poison();
                }
                break;
        }
    }

    public void Poison()
    {
        if (!poisonCD && poisonMeter >= 10)
            StartCoroutine(PoisonCR());
        else if (!poisonCD)
            poisonMeter++;
    }

    public virtual void Die()
    {
        GameManager.instance.counterToBoss++;
        transform.position = new Vector3(1000, 1000);
        gameObject.SetActive(false);
    }

    IEnumerator PoisonCR()
    {
        poisonCD = true;
        Debug.Log("poisoned!!");
        for (int i = 0; i < 20; i++)
        {
            life -= 2;
            if (TryGetComponent(out Heart heart))
                heart.SetHealth();
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(4f);
        Debug.Log("unpoisoned!!");
        poisonMeter = 0;
        poisonCD = false;
    }

    public void GoForward()
    {
        transform.position -= transform.up * Time.deltaTime * velocity;
    }
}
