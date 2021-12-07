using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SucideMonster : Enemies
{
    void Start()
    {
        StartS();
    }

    private void OnEnable()
    {
        StartS();
    }

    public void StartS()
    {
        LifeAndVelocityAsigner();

        velocity = velocity == 0 ? 5 : velocity;
        life = life == 0 ? 2 : life;
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
