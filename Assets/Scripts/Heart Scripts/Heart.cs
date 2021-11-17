using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Enemies
{
    [SerializeField] private GameObject miniHearPref;
    [SerializeField] private Transform leftCanon;
    [SerializeField] private Transform rightCanon;
    [SerializeField] private BulletsPooler bulletsPooler;
    [SerializeField] private Rigidbody2D rgbd;
    [HideInInspector] public int counterToMoveAgain,
        counterToRay;
    private bool stopMoving,
        pursuerRot,
        dontShoot;

    void Start()
    {
        LifeAndVelocityAsigner();
        //Continue();
        StartCoroutine(Shoot());
        StartCoroutine(HeartAttack());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (!dontShoot)
            {
                if (pursuerRot)
                    StartCoroutine(ShootPursuer(leftCanon.position, Quaternion.Euler(0, 0, 135)));
                else
                    StartCoroutine(ShootPursuer(leftCanon.position, Quaternion.Euler(0, 0, -135)));
                yield return new WaitForSeconds(2f);
                bulletsPooler.SpawnShapeB(rightCanon.position, Quaternion.identity);
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
        for (int i = 0; i < 10; i++)
        {
            life += 5;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void IncapacitatingBeam()
    {
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
        rgbd.AddForce(transform.right * velocity * Time.deltaTime);
        else
        rgbd.AddForce(transform.right * -velocity * Time.deltaTime);
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
            rgbd.AddForce(transform.right * -velocity * Time.deltaTime);
        }
        else if (transform.position.x <= -3f)
        {
            rgbd.AddForce(transform.right * velocity * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Stop();
        }
    }
}