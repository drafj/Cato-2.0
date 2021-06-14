﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Player : MonoBehaviour
{
    public float velocity;
    public float life;
    public float lifeAmount;
    public int secondAmmo;
    public int thirdAmmo;
    public int flashRange;
    public int points;
    public float foodMeter;
    [SerializeField] private bool shieldColdDown,
    flashColdDown,
    minimeColdown;
    GameObject shieldInst;
    public bool OnShooting;
    public bool ability;
    public bool invencible;
    public bool pushed;
    public bool unleashed;
    public static bool bossPhase;
    public CanyonOrder mOrder;
    public Gunz m_Gunz;
    public Abilities m_abilities;
    public Animator anim;
    public Rigidbody2D rgbd;
    public Pooler m_pooler;
    public Vector2 joyStickDir;
    public Vector2 axis;
    public GameObject actualShield;
    public Vector3[] canyonPositions = new Vector3[2];
    public GameObject minime;
    [SerializeField] private List<GameObject> minimeList = new List<GameObject>();

    private void Awake()
    {
        if (velocity == 0)
            velocity = 6;

        if (flashRange == 0)
            flashRange = 30;

        life = 100;
        anim = GetComponent<Animator>();

        for (int i = 0; i < 2; i++)
        {
            GameObject temporal = Instantiate(minime, new Vector3(9000, 9000, 9000), Quaternion.identity);
            temporal.SetActive(false);
            minimeList.Add(temporal);
            minimeList[i].transform.name = "minime: " + (i + 1);
        }
        minimeList.Add(null);
    }

    void Start()
    {
        //m_abilities = (Abilities)MenuController.selection;----------------descomentar para que funcione la seleccion de habilidad------------------------------------------
        points = PlayerPrefs.GetInt("Points");
        if (GameManager.instance.points != null)
            GameManager.instance.points.text = points + " :points ";

        if (GameManager.instance.Boss.activeInHierarchy)
            bossPhase = true;
        else
            bossPhase = false;
        StartCoroutine(BulletCreator());
        GameManager.instance.food.GetComponent<Image>().fillAmount = foodMeter;
    }

    IEnumerator BulletCreator()
    {
        while (true)
        {
            if (!GameManager.instance.pause && !GameManager.instance.gameOver)
            {
                if (secondAmmo > 0 && thirdAmmo <= 0)
                    m_Gunz = Gunz.SecondGun;
                else if (thirdAmmo > 0)
                    m_Gunz = Gunz.ThirdGun;
                else
                    m_Gunz = Gunz.BasicGun;

                if (thirdAmmo < 0)
                    thirdAmmo = 0;
                switch (m_Gunz)
                {
                    case Gunz.BasicGun:
                        if (OnShooting)
                        {
                            if (mOrder == CanyonOrder.Right)
                            {
                                m_pooler.Spawner(transform.localPosition + canyonPositions[0], Quaternion.identity);
                                mOrder = CanyonOrder.Left;
                            }
                            else
                            {
                                m_pooler.Spawner(transform.localPosition + canyonPositions[1], Quaternion.identity);
                                mOrder = CanyonOrder.Right;
                                m_pooler.bulletsPool[0].GetComponent<BulletController>().StartBullet();
                            }
                            AudioSource.PlayClipAtPoint(GameManager.instance.playerShot, Camera.main.transform.position);
                            yield return new WaitForSeconds(0.3f);
                        }
                        break;
                    case Gunz.SecondGun:
                        if (OnShooting)
                        {
                            secondAmmo--;
                            if (mOrder == CanyonOrder.Right)
                                m_pooler.Spawner(transform.localPosition + canyonPositions[0], Quaternion.identity);
                            else
                                m_pooler.Spawner(transform.localPosition + canyonPositions[1], Quaternion.identity);
                            if (mOrder == CanyonOrder.Right)
                                mOrder = CanyonOrder.Left;
                            else
                                mOrder = CanyonOrder.Right;
                            AudioSource.PlayClipAtPoint(GameManager.instance.playerShot, Camera.main.transform.position);
                            yield return new WaitForSeconds(0.3f);
                        }
                        break;
                    case Gunz.ThirdGun:
                        if (OnShooting)
                        {
                            thirdAmmo -= 6;

                            for (int i = 0; i < 6; i++)
                            {
                                if (i < 3)
                                {
                                    m_pooler.Spawner(transform.localPosition + canyonPositions[0], Quaternion.identity);
                                }
                                else
                                {
                                    m_pooler.Spawner(transform.localPosition + canyonPositions[1], Quaternion.identity);
                                }

                            }

                            AudioSource.PlayClipAtPoint(GameManager.instance.playerShot, Camera.main.transform.position);
                            yield return new WaitForSeconds(0.3f);
                        }
                        break;
                    default:
                        break;
                }
            }

            yield return null;
        }
    }

    public IEnumerator ShieldColdDown()
    {
        yield return new WaitForSeconds(3);
        shieldColdDown = false;
    }

    IEnumerator FlashColdDown()
    {
        yield return new WaitForSeconds(3);
        flashColdDown = false;
    }

    IEnumerator MinimeColdown()
    {
        yield return new WaitForSeconds(3);
        minimeColdown = false;
    }

    public IEnumerator Die()
    {
        if (GameManager.instance.Boss.activeInHierarchy)
            bossPhase = true;
        else
            bossPhase = false;
        anim.SetTrigger("Dead");
        GetComponent<CapsuleCollider2D>().enabled = false;
        AudioSource.PlayClipAtPoint(GameManager.instance.loseClip, GameManager.instance.deathCamera.transform.position);
        GameManager.instance.ambientSound.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(/*segundos que demore la animacion de muerte*/4f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("actualLevel", 1));
    }

    public void Starter()
    {
        StartCoroutine("ShieldColdDown");
    }

    public void UseAbility()
    {
        if (!flashColdDown && m_abilities == Abilities.Flash && !GameManager.instance.pause && !GameManager.instance.gameOver || Input.GetKeyDown(KeyCode.Space) && !flashColdDown)
        {
            flashColdDown = true;
            StartCoroutine("FlashColdDown");
            Instantiate(GameManager.instance.flashParticles, transform.position + new Vector3(0, 0, -5), Quaternion.identity);
            if (anim.GetBool("OnTouch"))
                transform.Translate(joyStickDir * 30);
            else
                transform.Translate(axis * 30);
            Instantiate(GameManager.instance.flashParticles, transform.position + new Vector3(0, 0, -5), Quaternion.identity);
            StartCoroutine(GameManager.instance.Invencible(PlayerPrefs.GetInt("flashInv", 1)));
        }

        if (!shieldColdDown && m_abilities == Abilities.Shield && !GameManager.instance.pause && !GameManager.instance.gameOver)
        {
            shieldColdDown = true;
            shieldInst = Instantiate(GameManager.instance.shield, transform);
            shieldInst.transform.localPosition = new Vector3(0, 1.27f, -9.041016f);
        }
        else if (shieldColdDown)
            shieldInst.GetComponent<Shield>().BulletLauncher();

        if (!minimeColdown && m_abilities == Abilities.Minime && !GameManager.instance.pause && !GameManager.instance.gameOver)
        {
            minimeColdown = true;
            StartCoroutine(MinimeSpawner(transform.position, Quaternion.identity));
        }
    }

    public void TakeFood()
    {
        foodMeter += 0.34f;
        if (foodMeter >= 1)
        {
            foodMeter = 0;
            if (life < 100)
                life += 20;
            else
                StartCoroutine(GameManager.instance.Invencible(3));
        }

        GameManager.instance.food.GetComponent<Image>().fillAmount = foodMeter;
    }

    public void UpdateLife()
    {
        lifeAmount = life / 100;
        GameManager.instance.life.GetComponent<Image>().fillAmount = lifeAmount;
        if (life <= 0)
        {
            Analytics.CustomEvent("Death", new Dictionary<string, object>
            {
                {"death", "by enemy"}
            });
            GameManager.instance.gameOver = true;
            StartCoroutine("Die");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemies>() != null && life > 0 && !invencible)
        {
            GameManager.instance.StartDealDamage();
            life -= 5;
        }
        UpdateLife();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<RayController>() != null)
        {
            if (life > 0 && !invencible)
            {
                life -= 5;
                GameManager.instance.StartDealDamage();
            }

            if (life <= 0)
            {
                Analytics.CustomEvent("Death", new Dictionary<string, object>
                {
                    {"death", "by boss ray"}
                });
                GameManager.instance.gameOver = true;
                StartCoroutine("Die");
            }
        }
    }

    public IEnumerator MinimeSpawner(Vector3 pos, Quaternion rot)
    {
        minimeList[0].GetComponent<Minime>().catchable = false;
        minimeList[0].transform.position = pos;
        minimeList[0].transform.rotation = rot;
        minimeList[0].SetActive(true);
        minimeList[minimeList.Count - 1] = minimeList[0];
        for (int i = 0; i < minimeList.Count - 1; i++)
        {
            minimeList[i] = minimeList[i + 1];
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(MinimeColdown());
    }

    void Update()
    {


        /*if (GameManager.instance.Boss.GetComponent<Boss>() != null)                                    cambia esto boludo
        {
            if (!pushed && GameManager.instance.Boss.GetComponent<Boss>().m_phase == Phase.SecondPhase)
            {
                pushed = true;
                rgbd.AddForce(transform.up * -3000);
            }
        }*/
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.pause && !GameManager.instance.gameOver)
        {
            if (anim.GetBool("OnTouch"))
                JoystickMoviment();
            else
                KeyboardMoviment();

            if (!unleashed)
            {
                if (transform.position.x > 6.85)
                    transform.position = new Vector3(6.85f, transform.position.y);
                if (transform.position.x < -6.85)
                    transform.position = new Vector3(-6.85f, transform.position.y);
                if (transform.position.y > 11.3)
                    transform.position = new Vector3(transform.position.x, 11.3f);
                if (transform.position.y < -14)
                    transform.position = new Vector3(transform.position.x, -14f);
            }
        }
    }

    public void JoystickMoviment()
    {
        joyStickDir = GameManager.instance.m_Joystick.Direction * velocity * Time.deltaTime;

        rgbd.velocity += new Vector2(joyStickDir.x, joyStickDir.y);

        anim.SetFloat("VelX", joyStickDir.x);
    }

    void KeyboardMoviment()
    {
        axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axis = axis * velocity * Time.deltaTime;

        rgbd.velocity += new Vector2(axis.x, axis.y);
        anim.SetFloat("VelX", Input.GetAxis("Horizontal"));
    }
}

public enum CanyonOrder {Left, Right}

public enum Gunz {BasicGun, SecondGun, ThirdGun}

public enum Abilities {Shield, Flash, Minime}

public enum Size
{
    Big,
    Tiny
}