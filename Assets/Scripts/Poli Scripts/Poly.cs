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
    [SerializeField] private GameObject pushers = null;
    [SerializeField] private Slider healthBar = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private AudioSource warninAudio = null,
        polyDragAudio = null,
        closeJawAudio = null;
    private int lastPos = 0;

    private void OnEnable()
    {
        armor = true;
        LifeAndVelocityAsigner();
        SetMaxHealth();
        healthBar.gameObject.SetActive(true);
        StartCoroutine(Entering());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            switch (bullet.gunz)
            {
                case Gunz.PIERCING:
                        life -= 3f;
                    break;
                case Gunz.LASER:
                        life -= 4;
                    break;
                case Gunz.PLASMA:
                        life -= 6;
                    break;
            }
            SetMaxHealth();

            if (life <= 0)
            {
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + 1000);
                FindObjectOfType<MenuController>().SetPoints();
                healthBar.gameObject.SetActive(false);
                GetComponent<Collider2D>().enabled = false;
                pushers.GetComponent<Collider2D>().enabled = false;
                eater.GetComponent<Collider2D>().enabled = false;
                PlayerPrefs.SetInt("actualLevel", 1);
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

    public void CloseJawAudio()
    {
        closeJawAudio.Play();
    }

    public override void Die()
    {
        base.Die();
        winMessage.SetActive(true);
    }

    IEnumerator Entering()
    {
        warning.SetActive(true);
        GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Warning", true);
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Warning", false);
        warning.SetActive(false);
        warninAudio.Stop();
        while (transform.position.y > 9.5f)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(0, 9f), 2 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2f);
        while (transform.position.y < 16.5f)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(0, 17f), 2 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(MoveBehaviour());
    }

    IEnumerator MoveBehaviour()
    {
        if (life > 0)
        {
            GetComponent<Collider2D>().enabled = true;
            eater.gameObject.SetActive(true);
            transform.GetChild(4).gameObject.SetActive(true);
            anim.SetBool("Attack", false);
            warninAudio.loop = false;
            warninAudio.Play();
            warning.SetActive(true);
            int actualPos = Random.Range(0, 6);
            while (actualPos == lastPos)
            {
                actualPos = Random.Range(0, 6);
            }
            lastPos = actualPos;
            transform.position = positions[actualPos];
            GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Warning", true);

            if (actualPos < 3)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 180);
            yield return new WaitForSeconds(2f);

            warning.SetActive(false);
            GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Warning", false);
            StartCoroutine(Move(transform.position));
        }
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
                    polyDragAudio.Play();
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
                    polyDragAudio.Play();
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
