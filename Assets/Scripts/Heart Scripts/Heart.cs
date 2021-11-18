﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Enemies
{
    [SerializeField] private GameObject miniHearPref,
        rayPref,
        chargedBulletPref,
        leftHeart,
        rightHeart,
        shieldParticle;
    [SerializeField] private GameObject[] shields = new GameObject[4];
    [SerializeField] private Transform leftCannon,
        rightCannon,
        rayCannon;
    [SerializeField] private BulletsPooler bulletsPooler;
    [SerializeField] private Rigidbody2D rgbd;
    [SerializeField] private Animator anim;
    [HideInInspector] public int counterToMoveAgain,
        counterToRay;
    private int actualShield = 0;
    private GameObject actualChargedB = null;
    private bool stopMoving,
        pursuerRot,
        dontShoot;
    public bool healing;

    void Start()
    {
        LifeAndVelocityAsigner();
        Continue();
        StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletController bullet))
        {
            if (!healing)
            {
                life -= 5;
                if (life == 40)
                {
                    if (Random.Range(0, 2) == 1)
                    StartCoroutine(Heal());
                }
                if (life <= 0)
                {
                    if (actualShield < 4)
                    {
                        LifeAndVelocityAsigner();
                        shields[actualShield].SetActive(false);
                        actualShield++;
                        if (actualShield < 4)
                        shields[actualShield].SetActive(true);
                        else
                        {
                            StartCoroutine(StartingHeartAttack());
                        }
                    }
                    else
                    {
                        leftCannon.parent.gameObject.SetActive(false);
                        rightCannon.parent.gameObject.SetActive(false);
                        dontShoot = true;
                        Stop();
                        anim.SetTrigger("Death");
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
                bulletsPooler.SpawnShapeB(rightCannon.position, Quaternion.identity);
                yield return new WaitForSeconds(2f);
                pursuerRot = !pursuerRot;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ShootPursuer(Vector2 pos, Quaternion rot)
    {
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
        for (int i = 0; i < 2; i++)
        {
            Instantiate(miniHearPref, new Vector2(-6, -15), Quaternion.identity);
            Instantiate(miniHearPref, new Vector2(7, -15), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator Heal()
    {
        Invoke(nameof(StopHealing), 10);
        Stop();
        leftHeart.SetActive(true);
        rightHeart.SetActive(true);
        shieldParticle.SetActive(true);
        healing = true;
        dontShoot = true;
        while (true)
        {
            if (healing)
            {
                if (life <= 78)
                life += 2;
            }
            else
            {
                leftHeart.SetActive(false);
                rightHeart.SetActive(false);
                shieldParticle.SetActive(false);
                if (actualChargedB != null)
                    actualChargedB.GetComponent<ChargedBullet>().ChargeStarter();
                dontShoot = false;
                Continue();
                break;
            }
            yield return new WaitForSeconds(0.2f);
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

    public void CheckIfStopHeal()
    {
        if (!leftHeart.activeSelf || !rightHeart.activeSelf)
        {
            StopHealing();
        }
    }

    public void StopHealing()
    {
        healing = false;
    }

    public void IncapacitatingBeam()
    {
        Instantiate(rayPref, rayCannon.position, Quaternion.identity);
        GameManager.instance.player.GetComponent<Player>().SilenceStarter();
    }

    public void Stop()
    {
        stopMoving = true;
        rgbd.velocity = Vector3.zero;
    }

    public void Continue(string direction = "right")
    {
        stopMoving = false;
        if (direction == "right")
        rgbd.AddForce(transform.right * velocity * Time.fixedDeltaTime);
        else
        rgbd.AddForce(transform.right * -velocity * Time.fixedDeltaTime);
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!stopMoving)
            BossDisplacement();
    }

    void BossDisplacement()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckIfStopHeal();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ...
        }
    }
}