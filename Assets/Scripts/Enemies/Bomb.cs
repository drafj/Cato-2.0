using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Enemies
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Collider2D _collider = null;
    private Vector3 placeToExplote = new Vector3();
    private bool chasing = false;

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
            GameManager.instance.counterToBoss++;
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BulletController bullet))
        {
            TakeDamage(bullet);
            if (life <= 0)
            {
                anim.SetTrigger("Death");
                _collider.enabled = false;
            }
        }
    }

    void PlayerIsClose()
    {
        Vector3 distance = transform.position - GameManager.instance.player.transform.position;
        if (distance.magnitude <= 9)
        {
            _collider.enabled = false;
            placeToExplote = GameManager.instance.player.transform.position;
            chasing = true;
            StartCoroutine(Explote());
        }
    }

    IEnumerator Explote()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, placeToExplote, (velocity / 3) * Time.fixedDeltaTime);
            Vector3 distance = transform.position - placeToExplote;
            if (distance.magnitude <= 2)
            {
                _collider.enabled = false;
                anim.SetTrigger("Death");
                distance = transform.position - GameManager.instance.player.transform.position;
                if (distance.magnitude <= 3)
                {
                    GameManager.instance.player.GetComponent<Player>().TakeDamage();
                }
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public override void LifeAndVelocityAsigner()
    {
        base.LifeAndVelocityAsigner();
        _collider.enabled = true;
        chasing = false;
    }

    private void Update()
    {
        if (!chasing)
        PlayerIsClose();
    }

    private void FixedUpdate()
    {
        if (!chasing)
            GoForward();
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 6);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }*/
}
