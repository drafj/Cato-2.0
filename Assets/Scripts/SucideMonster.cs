using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SucideMonster : Enemies
{
    private void Awake()
    {

    }

    void Start()
    {
        StartS();
    }

    public void StartS()
    {
        LifeAndVelocityAsigner();

        velocity = velocity == 0 ? 5 : velocity;
        life = life == 0 ? 2 : velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Player Bullet")
        {
            if (collision.gameObject.GetComponent<BulletController>().m_State == BulletState.NormalBullet)
                life -= 1;

            if (collision.gameObject.GetComponent<BulletController>().m_State == BulletState.SecondBullet)
                life -= GameManager.instance.secondGunDamage;

            if (collision.gameObject.GetComponent<BulletController>().m_State == BulletState.ThirdBullet)
                life -= GameManager.instance.thirdGunDamage;

            if (life <= 0)
            {
                AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
                GameManager.instance.counterToBoss++;
                switch (Random.Range(0, 4))
                {
                    case 0:
                        GameObject ins = Instantiate(GameManager.instance.ammoPrefab, transform.position, Quaternion.identity);
                        ins.GetComponent<Ammo>().m_fOA = (FoodOrAmmo) Random.Range(0, 2);
                        break;
                    default:
                        break;
                }
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
            if (!GameManager.instance.player.GetComponent<Player>().invencible)
            {
                collision.gameObject.GetComponent<Player>().life = collision.gameObject.GetComponent<Player>().life - 1;
                if (collision.gameObject.GetComponent<Player>().life == 0)
                    Analytics.CustomEvent("Death", new Dictionary<string, object>
                    {
                        {"death", "by monster 127"}
                    });
            }
        }
    }

    void Update()
    {
        GoForward();
    }
}
