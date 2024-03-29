﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electro : Enemies
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Collider2D _collider = null;

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
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + 15);
                if (PlayerPrefs.GetInt("Harpy", 0) < 3 && GameManager.instance.player.GetComponent<Player>().inmobilized)
                    PlayerPrefs.SetInt("Harpy", PlayerPrefs.GetInt("Harpy", 0) + 1);
                FindObjectOfType<MenuController>().SetPoints();
                _collider.enabled = false;
                anim.SetTrigger("Death");
                AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
                transform.GetChild(0).gameObject.SetActive(false);
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

    public override void LifeAndVelocityAsigner()
    {
        base.LifeAndVelocityAsigner();
        transform.GetChild(0).gameObject.SetActive(true);
        _collider.enabled = true;
        shield = true;
    }

    void Update()
    {
        GoForward();
    }
}
