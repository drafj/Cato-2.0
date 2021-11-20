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

    public void LifeAndVelocityAsigner()
    {
        life = lifeReference;
        velocity = velReference;
        /*firstBossLife = firstBossLife == 0 ? 60 : firstBossLife;
        secondBossLife = secondBossLife == 0 ? 90 : secondBossLife;
        suicideMonsterVelocity = suicideMonsterVelocity == 0 ? 4.5f : suicideMonsterVelocity;
        suicideMonsterLife = suicideMonsterLife == 0 ? 1.5f : suicideMonsterLife;
        pursuerMonsterVelocity = pursuerMonsterVelocity == 0 ? 4 : pursuerMonsterVelocity;
        pursuerMonsterLife = pursuerMonsterLife == 0 ? 3 : pursuerMonsterLife;
        shooterMonsterVelocity = shooterMonsterVelocity == 0 ? 2 : shooterMonsterVelocity;
        shooterMonsterLife = shooterMonsterLife == 0 ? 5 : shooterMonsterLife;*/
    }

    public void Poison()
    {
        if (!poisonCD && poisonMeter >= 50)
            StartCoroutine(PoisonCR());
        else if (!poisonCD)
            poisonMeter++;
    }

    IEnumerator PoisonCR()
    {
        poisonCD = true;
        for (int i = 0; i < 20; i++)
        {
            life -= 2;
            if (TryGetComponent(out Heart heart))
                heart.SetHealth();
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(4f);
        poisonMeter = 0;
        poisonCD = false;
    }

    public void GoForward()
    {
        if (!GameManager.instance.pause)
            transform.position -= transform.up * Time.deltaTime * velocity;
    }
}
