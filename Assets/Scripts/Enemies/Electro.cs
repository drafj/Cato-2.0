﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electro : Enemies
{
    void Start()
    {
        LifeAndVelocityAsigner();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Player Bullet")
        {
            life -= 1;

            if (life <= 0)
            {
                AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
                GameManager.instance.counterToBoss++;
                transform.position = new Vector3(1000, 1000);
                gameObject.SetActive(false);
            }
        }

        if (collision.transform.tag == "Border")
        {
            GameManager.instance.counterToBoss++;
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        GoForward();
    }
}
