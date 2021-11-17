﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHeart : Bullet
{
    [SerializeField] private float velocity = 0;
    [SerializeField] private float life = 30;
    [SerializeField] private Rigidbody2D rgbd = null;
    [SerializeField] private Transform target = null;

    void Start()
    {
        target = GameObject.Find("MiniHeartTarget").transform;
        Aim(target.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Heart heart) || collision.TryGetComponent(out Player player))
        {
            Heart boss = target.parent.GetComponent<Heart>();
            boss.counterToMoveAgain++;
            if (boss.counterToMoveAgain >= 4)
                boss.Continue();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rgbd.velocity = transform.up * velocity * Time.deltaTime;
    }
}
