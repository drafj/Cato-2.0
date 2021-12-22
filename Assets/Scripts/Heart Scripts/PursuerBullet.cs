using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerBullet : Bullet
{
    [SerializeField] private Rigidbody2D rgbd = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            gameObject.SetActive(false);
        }
    }

    void ChasePlayer()
    {
        if (GameManager.instance.player.transform.position.y < transform.position.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, GetRotToPlayer(), 2.5f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        ChasePlayer();
        rgbd.velocity = transform.up * 500 * Time.deltaTime;
    }
}
