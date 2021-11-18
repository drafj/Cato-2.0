using System.Collections;
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
        transform.GetChild(0).rotation = GameManager.instance.transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Heart boss = target.parent.GetComponent<Heart>();
        if (collision.TryGetComponent(out Heart heart))
        {
            boss.counterToMoveAgain++;
            boss.counterToRay++;
            if (boss.counterToRay >= 2)
            {
                boss.counterToRay = 0;
                boss.IncapacitatingBeam();
            }
            if (boss.counterToMoveAgain >= 4)
            {
                boss.counterToRay = 0;
                boss.Continue();
            }
            Destroy(gameObject);
        }
        else if (collision.TryGetComponent(out Player player))
        {
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
