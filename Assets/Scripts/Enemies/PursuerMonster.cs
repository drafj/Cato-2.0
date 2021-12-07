using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PursuerMonster : Enemies
{
    private GameObject player;
    private Animator anim;
    private bool dying;
    private bool isMoving;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().gameObject;
        isMoving = true;
    }

    void Start()
    {
        StarterP();
    }

    private void OnEnable()
    {
        StarterP();
    }

    public void StarterP()
    {
        LifeAndVelocityAsigner();

        velocity = velocity == 0 ? 5 : velocity;
        life = life == 0 ? 4 : life;

        StartRotation();
    }

    void StartRotation()
    {
        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - -90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            TakeDamage(bullet);
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
        }

    }

    IEnumerator Death()
    {
        if (life <= 0)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            anim.SetBool("idle", false);
            anim.SetTrigger("death");
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
