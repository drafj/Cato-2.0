using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BulletController : MonoBehaviour
{
    public float enemyDistance = 50;
    public bool onCourse;
    public bool flip;
    private Animator anim;
    public Quaternion limitRotations;
    public BulletFlipDirection mFlipDirection;
    public BulletState m_State;

    void Start()
    {

    }

    public void StartBullet()
    {
        onCourse = true;
        mFlipDirection = (BulletFlipDirection)Random.Range(0, 2);
        anim = GetComponent<Animator>();

        if (transform.tag == "Player Bullet")
        {
            if (GameManager.instance.player.GetComponent<Player>().m_Gunz == Gunz.BasicGun)
                m_State = BulletState.NormalBullet;
            else if (GameManager.instance.player.GetComponent<Player>().m_Gunz == Gunz.SecondGun)
                m_State = BulletState.SecondBullet;
            else
                m_State = BulletState.ThirdBullet;
        }

        if (transform.tag == "Enemy Bullet")
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = GameManager.instance.enemyBulletSprite;
            GetComponent<Renderer>().sortingOrder = 7;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (m_State == BulletState.ThirdBullet)
        {
            limitRotations = Quaternion.Euler(0, 0, Random.Range(-20, 20));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.tag == "Player Bullet")
        {
            if (collision.transform.tag == "Enemy")
            {
                Instantiate(GameManager.instance.shotPP, transform.position, Quaternion.identity);
                transform.position = new Vector3(1000, 1000);
                gameObject.SetActive(false);
            }
        }

        if (transform.tag == "Enemy Bullet")
        {
            if (collision.transform.tag == "Player")
            {
                if (!GameManager.instance.player.GetComponent<Player>().invencible)
                {
                    collision.gameObject.GetComponent<Player>().life--;
                    if (collision.gameObject.GetComponent<Player>().life == 0)
                    {
                        Analytics.CustomEvent("Death", new Dictionary<string, object>
                        {
                            {"death", "by shooter monster"}
                        });
                    }
                    GameManager.instance.StartDealDamage();
                }
                Instantiate(GameManager.instance.shotEP, transform.position, Quaternion.identity);
                transform.position = new Vector3(1000, 1000);
                gameObject.SetActive(false);
            }
        }

        if (collision.transform.tag == "Border")
        {
            transform.position = new Vector2(1000, 1000);
            gameObject.SetActive(false);
        }

        else if (collision.transform.tag == "Border" && transform.tag == "Enemy Bullet")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

        /*if (GameManager.instance.pause && transform.tag == "Player Bullet")
            anim.GetComponent<Animator>().enabled = false;
        else if (transform.tag == "Player Bullet")
            anim.GetComponent<Animator>().enabled = true;*/
    }

    void FixedUpdate()
    {
        //Debug.Log(transform.rotation);

        if (!GameManager.instance.pause)
        {
            switch (m_State)
            {
                case BulletState.NormalBullet:
                    NormalBullet();
                    break;
                case BulletState.SecondBullet:
                    SecondBullet();
                    break;
                case BulletState.ThirdBullet:
                    ThirdBullet();
                    break;
                default:
                    break;
            }
        }


        if (flip && !onCourse && !GameManager.instance.pause)
        {
            if (mFlipDirection == BulletFlipDirection.Left)
                transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, 0.00001f, 1f), 1.5f * Time.deltaTime);
            else if (mFlipDirection == BulletFlipDirection.Right)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 1.5f * Time.deltaTime);
        }
    }

    void NormalBullet()
    {
        if (onCourse)
        transform.position += transform.up * 15 * Time.fixedDeltaTime;
    }

    void SecondBullet()
    {
        transform.position += transform.up * 15 * Time.fixedDeltaTime;

        GameObject enemyCloser = null;
        enemyDistance = 50;

        foreach (var enemy in FindObjectsOfType<Enemies>())
        {
            Vector3 temporalUbication = enemy.transform.position;
            float temporalDistance = (temporalUbication - transform.position).magnitude;

            if (temporalDistance < enemyDistance && enemy.GetComponent<GameManager>() == null)
            {
                enemyDistance = temporalDistance;
                enemyCloser = enemy.gameObject;
            }
        }

        if (enemyCloser != null)
        {
            Vector3 diff = enemyCloser.transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        
    }

    void ThirdBullet()
    {
        transform.position += transform.up * 15 * Time.fixedDeltaTime;

        transform.rotation = limitRotations;
    }
}

public enum BulletFlipDirection {Right, Left }

public enum BulletState {NormalBullet, SecondBullet, ThirdBullet }