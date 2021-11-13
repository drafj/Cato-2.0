using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float velReference,
        lifeReference;
    [HideInInspector] public float velocity;
    [HideInInspector] public float life;

    /*public float firstBossLife,
    secondBossLife,
    suicideMonsterVelocity,
    suicideMonsterLife,
    pursuerMonsterVelocity,
    pursuerMonsterLife,
    shooterMonsterVelocity,
    shooterMonsterLife;*/

    /*public void LifeAndVelocityAsigner()
    {
        Seter();

        /*if (GetComponent<Boss>() != null)
        {
            life = firstBossLife;
        }

        if (GetComponent<SecondBoss>() != null)
        {
            life = secondBossLife;
        }

        if (GetComponent<PursuerMonster>() != null)
        {
            life = pursuerMonsterLife;
            velocity = pursuerMonsterVelocity;
        }

        if (GetComponent<SucideMonster>() != null)
        {
            life = suicideMonsterLife;
            velocity = suicideMonsterVelocity;
        }

        if (GetComponent<ShooterMonster>() != null)
        {
            life = shooterMonsterLife;
            velocity = shooterMonsterVelocity;
        }
    }*/

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

    public void GoForward()
    {
        if (!GameManager.instance.pause)
            transform.position -= transform.up * Time.deltaTime * velocity;
    }
}
