using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BulletController : MonoBehaviour
{
    public bool onCourse;
    public bool flip;
    private Animator anim;
    public BulletFlipDirection mFlipDirection;
    public Gunz gunz;

    private void OnEnable()
    {
        StartBullet();
    }

    public void StartBullet()
    {
        onCourse = true;
        mFlipDirection = (BulletFlipDirection)Random.Range(0, 2);
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemies enemies) || collision.TryGetComponent(out HeartHealer heartHealer))
        {
            Instantiate(GameManager.instance.shotPP, transform.position, Quaternion.identity);
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }

        /*if (transform.tag == "Enemy Bullet")
        {
            if (collision.transform.tag == "Player")
            {
                Player m_player = collision.gameObject.GetComponent<Player>();
                m_player.SetLife(m_player.GetLife() - 5);
                GameManager.instance.StartDealDamage();
                GameManager.instance.player.GetComponent<Player>().UpdateLife();

                if (m_player.GetLife() <= 0)
                {
                    GameManager.instance.gameOver = true;
                    StartCoroutine(GameManager.instance.player.GetComponent<Player>().Die());
                }
                Instantiate(GameManager.instance.shotEP, transform.position, Quaternion.identity);
                transform.position = new Vector3(1000, 1000);
                gameObject.SetActive(false);
            }
        }*/

        if (collision.transform.tag == "Border")
        {
            transform.position = new Vector2(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.pause)
        {
            NormalBullet();
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
}

public enum BulletFlipDirection {Right, Left }
/*-------------------------------------------------------------------------------perseguir enemigo---------------------------------------------------------
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
*/