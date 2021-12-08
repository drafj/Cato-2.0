using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electro : Enemies
{
    [SerializeField] private Animator anim;
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

            if (life <= 0)
            {
                _collider.enabled = false;
                anim.SetTrigger("Death");
                transform.GetChild(0).gameObject.SetActive(false);
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
        if (collision.gameObject.TryGetComponent(out Player player))
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
        transform.GetChild(0).gameObject.SetActive(true);
        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;
        shield = true;
    }

    void Update()
    {
        if (life > 0)
        GoForward();
    }
}
