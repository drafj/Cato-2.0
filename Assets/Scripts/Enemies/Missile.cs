using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Enemies
{
    [SerializeField] private GameObject warning = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private Collider2D _collider = null;
    private bool goDown = false;

    void Start()
    {
        LifeAndVelocityAsigner();
    }

    private void OnEnable()
    {
        LifeAndVelocityAsigner();
        Invoke(nameof(GoDown), 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            TakeDamage(bullet);
            if (life <= 0)
            {
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + 8);
                if (PlayerPrefs.GetInt("Harpy", 0) < 3 && GameManager.instance.player.GetComponent<Player>().inmobilized)
                    PlayerPrefs.SetInt("Harpy", PlayerPrefs.GetInt("Harpy", 0) + 1);
                FindObjectOfType<MenuController>().SetPoints();
                GameManager.instance.counterToBoss++;
                anim.SetTrigger("Death");
                AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
                _collider.enabled = false;
            }
        }

        if (collision.transform.tag == "Border")
        {
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            anim.SetTrigger("Death");
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            _collider.enabled = false;
        }
    }

    public override void Die()
    {
        base.Die();
        warning.transform.parent = transform;
    }

    void GoDown()
    {
        goDown = true;
        warning.transform.parent = transform;
        warning.SetActive(false);
        GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Warning", false);
    }

    public override void LifeAndVelocityAsigner()
    {
        base.LifeAndVelocityAsigner();
        _collider.enabled = true;
        warning.transform.parent = null;
        warning.transform.position = new Vector2(transform.position.x, 11.5f);
        warning.SetActive(true);
        goDown = false;
    }

    private void FixedUpdate()
    {
        if (goDown)
        GoForward();
    }
}
