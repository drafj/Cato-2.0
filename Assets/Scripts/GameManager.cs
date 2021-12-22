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
    public GameObject shotPP;
    public GameObject player;
    public GameObject shield;
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
    public AudioClip bulletCollision;
    public AudioClip shieldCollapse;
    public AudioClip enemyDeath;
    public AudioClip playerShot;
    public AudioClip loseClip;
    public AudioSource ambientSound;
    public Pooler m_pooler;
    public int counterToBoss;
    public int arrivalBoss;
    private int enemyChooser;

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
                enemyChooser = Random.Range(0, 5);
                switch (enemyChooser)
                {
                    case 0:
                        m_pooler.Spawner127(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                        break;
                    case 1:
                        m_pooler.PursuerSpawner(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                        break;
                    case 2:
                        m_pooler.MissileSpawner(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                        break;
                    case 3:
                        m_pooler.BombSpawner(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                        break;
                    case 4:
                        m_pooler.BigSpawner(new Vector3(Random.Range(-7.18f, 7.18f), 14.76f, -5), Quaternion.identity);
                        break;
                }
                yield return new WaitForSeconds(1.5f);
            }
            yield return null;
        }
    }

    public IEnumerator DealDamageC()
    {
        normalCameras.SetActive(false);
        damageCamera.SetActive(true);
        yield return new WaitForSeconds(0.35f);
        damageCamera.SetActive(false);
        normalCameras.SetActive(true);

        //deathCirclesCamera.SetActive(false);
        //deathCirclesDamCamera.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        //deathCirclesCamera.SetActive(true);
        //deathCirclesDamCamera.SetActive(false);

        //normalCameras.SetActive(true);
        //deathCirclesCamera.SetActive(false);
    }

    public void StartDealDamage()
    {
         StartCoroutine("DealDamageC");
    }

    public void InvokeBoss()
    {
        Boss.SetActive(true);
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
