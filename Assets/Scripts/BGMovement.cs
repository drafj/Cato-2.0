using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    [SerializeField] private Background background = null;
    [SerializeField] private Transform pos = null;
    [SerializeField] private BackgroundType myType = BackgroundType.TopTile;
    private int borderTouches = 0;

    private void Start()
    {
        switch (myType)
        {
            case BackgroundType.TopTile:
                background = GameManager.instance.topTileBG;
                break;
            case BackgroundType.MidTile:
                background = GameManager.instance.midTileBG;
                break;
            case BackgroundType.BottomTile:
                background = GameManager.instance.bottomTileBG;
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
        transform.position -= transform.up * background.velocity * Time.fixedDeltaTime;
    }
}

public enum BackgroundType { TopTile, MidTile, BottomTile, Space }