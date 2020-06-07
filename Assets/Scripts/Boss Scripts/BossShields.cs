using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShields : MonoBehaviour
{
    public int life;

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
        }
    }

    void Update()
    {
        if (life <= 0 && GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.FirstPhase)
        {
            life = 41;
            GameObject ins = Instantiate(GameManager.instance.ammoPrefab, transform.position, Quaternion.identity);
            ins.GetComponent<Ammo>().m_fOA = FoodOrAmmo.Food;
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
            GameObject ins = Instantiate(GameManager.instance.ammoPrefab, transform.position, Quaternion.identity);
            ins.GetComponent<Ammo>().m_fOA = FoodOrAmmo.Food;
            Destroy(gameObject);
        }
    }
}
