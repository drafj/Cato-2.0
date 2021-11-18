using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBullet : Bullet
{
    [SerializeField] private Rigidbody2D rgbd;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        Aim(GameManager.instance.player.transform.position);
    }

    private void FixedUpdate()
    {
        rgbd.velocity = transform.up * 500 * Time.deltaTime;
    }
}
