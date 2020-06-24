using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMonster : Enemies
{
    private void Awake()
    {

    }

    void Start()
    {
        //StartShooter();
    }

    public void StartShooter()
    {
        LifeAndVelocityAsigner();

        velocity = velocity == 0 ? 4 : velocity;
        life = life == 0 ? 5 : life;

        StartCoroutine("Shooter");
    }

    IEnumerator Shooter()
    {
        while (true)
        {
            if (!GameManager.instance.pause && !GameManager.instance.gameOver)
            {
                GameManager.instance.m_pooler.EBSpawner(transform.InverseTransformVector(transform.localPosition + new Vector3(0.00399971f, -1.449f, -0.0999999f)), Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player Bullet")
        {
            if (collision.gameObject.GetComponent<BulletController>().m_State == BulletState.NormalBullet)
                life -= 1;

            if (collision.gameObject.GetComponent<BulletController>().m_State == BulletState.SecondBullet)
                life -= GameManager.instance.secondGunDamage;

            if (collision.gameObject.GetComponent<BulletController>().m_State == BulletState.ThirdBullet)
                life -= GameManager.instance.thirdGunDamage;
        }

        if (life <= 0)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            GameManager.instance.counterToBoss++;
            switch (Random.Range(0, 2))
            {
                case 0:
                    GameObject ins = Instantiate(GameManager.instance.ammoPrefab, transform.position, Quaternion.identity);
                        ins.GetComponent<Ammo>().m_fOA = (FoodOrAmmo) Random.Range(1, 3);
                    break;
                case 1:
                    break;
                default:
                    break;
            }
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
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
            }
        }
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        GoForward();
    }
}
