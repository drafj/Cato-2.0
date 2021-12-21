using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Enemies
{
    [SerializeField] private GameObject warning = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private Collider2D _collider = null;

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
                anim.SetTrigger("Death");
                _collider.enabled = false;
            }
        }

        if (collision.transform.tag == "Border")
        {
            warning.transform.parent = transform;
            GameManager.instance.counterToBoss++;
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            life = 0;
            anim.SetTrigger("Death");
            _collider.enabled = false;
        }
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
        warning.transform.parent = transform;
        GameManager.instance.counterToBoss++;
        transform.position = new Vector3(1000, 1000);
        gameObject.SetActive(false);
    }

    public override void LifeAndVelocityAsigner()
    {
        base.LifeAndVelocityAsigner();
        _collider.enabled = true;
        warning.transform.parent = null;
        warning.transform.position = new Vector2(transform.position.x, 11.5f);
    }

    private void FixedUpdate()
    {
        GoForward();
    }
}
