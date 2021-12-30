using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpy : Enemies
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Collider2D _collider = null;
    [SerializeField] private Rigidbody2D rgbd = null;
    [SerializeField] private bool chasing = false;
    [SerializeField] private bool enganched = false;
    [SerializeField] private bool goDown = true;
    private Vector3 playerPos = new Vector3();

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
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + 10);
                if (PlayerPrefs.GetInt("Harpy", 0) < 3 && GameManager.instance.player.GetComponent<Player>().inmobilized)
                    PlayerPrefs.SetInt("Harpy", PlayerPrefs.GetInt("Harpy", 0) + 1);
                FindObjectOfType<MenuController>().SetPoints();
                anim.SetTrigger("Death");
                AudioSource.PlayClipAtPoint(GameManager.instance.enemyDeath, Camera.main.transform.position);
                chasing = false;
                enganched = false;
                goDown = true;
                rgbd.velocity = Vector2.zero;
                _collider.enabled = false;
            }
        }

        if (collision.transform.tag == "Border")
        {
            GameManager.instance.counterToBoss++;
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }

        if (collision.gameObject.TryGetComponent(out Player player))
        {
            StartCoroutine(InmobilizePlayer(player));
        }
    }

    IEnumerator CatchPlayer()
    {
        while (!enganched && life > 0)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, 2 * Time.fixedDeltaTime);
            Vector3 distance = transform.position - playerPos;
            if (distance.magnitude <= 0.5f && !enganched)
            {
                goDown = true;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator InmobilizePlayer(Player player)
    {
        GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Root", true);
        goDown = false;
        enganched = true;
        _collider.enabled = false;
        rgbd.velocity = Vector2.zero;
        rgbd.simulated = false;
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0, -0.5f, 0);
        player.inmobilized = true;
        player.rgbd.AddForce(Vector2.up * 2000);
        yield return new WaitForSeconds(1.5f);
        if (PlayerPrefs.GetInt("Harpy", 0) < 3)
            PlayerPrefs.SetInt("Harpy", 0);
        GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Root", false);
        player.inmobilized = false;
        transform.parent = null;
        rgbd.simulated = true;
        goDown = true;
        anim.SetTrigger("Death");
    }

    void PlayerIsClose()
    {
        Vector3 distance = transform.position - GameManager.instance.player.transform.position;
        if (distance.magnitude <= 9 && life > 0)
        {
            playerPos = GameManager.instance.player.transform.position;
            chasing = true;
            goDown = false;
            rgbd.velocity = Vector2.zero;
            StartCoroutine(CatchPlayer());
        }
    }

    void Movement()
    {
        if (transform.position.x >= 3f)
        {
            rgbd.velocity = Vector2.zero;
            rgbd.AddForce(transform.right * -velocity * Time.fixedDeltaTime);
        }
        else if (transform.position.x <= -3f)
        {
            rgbd.velocity = Vector2.zero;
            rgbd.AddForce(transform.right * velocity * Time.fixedDeltaTime);
        }
    }

    public override void LifeAndVelocityAsigner()
    {
        base.LifeAndVelocityAsigner();
        _collider.enabled = true;
        chasing = false;
        enganched = false;
        goDown = true;
        rgbd.velocity = Vector2.zero;
        rgbd.AddForce(transform.right * velocity * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        if (!chasing && life > 0)
        Movement();
        if (goDown)
        transform.position -= transform.up * Time.deltaTime * 5.3f;
    }

    private void Update()
    {
        if (!chasing && life > 0)
        PlayerIsClose();
    }
}
