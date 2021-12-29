using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float velocity = 15;
    [SerializeField] private AudioClip audioShot = null;
    public bool onCourse;
    public bool flip;
    public BulletFlipDirection mFlipDirection;
    [SerializeField] private GameObject hitParticles = null;
    public Gunz gunz;
    [HideInInspector] public bool oldEnemyBullet;

    private void OnEnable()
    {
        StartBullet();
    }

    public void StartBullet()
    {
        onCourse = true;
        mFlipDirection = (BulletFlipDirection)Random.Range(0, 2);
    }

    public void ShootSound()
    {
        AudioSource.PlayClipAtPoint(audioShot, Camera.main.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!oldEnemyBullet)
        {
            if (collision.TryGetComponent(out Enemies enemies) || collision.TryGetComponent(out HeartHealer heartHealer))
            {
                Instantiate(hitParticles, transform.position, Quaternion.identity);
                transform.position = new Vector3(1000, 1000);
                gameObject.SetActive(false);
            }

            if (collision.transform.tag == "Border")
            {
                transform.position = new Vector2(1000, 1000);
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (collision.transform.tag == "Border")
            {
                Destroy(gameObject);
            }
            else if (collision.TryGetComponent(out Enemies enemies) || collision.TryGetComponent(out HeartHealer heartHealer))
            {
                Instantiate(hitParticles, transform.position, Quaternion.identity);
                transform.position = new Vector3(1000, 1000);
                Destroy(gameObject);
            }
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
                transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, 0.00001f, 1f), 3f * Time.fixedDeltaTime);
            else if (mFlipDirection == BulletFlipDirection.Right)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 3f * Time.fixedDeltaTime);
        }
    }

    void NormalBullet()
    {
        if (onCourse)
        transform.position += transform.up * velocity * Time.fixedDeltaTime;
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