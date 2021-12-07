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
        if (collision.TryGetComponent(out BulletController bullet))
        {
            switch (bullet.gunz)
            {
                case Gunz.PIERCING:
                    life -= 5;
                    break;
                case Gunz.LASER:
                    life -= 3;
                    break;
                case Gunz.PLASMA:
                    life -= 7;
                    break;
                case Gunz.VENOM:
                    life -= 1;
                    break;
            }

            if (actualShield <= 2)
            {
                if (actualShield == 0 && life * 100 / lifeReference <= 70)
                {
                    Debug.Log("vida: " + life);
                    transform.GetChild(actualShield).gameObject.SetActive(false);
                    actualShield++;
                }
                else if (actualShield == 1 && life * 100 / lifeReference <= 40)
                {
                    Debug.Log("vida: " + life);
                    transform.GetChild(actualShield).gameObject.SetActive(false);
                    actualShield++;
                }
                else if (actualShield == 2 && life * 100 / lifeReference <= 10)
                {
                    Debug.Log("vida: " + life);
                    transform.GetChild(actualShield).gameObject.SetActive(false);
                    actualShield++;
                }
            }

            if (life <= 0)
            {
                GetComponent<Animator>().SetTrigger("Death");
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

    public void Die()
    {
        AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
        GameManager.instance.counterToBoss++;
        transform.position = new Vector3(1000, 1000);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (life > 0)
        GoForward();
    }
}
