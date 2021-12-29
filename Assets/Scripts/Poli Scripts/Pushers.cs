using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushers : MonoBehaviour
{
    [SerializeField] private float force = 100000;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.inmobilized = true;
            player.rgbd.velocity = Vector2.zero;
            if (player.transform.position.x > transform.position.x)
            {
                player.rgbd.AddForce(Vector2.right * force * Time.fixedDeltaTime);
            }
            else
            {
                player.rgbd.AddForce(Vector2.right * -force * Time.fixedDeltaTime);
            }
            player.inmobilized = false;
        }
    }
}
