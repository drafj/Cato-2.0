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
                Debug.Log("empuja a la derecha");
                player.rgbd.AddForce(Vector2.right * force * Time.fixedDeltaTime);
            }
            else
            {
                Debug.Log("empuja a la izquierda");
                player.rgbd.AddForce(Vector2.right * -force * Time.fixedDeltaTime);
            }
            player.inmobilized = false;
        }
    }
}
