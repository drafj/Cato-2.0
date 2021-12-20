using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private Background background;
    [SerializeField] private Transform pos;
    private int borderTouches;

    private void Start()
    {
        background = FindObjectOfType<Background>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            borderTouches++;
            switch (borderTouches)
            {
                case 1:
                    background.Instancer(pos.position);
                    break;
                case 2:
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        transform.position -= transform.up * velocity * Time.fixedDeltaTime;
    }
}
