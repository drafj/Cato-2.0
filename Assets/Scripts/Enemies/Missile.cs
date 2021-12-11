﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Enemies
{
    [SerializeField] private GameObject warning;
    [SerializeField] private Animator anim;
    private Collider2D _collider;

    void Start()
    {
        LifeAndVelocityAsigner();
    }

    private void OnEnable()
    {
        LifeAndVelocityAsigner();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            TakeDamage(bullet);
            if (life <= 0)
            {
                anim.SetTrigger("Death");
                _collider
            }
        }

        if (collision.transform.tag == "Border")
        {
            warning.transform.parent = transform;
            GameManager.instance.counterToBoss++;
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            anim.SetTrigger("Death");
        }
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
        warning.transform.parent = transform;
        GameManager.instance.counterToBoss++;
        transform.position = new Vector3(1000, 1000);
        gameObject.SetActive(false);
    }

    public override void LifeAndVelocityAsigner()
    {
        base.LifeAndVelocityAsigner();
        warning.transform.parent = null;
        warning.transform.position = new Vector2(transform.position.x, 11.5f);
    }

    void Update()
    {
        if (life > 0)
        GoForward();
    }
}