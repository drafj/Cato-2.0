using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Boss : Enemies
{
    public float cadence;
    public int rayCounter;
    public int shootRaysLimit;
    int raySelecTemporal;
    bool iterator;
    public bool rayCC;
    public Rigidbody2D rgbd;
    public List<GameObject> rays = new List<GameObject>();
    public List<BossBullet> m_bossBulletList = new List<BossBullet>();
    public Stage m_stage;
    public Phase m_phase;
    public displacement m_displacement;

    void Start()
    {
        LifeAndVelocityAsigner();

        cadence = 1;
        m_stage = Stage.Approaching;
        rgbd = GetComponent<Rigidbody2D>();
        velocity = -40f;
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (m_stage == Stage.Attacking && m_phase == Phase.FirstPhase)
            {
                GameObject R = Instantiate(GameManager.instance.bulletBossPrefab, transform.GetChild(0).position, transform.GetChild(0).rotation * Quaternion.Euler(0, 0, Random.Range(-20, 20)));
                R.GetComponent<BossBullet>().m_type = BossBulletTypes.R;
                yield return new WaitForSeconds(cadence);
                GameObject L = Instantiate(GameManager.instance.bulletBossPrefab, transform.GetChild(1).position, transform.GetChild(1).rotation * Quaternion.Euler(0, 0, Random.Range(-20, 20)));
                L.GetComponent<BossBullet>().m_type = BossBulletTypes.L;
                yield return new WaitForSeconds(cadence);
            }

            else if(m_stage == Stage.Attacking && m_phase == Phase.SecondPhase && rayCounter < shootRaysLimit)
            {
                GameObject R = Instantiate(GameManager.instance.bulletBossPrefab, transform.GetChild(0).position, Quaternion.Euler(0, 0, 180));
                R.GetComponent<BossBullet>().m_type = BossBulletTypes.R;
                rayCounter++;
                yield return new WaitForSeconds(cadence);
                GameObject L = Instantiate(GameManager.instance.bulletBossPrefab, transform.GetChild(1).position, Quaternion.Euler(0, 0, 180));
                L.GetComponent<BossBullet>().m_type = BossBulletTypes.L;
                rayCounter++;
                yield return new WaitForSeconds(cadence);
            }

            else if (m_stage == Stage.Attacking && m_phase == Phase.SecondPhase && rayCounter >= shootRaysLimit)
            {
                rayCounter = 0;
                if (iterator)
                GameManager.instance.m_pooler.SpawnRay(new Vector3(-30, -9f), Quaternion.identity);
                else
                GameManager.instance.m_pooler.SpawnRay(new Vector3(-30, -12.5f), Quaternion.identity);
                iterator = !iterator;
                yield return new WaitForSeconds(4);
            }
            yield return null;
        }
    }

    IEnumerator EnteringSecondPhase()
    {
        m_phase = Phase.SecondPhase;
        m_stage = Stage.Approaching;
        velocity = 0;
        rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(3);
        rgbd.constraints = RigidbodyConstraints2D.None;
        rgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
        cadence = 0.7f;
        velocity = -65;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player Bullet")
        {
            collision.transform.position = new Vector3(1000, 1000);
            collision.gameObject.SetActive(false);
            life--;

            if (life <= 0 && m_phase == Phase.FirstPhase)
            {
                if (GameManager.instance.m_pooler != null)
                    LifeAndVelocityAsigner();
                StartCoroutine(EnteringSecondPhase());
                transform.GetChild(2).gameObject.SetActive(false);

                foreach (BossBullet bullet in FindObjectsOfType<BossBullet>())
                {
                    m_bossBulletList.Add(bullet);
                }

                for (int i = 0; i < m_bossBulletList.Count; i++)
                {
                    m_bossBulletList[i].GetComponent<BossBullet>().pushed = true;
                    m_bossBulletList[i].GetComponent<BossBullet>().persecution = false;
                    m_bossBulletList[i].GetComponent<BossBullet>().rgbd.AddForce(Vector2.up * -600);
                }
            }

            else if (life <= 0 && m_phase == Phase.SecondPhase)
            {
                Instantiate(GameManager.instance.fireworksOne, transform.position * new Vector2(Random.Range(-4, 5), Random.Range(-4, 5)), Quaternion.identity);
                Instantiate(GameManager.instance.fireworksTwo, transform.position * new Vector2(Random.Range(-4, 5), Random.Range(-4, 5)), Quaternion.identity);
                GameManager.instance.winMessage.SetActive(true);
                PlayerPrefs.SetInt("actualLevel", 3);
                MenuController.blockPause = true;
                /*Analytics.CustomEvent("Winner", new Dictionary<string, object>
            {
                {"first boss with Life", GameManager.instance.player.GetComponent<Player>().life},
            });*/
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Aproach());
    }

    IEnumerator Aproach()
    {
        while (transform.position.y >= 9.5f)
        {
            if (!GameManager.instance.pause)
                rgbd.AddForce(transform.up * (velocity * 50) * Time.deltaTime);
            yield return null;
        }
    }

    void Update()
    {
        if (!GameManager.instance.pause)
        BossDisplacement();
    }

    void BossDisplacement()
    {
        if (transform.position.y <= 9.5f)
        {
            m_stage = Stage.Attacking;
            if (transform.position.x >= 0)
            {
                m_displacement = displacement.L;
            }
            else if (transform.position.x <= -1.26f)
            {
                m_displacement = displacement.R;
            }

            if (m_displacement == displacement.R)
            {
                rgbd.AddForceAtPosition(transform.right * (-velocity - 15), new Vector2(0, 9.5f));
            }
            else if (m_displacement == displacement.L)
            {
                rgbd.AddForceAtPosition(transform.right * (velocity + 15), new Vector2(-1.26f, 9.5f));
            }
        }
    }
}

public enum Stage {Approaching, Attacking}
public enum Phase {FirstPhase, SecondPhase}
public enum displacement {R, L};