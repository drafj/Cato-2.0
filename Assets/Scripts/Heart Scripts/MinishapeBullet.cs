using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinishapeBullet : Bullet
{
    [SerializeField] private GameObject playerBullet = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            gameObject.SetActive(false);
        }

        if (collision.TryGetComponent(out Shield shield))
        {
            if (!shield.launchingBullets)
            {
                GameObject temp = Instantiate(playerBullet, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(GameManager.instance.bulletCollision, Camera.main.transform.position);
                temp.GetComponent<BulletController>().oldEnemyBullet = true;
                temp.GetComponent<CircleCollider2D>().enabled = false;
                temp.transform.SetParent(shield.transform);
                temp.GetComponent<BulletController>().onCourse = false;
                temp.GetComponent<BulletController>().flip = true;
                shield.bullets.Add(temp);
            }
            gameObject.SetActive(false);
        }
    }
}
