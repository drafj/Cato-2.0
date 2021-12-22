using System.Collections;
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
                life -= 5;
                else
                life -= 1;
                break;
            case Gunz.LASER:
                if (shield && !armor)
                life -= 10;
                else if (armor)
                life -= 1;
                else
                life -= 3;
                break;
            case Gunz.PLASMA:
                if (armor && !shield)
                life -= 12;
                else if (shield)
                life -= 3;
                else
                life -= 7;
                break;
            case Gunz.VENOM:
                if (!shield && !armor)
                life -= 1f;
                Poison();
                break;
        }
    }

    public void Poison()
    {
        if (!poisonCD && poisonMeter >= 50)
            StartCoroutine(PoisonCR());
        else if (!poisonCD)
            poisonMeter++;
    }

    public virtual void Die()
    {
        AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
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
