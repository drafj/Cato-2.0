using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Poly : Enemies
{
    [SerializeField] private List<Vector2> positions = new List<Vector2>();
    [SerializeField] private GameObject warning = null,
        winMessage = null;
    [SerializeField] private SoulEater eater = null;
    [SerializeField] private Slider healthBar = null;
    [SerializeField] private Animator anim = null;
    private int lastPos = 0;

    private void OnEnable()
    {
        armor = true;
        LifeAndVelocityAsigner();
        SetMaxHealth();
        healthBar.gameObject.SetActive(true);
        StartCoroutine(MoveBehaviour());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            switch (bullet.gunz)
            {
                case Gunz.PIERCING:
                        life -= 0.5f;
                    break;
                case Gunz.LASER:
                        life -= 1;
                    break;
                case Gunz.PLASMA:
                        life -= 3;
                    break;
            }
            SetMaxHealth();

            if (life <= 0)
            {
                healthBar.gameObject.SetActive(false);
                anim.SetTrigger("Death");
            }
        }
    }

    void SetMaxHealth()
    {
        healthBar.maxValue = lifeReference;
        healthBar.value = life;
    }

    public void SetHealth()
    {
        healthBar.value = life;
    }

    public override void Die()
    {
        base.Die();
        winMessage.SetActive(true);
    }

    IEnumerator MoveBehaviour()
    {
        anim.SetBool("Attack", false);
        warning.SetActive(true);
        int actualPos = Random.Range(0, 6);
        while (actualPos == lastPos)
        {
            actualPos = Random.Range(0, 6);
        }
        lastPos = actualPos;
        transform.position = positions[actualPos];

        if (actualPos < 3)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 180);
        yield return new WaitForSeconds(2f);

        warning.SetActive(false);
        StartCoroutine(Move(transform.position));
    }

    IEnumerator Move(Vector3 pos)
    {
        bool waited = false;
        while (life > 0)
        {
            if (pos.y > 0 && transform.position.y >= -19)
            {
                if (transform.position.y >= 10)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 9, 0), velocity * Time.fixedDeltaTime);
                }
                else if (!waited)
                {
                    waited = true;
                    if (eater.bellyFull)
                        eater.Spit();
                    yield return new WaitForSeconds(1.5f);
                    anim.SetBool("Attack", true);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -20, 0), velocity * Time.fixedDeltaTime);
                }
            }
            else if (pos.y > 0)
            {
                StartCoroutine(MoveBehaviour());
                break;
            }
            else if (pos.y < 0 && transform.position.y <= 17)
            {
                if (transform.position.y <= -11)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -10, 0), velocity * Time.fixedDeltaTime);
                }
                else if (!waited)
                {
                    waited = true;
                    if (eater.bellyFull)
                        eater.Spit();
                    yield return new WaitForSeconds(1.5f);
                    anim.SetBool("Attack", true);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 18, 0), velocity * Time.fixedDeltaTime);
                }
            }
            else
            {
                StartCoroutine(MoveBehaviour());
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
