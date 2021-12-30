using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : Enemies
{
    [SerializeField] private GameObject miniHearPref = null,
        rayPref = null,
        chargedBulletPref = null,
        leftHeart = null,
        rightHeart = null,
        shieldParticle = null;
    [SerializeField] private Slider healthBar = null;
    [SerializeField] private GameObject[] shields = new GameObject[4];
    [SerializeField] private Transform leftCannon = null,
        rightCannon = null,
        rayCannon = null;
    [SerializeField] private BulletsPooler bulletsPooler = null;
    [SerializeField] private Rigidbody2D rgbd = null;
    [SerializeField] private Animator anim = null;
    [HideInInspector] public int counterToMoveAgain,
        counterToRay;
    [SerializeField]
    private AudioSource warninAudio = null,
        shieldAudio = null,
        healAudio = null;
    [SerializeField] private AudioClip audioShot = null;
    [SerializeField]
    private AudioClip beanAudio = null;
    private int actualShield = 0;
    private GameObject actualChargedB = null;
    private bool stopMoving = false,
        pursuerRot = false,
        dontShoot = false;
    public bool healing;
    public bool stopHealAbility;
    public bool alreadyHealed;

    void Start()
    {
        healthBar.gameObject.SetActive(true);
        armor = true;
        LifeAndVelocityAsigner();
        SetMaxHealth();
        StartCoroutine(Entering());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            if (!healing)
            {
                TakeDamage(bullet);
                if (life <= (lifeReference / 2) && !alreadyHealed)
                {
                    Stop();
                    StartCoroutine(GoToCenter());
                    alreadyHealed = true;
                }
                if (life <= 0)
                {
                    if (actualShield < 4)
                    {
                        alreadyHealed = false;
                        LifeAndVelocityAsigner();
                        shields[actualShield].SetActive(false);
                        actualShield++;
                        if (actualShield < 4)
                        {
                            shields[actualShield].SetActive(true);
                            if (actualShield == 3)
                                armor = false;
                        }
                        else
                        {
                            StartCoroutine(StartingHeartAttack());
                        }
                    }
                    else
                    {
                        leftCannon.parent.gameObject.SetActive(false);
                        rightCannon.parent.gameObject.SetActive(false);
                        GetComponent<Collider2D>().enabled = false;
                        dontShoot = true;
                        Stop();
                        anim.SetTrigger("Death");
                        PlayerPrefs.SetInt("actualLevel", 2);
                    }
                }
            }
            else
            {
                if (actualChargedB == null)
                { 
                    actualChargedB = Instantiate(chargedBulletPref, transform.position, Quaternion.identity);
                }
                else
                {
                    if (actualChargedB.GetComponent<ChargedBullet>().stage == ChargedBS.Chase)
                    actualChargedB.transform.localScale += new Vector3(0.2f, 0.2f, 0);
                }
            }
            SetHealth();
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (!dontShoot)
            {
                if (pursuerRot)
                    StartCoroutine(ShootPursuer(leftCannon.position, Quaternion.Euler(0, 0, 135)));
                else
                    StartCoroutine(ShootPursuer(leftCannon.position, Quaternion.Euler(0, 0, -135)));
                yield return new WaitForSeconds(2f);
                if (!dontShoot)
                {
                    AudioSource.PlayClipAtPoint(audioShot, Camera.main.transform.position);
                    bulletsPooler.SpawnShapeB(rightCannon.position, Quaternion.identity);
                }
                yield return new WaitForSeconds(2f);
                pursuerRot = !pursuerRot;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ShootPursuer(Vector2 pos, Quaternion rot)
    {
        AudioSource.PlayClipAtPoint(audioShot, Camera.main.transform.position);
        for (int i = 0; i < 6; i++)
        {
            bulletsPooler.SpawnPursuerB(pos, rot);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator HeartAttack()
    {
        Stop();
        counterToMoveAgain = 0;
        yield return new WaitForSeconds(0.05f);
        if (life > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(miniHearPref, new Vector2(-6, -15), Quaternion.identity);
                Instantiate(miniHearPref, new Vector2(7, -15), Quaternion.identity);
                yield return new WaitForSeconds(3f);
                if (life >= 0)
                    break;
            }
        }
    }

    IEnumerator Heal()
    {
        stopHealAbility = false;
        StartCoroutine(StopHealAbility());
        leftHeart.SetActive(true);
        rightHeart.SetActive(true);
        shieldParticle.SetActive(true);
        healing = true;
        dontShoot = true;
        shieldAudio.Play();
        while (true)
        {
            if (healing)
            {
                healAudio.Play();
                if (life <= (lifeReference - 2))
                life += 5;
            }
            else if (stopHealAbility)
            {
                shieldAudio.Stop();
                shieldParticle.SetActive(false);
                if (actualChargedB != null)
                    actualChargedB.GetComponent<ChargedBullet>().ChargeStarter();
                leftHeart.SetActive(false);
                rightHeart.SetActive(false);
                dontShoot = false;
                StopCoroutine(nameof(StopHealAbility));
                StartCoroutine(BossDisplacement());
                break;
            }
            SetHealth();
            yield return new WaitForSeconds(0.5f);
        }
        dontShoot = false;
    }

    IEnumerator StartingHeartAttack()
    {
        while (true)
        {
            StartCoroutine(HeartAttack());
            yield return new WaitForSeconds(15);
        }
    }

    IEnumerator Entering()
    {
        GetComponent<Collider2D>().enabled = false;
        GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Warning", true);
        yield return new WaitForSeconds(4.5f);
        GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Warning", false);
        warninAudio.Stop();
        while (transform.position.y > 7.5f)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(0, 7.4f), 2 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        GetComponent<Collider2D>().enabled = true;
        StartCoroutine(Shoot());
        StartCoroutine(BossDisplacement());
    }

    IEnumerator GoToCenter()
    {
        while (stopMoving)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(0, transform.position.y), 4 * Time.fixedDeltaTime);

            if (transform.position.x > -0.3f && transform.position.x < 0.3f)
                break;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Heal());
    }

    public void CheckIfStopHeal()
    {
        if (leftHeart.GetComponent<HeartHealer>().life <= 0 && rightHeart.GetComponent<HeartHealer>().life <= 0)
        {
            StopHealing();
        }
    }

    public void CheckIfStopHealAbility()
    {
        if (leftHeart.GetComponent<HeartHealer>().life <= 0 && rightHeart.GetComponent<HeartHealer>().life <= 0)
        {
            stopHealAbility = true;
        }
    }

    IEnumerator StopHealAbility()
    {
        yield return new WaitForSeconds(10f);
        healing = false;
        stopHealAbility = true;
    }

    public void StopHealing()
    {
        healing = false;
    }

    void SetMaxHealth()
    {
        healthBar.maxValue = lifeReference;
        healthBar.value = life;
    }

    public void SetHealth()
    {
        healthBar.value = life;
    }

    public void IncapacitatingBeam()
    {
        AudioSource.PlayClipAtPoint(beanAudio, Camera.main.transform.position);
        Instantiate(rayPref, rayCannon.position, Quaternion.identity);
        GameManager.instance.player.GetComponent<Player>().SilenceStarter();
    }

    public void Stop()
    {
        stopMoving = true;
        rgbd.velocity = Vector3.zero;
    }

    public void Continue()
    {
        stopMoving = false;
        if (transform.position.x < 0)
            rgbd.AddForce(transform.right * velocity * Time.fixedDeltaTime);
        else
            rgbd.AddForce(transform.right * -velocity * Time.fixedDeltaTime);
    }

    public void Death()
    {
        PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + 1000);
        FindObjectOfType<MenuController>().SetPoints();
        healthBar.gameObject.SetActive(false);
        GameManager.instance.winMessage.SetActive(true);
        PlayerPrefs.SetInt("actualLevel", 2);
        gameObject.SetActive(false);
    }

    public IEnumerator BossDisplacement()
    {
        Continue();
        while (!stopMoving)
        {
            if (!GameManager.instance.pause)
            {
                if (transform.position.x >= 3f)
                {
                    rgbd.AddForce(transform.right * -velocity * Time.fixedDeltaTime);
                }
                else if (transform.position.x <= -3f)
                {
                    rgbd.AddForce(transform.right * velocity * Time.fixedDeltaTime);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}