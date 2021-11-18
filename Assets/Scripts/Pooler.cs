using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject monster127,
        pursuerEnemy,
        shooterMonster;
    public GameObject[] BulletsPref;
    public int bulletslength1;
    public int bulletslength2;
    public int eBulletslength;
    public int length127;
    public int pursuerlength;
    public int shooterlength;
    public GameObject[] bulletsPool1;
    public GameObject[] bulletsPool2;
    public GameObject[] eBulletsPool;
    public GameObject[] monster127Pool;
    public GameObject[] pursuerMonsterPool;
    public GameObject[] shooterMonsterPool;
    public GameObject[] BossRays = new GameObject[4];

    private void Awake()
    {
        eBulletsPool = new GameObject[eBulletslength + 1];
        monster127Pool = new GameObject[length127 + 1];
        pursuerMonsterPool = new GameObject[pursuerlength + 1];
        shooterMonsterPool = new GameObject[shooterlength + 1];

        for (int i = 0; i < eBulletsPool.Length; i++)
        {
            GameObject temporal = Instantiate(BulletsPref[0], transform.position, Quaternion.identity);
            temporal.SetActive(false);
            eBulletsPool[i] = temporal;
        }

        for (int i = 0; i < monster127Pool.Length; i++)
        {
            GameObject temporal = Instantiate(monster127, transform.position, Quaternion.identity);
            temporal.GetComponent<SucideMonster>().StartS();
            temporal.SetActive(false);
            monster127Pool[i] = temporal;
        }

        for (int i = 0; i < pursuerMonsterPool.Length; i++)
        {
            GameObject temporal = Instantiate(pursuerEnemy, transform.position, Quaternion.identity);
            temporal.GetComponent<PursuerMonster>().StarterP();
            temporal.SetActive(false);
            pursuerMonsterPool[i] = temporal;
        }

        for (int i = 0; i < shooterMonsterPool.Length; i++)
        {
            GameObject temporal = Instantiate(shooterMonster, transform.position, Quaternion.identity);
            temporal.GetComponent<ShooterMonster>().StartShooter();
            temporal.SetActive(false);
            shooterMonsterPool[i] = temporal;
            shooterMonsterPool[i].transform.name = "n = " + i;
        }
    }

    public void PrimaryShoot(Vector3 pos, Quaternion rot)
    {
        bulletsPool1[0].transform.position = pos;
        bulletsPool1[0].transform.rotation = rot;
        bulletsPool1[0].SetActive(true);
        bulletsPool1[0].GetComponent<BulletController>().StartBullet();
        bulletsPool1[bulletslength1] = bulletsPool1[0];

        for (int i = 0; i < bulletslength1; i++)
        {
            bulletsPool1[i] = bulletsPool1[i + 1];
        }
    }

    public void SecondaryShoot(Vector3 pos, Quaternion rot)
    {
        bulletsPool2[0].transform.position = pos;
        bulletsPool2[0].transform.rotation = rot;
        bulletsPool2[0].SetActive(true);
        bulletsPool2[0].GetComponent<BulletController>().StartBullet();
        bulletsPool2[bulletslength2] = bulletsPool2[0];

        for (int i = 0; i < bulletslength2; i++)
        {
            bulletsPool2[i] = bulletsPool2[i + 1];
        }
    }

    public void EBSpawner(Vector3 pos, Quaternion rot)
    {
        eBulletsPool[0].transform.position = pos;
        eBulletsPool[0].transform.rotation = rot;
        eBulletsPool[0].SetActive(true);
        eBulletsPool[0].transform.tag = "Enemy Bullet";
        eBulletsPool[0].GetComponent<BulletController>().StartBullet();
        eBulletsPool[eBulletslength] = eBulletsPool[0];

        for (int i = 0; i < eBulletslength; i++)
        {
            eBulletsPool[i] = eBulletsPool[i + 1];
        }
    }

    public void Spawner127(Vector3 pos, Quaternion rot)
    {
        monster127Pool[0].transform.position = pos;
        monster127Pool[0].transform.rotation = rot;
        monster127Pool[0].SetActive(true);
        monster127Pool[0].GetComponent<SucideMonster>().StartS();
        monster127Pool[length127] = monster127Pool[0];

        for (int i = 0; i < length127; i++)
        {
            monster127Pool[i] = monster127Pool[i + 1];
        }
    }

    public void PursuerSpawner(Vector3 pos, Quaternion rot)
    {
        pursuerMonsterPool[0].transform.position = pos;
        pursuerMonsterPool[0].transform.rotation = rot;
        pursuerMonsterPool[0].SetActive(true);
        pursuerMonsterPool[0].GetComponent<PursuerMonster>().StarterP();
        pursuerMonsterPool[pursuerlength] = pursuerMonsterPool[0];

        for (int i = 0; i < pursuerlength; i++)
        {
            pursuerMonsterPool[i] = pursuerMonsterPool[i + 1];
        }
    }

    public void ShooterSpawner(Vector3 pos, Quaternion rot)
    {
        shooterMonsterPool[0].transform.position = pos;
        shooterMonsterPool[0].transform.rotation = rot;
        shooterMonsterPool[0].SetActive(true);
        shooterMonsterPool[0].GetComponent<ShooterMonster>().StartShooter();
        shooterMonsterPool[shooterlength] = shooterMonsterPool[0];

        for (int i = 0; i < shooterlength; i++)
        {
            shooterMonsterPool[i] = shooterMonsterPool[i + 1];
        }
    }

    public void SpawnRay(Vector3 pos, Quaternion rot)
    {
        BossRays[0].transform.position = pos;
        BossRays[0].transform.rotation = rot;
        BossRays[0].SetActive(true);
        BossRays[0].GetComponent<RayController>().velocity = 6;
        BossRays[(BossRays.Length) - 1] = BossRays[0];

        for (int i = 0; i < ((BossRays.Length) - 1); i++)
        {
            BossRays[i] = BossRays[i + 1];
        }
    }
}
