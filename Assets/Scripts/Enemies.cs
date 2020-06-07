using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [HideInInspector] public float velocity;
    [HideInInspector] public float life;

    public float BossLife;
    public float suicideMonsterVelocity;
    public float suicideMonsterLife;
    public float pursuerMonsterVelocity;
    public float pursuerMonsterLife;
    public float shooterMonsterVelocity;
    public float shooterMonsterLife;

    void Start()
    {
        
    }



    public void LifeAndVelocityAsigner()
    {
        if (GetComponent<Boss>() != null)
        {
            life = GameManager.instance.m_enemiesStats.BossLife;
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
            transform.position -= transform.up * Time.fixedDeltaTime * velocity;
    }

    void Update()
    {
        
    }
}
