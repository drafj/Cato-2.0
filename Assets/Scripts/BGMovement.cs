using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    [SerializeField] private float velocity = 0;
    [SerializeField] private Background background = null;
    [SerializeField] private Transform pos = null;
    [SerializeField] private BackgroundType myType = BackgroundType.Tile;
    private int borderTouches = 0;

    private void Start()
    {
        switch (myType)
        {
            case BackgroundType.Tile:
                background = GameManager.instance.tileBG;
                break;
            case BackgroundType.Space:
                background = GameManager.instance.spaceBG;
                break;
            default:
                break;
        }
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

public enum BackgroundType { Tile, Space }