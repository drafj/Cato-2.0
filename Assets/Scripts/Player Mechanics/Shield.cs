using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public List<GameObject> bullets = new List<GameObject>();
    public float destroyLimit;
    private float destructionCounter;
    public bool launchingBullets;
    
    void Start()
    {
        //destroyLimit = PlayerPrefs.GetInt("shieldTime", 5);
    }

    public void BulletLauncher()
    {
        launchingBullets = true;
        GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].transform.SetParent(null);
            bullets[i].GetComponent<Collider2D>().enabled = true;
            bullets[i].gameObject.GetComponent<BulletController>().onCourse = true;
        }
        AudioSource.PlayClipAtPoint(GameManager.instance.shieldCollapse, Camera.main.transform.position);
        GameManager.instance.player.GetComponent<Player>().StartShieldCC();
        bullets.Clear();
        Destroy(gameObject);
    }

    void Update()
    {
        if (!GameManager.instance.pause && !GameManager.instance.gameOver)
        destructionCounter += Time.deltaTime;

        if (destructionCounter >= destroyLimit && !launchingBullets)
        {
            BulletLauncher();
        }
    }
}
