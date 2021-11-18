using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : Bullet
{
    [SerializeField] private Rigidbody2D rgbd = null;
    public ChargedBS stage;
    private bool shooted,
        dead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            StartCoroutine(Die());
        }

        if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }

    public void ChargeStarter()
    {
        if (!shooted && transform.localScale.x >= 4.5)
        {
            shooted = true;
            StartCoroutine(Chase());
        }
        else if (!shooted)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Chase()
    {
        while (true)
        {
            if (stage == ChargedBS.Chase)
            {
                Aim(GameManager.instance.player.transform.position);
                rgbd.velocity = transform.up * 350 * Time.fixedDeltaTime;
            }
            else
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Charge()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!dead)
            {
                Aim(GameManager.instance.player.transform.position);
                rgbd.velocity = Vector2.zero;
                rgbd.velocity = transform.up * 500 * Time.fixedDeltaTime;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Die()
    {
        rgbd.velocity = Vector2.zero;
        GetComponent<CircleCollider2D>().enabled = false;
        dead = true;
        while (true)
        {
            if (transform.localScale.x > 0.3)
            {
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * 150 * Time.fixedDeltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void Update()
    {
        float Distance = (GameManager.instance.player.transform.position - transform.position).magnitude;
        if (Distance < 7 && stage == ChargedBS.Chase)
        {
            stage = ChargedBS.Charge;
            StartCoroutine(Charge());
        }
    }
}

public enum ChargedBS { Chase, Charge }