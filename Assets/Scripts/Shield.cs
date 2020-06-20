using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private List<GameObject> bullets = new List<GameObject>();
    [SerializeField] private float destroyLimit;
    private float destructionCounter;
    private bool launchingBullets;
    
    void Start()
    {
        if (destroyLimit == 0)
            destroyLimit = 5;
    }

    void BulletLauncher()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        launchingBullets = true;
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].transform.SetParent(null);
            bullets[i].GetComponent<CircleCollider2D>().enabled = true;
            bullets[i].gameObject.GetComponent<BulletController>().onCourse = true;
        }
        AudioSource.PlayClipAtPoint(GameManager.instance.shieldCollapse, Camera.main.transform.position);
        GameManager.instance.player.GetComponent<Player>().Starter();
        bullets.Clear();
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy Bullet" && !launchingBullets && collision.gameObject.GetComponent<BulletController>() != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.bulletCollision, Camera.main.transform.position);
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.transform.tag = "Player Bullet";
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.GetComponent<BulletController>().onCourse = false;
            collision.gameObject.GetComponent<BulletController>().flip = true;
            bullets.Add(collision.gameObject);
        }

        else if (collision.transform.tag == "Enemy Bullet" && !launchingBullets && collision.gameObject.GetComponent<BossBullet>() != null)
        {
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        //Debug.Log(bullets.Count);
        if (!GameManager.instance.pause && !GameManager.instance.gameOver)
        destructionCounter += Time.deltaTime;

        if (GameManager.instance.player.GetComponent<Player>().ability || destructionCounter >= destroyLimit)
        {
            GameManager.instance.player.GetComponent<Player>().ability = false;
            BulletLauncher();
        }
    }
}
