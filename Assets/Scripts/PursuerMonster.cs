using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PursuerMonster : Enemies
{
    private Animator anim;
    private bool dying;
    private bool isMoving;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isMoving = true;
    }

    void Start()
    {
        StarterP();
    }

    public void StarterP()
    {
        LifeAndVelocityAsigner();

        velocity = velocity == 0 ? 5 : velocity;
        life = life == 0 ? 5 : velocity;

        StartRotation();
    }

    void StartRotation()
    {
        if (GameManager.instance.player != null)
        {
            Vector3 diff = GameManager.instance.player.transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - -90);
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

        if (collision.transform.tag == "Border")
        {
            StartCoroutine("Death");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            life = 0;
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            isMoving = false;
            dying = true;
            GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine("Death");
            if (!GameManager.instance.player.GetComponent<Player>().invencible)
            {
                collision.gameObject.GetComponent<Player>().life = collision.gameObject.GetComponent<Player>().life - 1;
                if (collision.gameObject.GetComponent<Player>().life == 0)
                    Analytics.CustomEvent("Death", new Dictionary<string, object>
                    {
                        {"death", "by pursuer monster"}
                    });
            }
        }

    }

    IEnumerator Death()
    {
        if (life <= 0)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            anim.SetBool("idle", false);
            anim.SetTrigger("death");
            switch (Random.Range(0, 4))
            {
                case 0:
                    GameObject ins = Instantiate(GameManager.instance.ammoPrefab, transform.position, Quaternion.identity);
                    ins.GetComponent<Ammo>().m_fOA = FoodOrAmmo.Food;
                    break;
                case 1:
                    GameObject inst = Instantiate(GameManager.instance.ammoPrefab, transform.position, Quaternion.identity);
                    inst.GetComponent<Ammo>().m_fOA = FoodOrAmmo.Points;
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.55f);
        }
        GameManager.instance.counterToBoss++;
        transform.position = new Vector3(1000, 1000);
        anim.SetBool("idle", true);
        dying = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (life <= 0 && !dying)
        {
            dying = true;
            StartCoroutine("Death");
        }

        if (GameManager.instance.pause)
        {
            anim.GetComponent<Animator>().enabled = false;
        }
        else
        {
            anim.GetComponent<Animator>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving) 
        GoForward();
    }
}
