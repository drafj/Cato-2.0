using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver;
    public bool pause;
    public bool spawnEnemies;
    public GameObject Boss;
    public GameObject gOPusher;
    public GameObject bulletBossPrefab;
    public GameObject shotPP;
    public GameObject shotEP;
    public GameObject player;
    public GameObject shield;
    public GameObject ammoPrefab;
    public GameObject damageCamera;
    public GameObject normalCameras;
    public GameObject deathCamera;
    public GameObject deathCirclesCamera;
    public GameObject deathCirclesDamCamera;
    public GameObject pauseMenu;
    public GameObject flashParticles;
    public GameObject fireworksOne;
    public GameObject fireworksTwo;
    public GameObject winMessage;
    public FixedJoystick m_Joystick;
    public Image life;
    public Image food;
    public Text points;
    public Sprite playerBulletSprite;
    public Sprite enemyBulletSprite;
    public AudioClip bulletCollision;
    public AudioClip shieldCollapse;
    public AudioClip enemyDeath;
    public AudioClip playerShot;
    public AudioClip loseClip;
    public AudioSource ambientSound;
    public Pooler m_pooler;
    public float secondGunDamage;
    public float thirdGunDamage;
    public int ammoFallVelocity;
    public int counterToBoss;
    public int arrivalBoss;
    private int enemyChooser;
    private int enemyCounter = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        arrivalBoss = arrivalBoss == 0 ? 30 : arrivalBoss;

        if (Player.bossPhase)
        {
            spawnEnemies = false;
            counterToBoss = arrivalBoss;
        }
    }

    void Start()
    {
        StartCoroutine(EnemySpawner());
    }


    IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(0.3f);
        while (!gameOver && spawnEnemies)
        {
            if (!pause)
            {
                if (enemyCounter < 2)
                {
                    enemyCounter++;
                    enemyChooser = Random.Range(0, 2);
                    switch (enemyChooser)
                    {
                        case 0:
                            m_pooler.Spawner127(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                            break;
                        case 1:
                            m_pooler.PursuerSpawner(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                            break;
                    }
                    yield return new WaitForSeconds(1.5f);
                }
                else
                {
                    enemyCounter = 0;
                    enemyChooser = Random.Range(0, 3);
                    switch (enemyChooser)
                    {
                        case 0:
                            m_pooler.Spawner127(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                            break;
                        case 1:
                            m_pooler.PursuerSpawner(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                            break;
                        case 2:
                            m_pooler.ShooterSpawner(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                            break;
                    }
                    yield return new WaitForSeconds(1.5f);
                }
            }
            yield return null;
        }
    }

    public IEnumerator Invencible(float time)
    {
        if (time <= 0)
            time = 3;
        player.GetComponent<Player>().anim.SetBool("Damage", true);
        player.GetComponent<Player>().invencible = true;
        yield return new WaitForSeconds(time);
        player.GetComponent<Player>().anim.SetBool("Damage", false);
        player.GetComponent<Player>().invencible = false;
    }

    public IEnumerator DealDamageC()
    {
        if (!player.GetComponent<Player>().unleashed)
        {
            normalCameras.SetActive(false);
            damageCamera.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            damageCamera.SetActive(false);
            normalCameras.SetActive(true);
        }

        else
        {
            deathCirclesCamera.SetActive(false);
            deathCirclesDamCamera.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            deathCirclesCamera.SetActive(true);
            deathCirclesDamCamera.SetActive(false);
        }

        if (!player.GetComponent<Player>().unleashed)
        {
            normalCameras.SetActive(true);
            deathCirclesCamera.SetActive(false);
        }

    }

    public void StartDealDamage()
    {
        if (!player.GetComponent<Player>().invencible)
        {
            StartCoroutine("DealDamageC");
            StartCoroutine(Invencible(3));
        }
    }

    public void InvokeBoss()
    {
        Boss.SetActive(true);
        if (gOPusher != null)
            gOPusher.SetActive(true);
    }

    void Update()
    {
        if (counterToBoss >= arrivalBoss)
        {
            spawnEnemies = false;
            counterToBoss = 0;
            Invoke("InvokeBoss", 3);
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            player.GetComponent<Player>().anim.SetBool("OnTouch", true);
        else
            player.GetComponent<Player>().anim.SetBool("OnTouch", false);


        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
}
