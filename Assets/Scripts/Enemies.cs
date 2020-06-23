using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [HideInInspector] public float velocity;
    /*[HideInInspector] */public float life;

    public float firstBossLife;
    public float secondBossLife;
    public float suicideMonsterVelocity;
    public float suicideMonsterLife;
    public float pursuerMonsterVelocity;
    public float pursuerMonsterLife;
    public float shooterMonsterVelocity;
    public float shooterMonsterLife;

    //private void OnDisable()
    //{
    //    if (life <= 0)
    //    Debug.Log("Desactivando");
    //}

    public void LifeAndVelocityAsigner()
    {
        if (GetComponent<Boss>() != null)
        {
            life = GameManager.instance.m_enemiesStats.firstBossLife;
        }

        if (GetComponent<SecondBoss>() != null)
        {
            life = GameManager.instance.m_enemiesStats.secondBossLife;
        }

        if (GetComponent<PursuerMonster>() != null)
        {
            life = GameManager.instance.m_enemiesStats.pursuerMonsterLife;
            velocity = GameManager.instance.m_enemiesStats.pursuerMonsterVelocity;
        }

        if (GetComponent<SucideMonster>() != null)
        {
            life = GameManager.instance.m_enemiesStats.suicideMonsterLife;
            velocity = GameManager.instance.m_enemiesStats.suicideMonsterVelocity;
        }

        if (GetComponent<ShooterMonster>() != null)
        {
            life = GameManager.instance.m_enemiesStats.shooterMonsterLife;
            velocity = GameManager.instance.m_enemiesStats.shooterMonsterVelocity;
        }
    }

    public void GoForward()
    {
        if (!GameManager.instance.pause)
            transform.position -= transform.up * Time.deltaTime * velocity;
    }

    void Update()
    {
        
    }
}
