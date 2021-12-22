using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : Bullet
{
    [SerializeField] private float velocity = 0;
    void Start()
    {
        Destroy(gameObject, 0.35f);
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.player.transform.position.y <= transform.position.y)
            transform.position += transform.up * velocity * Time.deltaTime;
        Aim(GameManager.instance.player.transform.position);
    }
}
