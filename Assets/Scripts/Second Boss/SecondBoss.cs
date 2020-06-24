using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SecondBoss : Enemies
{
    public GameObject circle;
    public GameObject cameras;
    public GameObject aimCam;
    public GameObject damageCam;
    public GameObject blackSpace;
    private bool ready;
    public Animator blackSpaceAnim;

    void Start()
    {
        ready = false;
        transform.localScale = new Vector3(0, 0, 0);

        LifeAndVelocityAsigner();
        StartCoroutine(Aproach());
        StartCoroutine(CircleCreator());
    }

    IEnumerator Aproach()
    {
        while (transform.localScale.y < 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.01f, 1.01f, 1.01f), Time.deltaTime);
            yield return null;
        }

        ready = true;
    }

    IEnumerator CircleCreator()
    {
        while (true)
        {
            if (ready)
            {
                yield return new WaitForSeconds(3);
                cameras.SetActive(false);
                aimCam.SetActive(true);
                GameManager.instance.player.GetComponent<Player>().unleashed = true;
                blackSpaceAnim.SetTrigger("enterBS");
                CreateCircle();
                yield return new WaitForSeconds(27);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player Bullet" && ready)
        {
            collision.transform.position = new Vector3(1000, 1000);
            collision.gameObject.SetActive(false);
            life--;

            if (life <= 0)
            {
                Instantiate(GameManager.instance.fireworksOne, transform.position * new Vector2(Random.Range(-4, 5), Random.Range(-4, 5)), Quaternion.identity);
                Instantiate(GameManager.instance.fireworksTwo, transform.position * new Vector2(Random.Range(-4, 5), Random.Range(-4, 5)), Quaternion.identity);
                GameManager.instance.winMessage.SetActive(true);
                PlayerPrefs.SetInt("actualLevel", 1);
                MenuController.blockPause = true;
                Analytics.CustomEvent("Winner", new Dictionary<string, object>
                {
                    {"second boss with Life", GameManager.instance.player.GetComponent<Player>().life},
                });
                gameObject.SetActive(false);
            }
        }
    }

    public void CreateCircle()
    {
        Instantiate(circle, GameManager.instance.player.transform.position, Quaternion.identity);
    }
}
