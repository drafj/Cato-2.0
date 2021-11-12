using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShields : MonoBehaviour
{
    public int life;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (life == 0)
            life = 30;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player Bullet")
        {
            collision.transform.position = new Vector3(1000, 1000);
            collision.gameObject.SetActive(false);
            life--;
            anim.SetTrigger("damage");
        }
    }

    void Update()
    {
        if (life <= 0 && GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.FirstPhase)
        {
            life = 41;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else if (life > 31 && GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.SecondPhase)
        {
            life = 30;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
        else if (life <= 0 && GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.SecondPhase)
        {
            Destroy(gameObject);
        }
    }
}
