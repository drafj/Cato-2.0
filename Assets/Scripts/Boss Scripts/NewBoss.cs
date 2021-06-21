using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoss : MonoBehaviour
{
    public Rigidbody2D rgbd;
    public GameObject PursuerBulletPref;
    public float velocity,
          cadence;
    bool shooting;
    displacement m_displacement;

    void Start()
    {
        cadence = cadence == 0 ? 0.1f : cadence;
        StartCoroutine(Shoot());
    }

    void BossDisplacement()
    {
        if (transform.position.y <= 9.5f)
        {
            shooting = true;
            if (transform.position.x >= 5.18f)
            {
                m_displacement = displacement.L;
            }
            else if (transform.position.x <= -5.18f)
            {
                m_displacement = displacement.R;
            }

            if (m_displacement == displacement.R)
            {
                rgbd.AddForce(transform.right * (velocity + 15));
            }
            else if (m_displacement == displacement.L)
            {
                rgbd.AddForce(transform.right * (velocity - 15));
            }
        }
        else
            rgbd.AddForce(transform.up * (velocity - 15));
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (shooting)
            {
                GameObject R = Instantiate(PursuerBulletPref, transform.GetChild(0).position, Quaternion.Euler(0, 0, -236));
                yield return new WaitForSeconds(cadence);
            }
            yield return null;
        }
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        BossDisplacement();
    }
}
