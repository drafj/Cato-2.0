using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemies
{
    private int actualShield;

    void Start()
    {
        LifeAndVelocityAsigner();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Player Bullet")
        {
            life -= 1;
            if (actualShield <= 2)
            {
                transform.GetChild(actualShield).gameObject.SetActive(false);
                actualShield++;
            }

            if (life <= 0)
            {
                AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
                GameManager.instance.counterToBoss++;
                transform.position = new Vector3(1000, 1000);
                gameObject.SetActive(false);
            }
        }

        if (collision.transform.tag == "Border")
        {
            GameManager.instance.counterToBoss++;
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        GoForward();
    }
}
