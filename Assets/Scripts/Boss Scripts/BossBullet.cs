using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BossBullet : MonoBehaviour
{
    public bool persecution;
    public bool pushed;
    public Rigidbody2D rgbd;
    public BossBulletTypes m_type;

    void Start()
    {
        if (m_type == BossBulletTypes.L)
            GetComponent<Renderer>().sortingOrder = 19;

        rgbd = GetComponent<Rigidbody2D>();
        rgbd.AddForce(transform.up * 60);
        FirstPush();
    }

    public void FirstPush()
    {
        if (GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.FirstPhase)
        rgbd.AddForce(transform.up * 60);
        if (GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.SecondPhase)
            rgbd.AddForce(transform.up * 90);

        StartCoroutine("Pursuing");
    }

    IEnumerator Pursuing()
    {
        yield return new WaitForSeconds(3);
        if (!pushed)
        persecution = true;
    }

    IEnumerator TargetPlayerL()
    {
        int ct = 0;
        while (true)
        {
            ct++;
            if (ct <= 10)
            {
                TargetBasic();
                rgbd.AddForce(transform.up * 60);
                yield return new WaitForSeconds(0.3f);
            }
            else
                yield break;
        }
    }

    //IEnumerator Push()
    //{
    //    while (true)
    //    {
    //        persecution = false;
    //        rgbd.AddForce(Vector2.up * -400);
    //        yield return new WaitForSeconds(3);
    //        persecution = true;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (!GameManager.instance.player.GetComponent<Player>().invencible && GameManager.instance.Boss.GetComponent<Boss>().life > 0 && !pushed)
            {
                collision.gameObject.GetComponent<Player>().life = collision.gameObject.GetComponent<Player>().life - 1;
                GameManager.instance.StartDealDamage();
                if (collision.gameObject.GetComponent<Player>().life == 0)
                {
                    Analytics.CustomEvent("Death", new Dictionary<string, object>
                        {
                            {"death", "by a boss 01 bullet"}
                        });
                }
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (transform.position.y <= -16.5 || transform.position.y >= 13.5 || transform.position.x >= 17 || transform.position.x <= -17)
            Destroy(gameObject);

        if (persecution && !GameManager.instance.gameOver && !GameManager.instance.pause && m_type == BossBulletTypes.R && GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.FirstPhase)
            TargetPlayerR();
        if (persecution && !GameManager.instance.gameOver && !GameManager.instance.pause && m_type == BossBulletTypes.L && GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.FirstPhase)
        {
            persecution = false;
            StartCoroutine(TargetPlayerL());
        }

        else if (persecution && !GameManager.instance.gameOver && !GameManager.instance.pause && GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.SecondPhase)
        {
            //persecution = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 10 * Time.deltaTime);
            rgbd.velocity = (transform.up * 20);
        }
    }

    void TargetBasic()
    {
        Vector3 diff = GameManager.instance.player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void TargetPlayerR()
    {
        persecution = false;
        TargetBasic();
        rgbd.velocity = transform.up * 7;
    }
}

public enum BossBulletTypes {R, L}