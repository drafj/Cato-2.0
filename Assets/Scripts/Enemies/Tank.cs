using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemies
{
    private int actualShield;
    private Collider2D _collider;

    void Start()
    {
        LifeAndVelocityAsigner();
    }

    private void OnEnable()
    {
        LifeAndVelocityAsigner();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            TakeDamage(bullet);

            if (actualShield <= 2)
            {
                if (actualShield == 0 && life * 100 / lifeReference <= 70)
                {
                    transform.GetChild(actualShield).gameObject.SetActive(false);
                    actualShield++;
                }
                else if (actualShield == 1 && life * 100 / lifeReference <= 40)
                {
                    transform.GetChild(actualShield).gameObject.SetActive(false);
                    actualShield++;
                }
                else if (actualShield == 2 && life * 100 / lifeReference <= 10)
                {
                    transform.GetChild(actualShield).gameObject.SetActive(false);
                    actualShield++;
                    armor = false;
                }
            }

            if (life <= 0)
            {
                GetComponent<Animator>().SetTrigger("Death");
                _collider.enabled = false;
                for (int i = 0; i < 3; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
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

    public override void LifeAndVelocityAsigner()
    {
        base.LifeAndVelocityAsigner();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;
        armor = true;
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (life > 0)
        GoForward();
    }
}
