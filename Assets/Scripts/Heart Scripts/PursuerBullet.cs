using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerBullet : Bullet
{
    [SerializeField] private Rigidbody2D rgbd;

    void ChasePlayer()
    {
        if (GameManager.instance.player.transform.position.y < transform.position.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, GetRotToPlayer(), 2.5f * Time.deltaTime);
            rgbd.velocity = transform.up * 500 * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        ChasePlayer();
    }
}
