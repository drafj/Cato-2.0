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
            anim.SetTrigger("Death");
            AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
            _collider.enabled = false;
        }
    }

    public override void Die()
    {
        base.Die();
        _collider.enabled = true;
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
