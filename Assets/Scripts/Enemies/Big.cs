﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big : Enemies
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Collider2D _collider = null;
    [SerializeField] private GameObject gap = null;
    [SerializeField] private float cadence = 0;
    private BulletsPooler bPool = null;
    private Pooler ePool = null;

    private void Awake()
    {
        bPool = FindObjectOfType<BulletsPooler>();
        ePool = FindObjectOfType<Pooler>();
    }

    void Start()
    {
        LifeAndVelocityAsigner();
    }

    private void OnEnable()
    {
        LifeAndVelocityAsigner();
        StartCoroutine(StartShooting());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            TakeDamage(bullet);
            if (life <= 0)
            {
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + 20);
                if (PlayerPrefs.GetInt("Harpy", 0) < 3 && GameManager.instance.player.GetComponent<Player>().inmobilized)
                    PlayerPrefs.SetInt("Harpy", PlayerPrefs.GetInt("Harpy", 0) + 1);
                FindObjectOfType<MenuController>().SetPoints();
                anim.SetTrigger("Death");
                AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
                _collider.enabled = false;
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
            anim.SetTrigger("Death");
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            _collider.enabled = false;
        }
    }

    IEnumerator StartShooting()
    {
        while (life > 0)
        {
            bPool.SpawnPursuerB(gap.transform.position, Quaternion.Euler(0, 0, 180), 2);
            yield return new WaitForSeconds(cadence);
            if (life <= 0)
                break;
            ePool.BombSpawner(gap.transform.position, Quaternion.identity, 7);
            yield return new WaitForSeconds(cadence);
        }
    }

    public override void LifeAndVelocityAsigner()
    {
        base.LifeAndVelocityAsigner();
        _collider.enabled = true;
    }

    private void FixedUpdate()
    {
        GoForward();
    }
}
