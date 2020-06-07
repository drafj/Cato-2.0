using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public int bulletslength;
    public int eBulletslength;
    public int length127;
    public int pursuerlength;
    public int shooterlength;
    public GameObject[] bulletsPool;
    public GameObject[] eBulletsPool;
    public GameObject[] monster127Pool;
    public GameObject[] pursuerMonsterPool;
    public GameObject[] shooterMonsterPool;

    void Start()
    {
        bulletsPool = new GameObject[bulletslength + 1];
        eBulletsPool = new GameObject[eBulletslength + 1];
        monster127Pool = new GameObject[length127 + 1];
        pursuerMonsterPool = new GameObject[pursuerlength + 1];
        shooterMonsterPool = new GameObject[shooterlength + 1];

        for (int i = 0; i < bulletsPool.Length; i++)
        {
            GameObject temporal = Instantiate(GameManager.instance.bulletPrefab, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            bulletsPool[i] = temporal;
            bulletsPool[i].transform.name = "bulet: " + i;
        }

        for (int i = 0; i < eBulletsPool.Length; i++)
        {
            GameObject temporal = Instantiate(GameManager.instance.bulletPrefab, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            eBulletsPool[i] = temporal;
        }

        for (int i = 0; i < monster127Pool.Length; i++)
        {
            GameObject temporal = Instantiate(GameManager.instance.monster127, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            monster127Pool[i] = temporal;
        }

        for (int i = 0; i < pursuerMonsterPool.Length; i++)
        {
            GameObject temporal = Instantiate(GameManager.instance.pursuerEnemy, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            pursuerMonsterPool[i] = temporal;
        }

        for (int i = 0; i < shooterMonsterPool.Length; i++)
        {
            GameObject temporal = Instantiate(GameManager.instance.shooterMonster, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            shooterMonsterPool[i] = temporal;
            shooterMonsterPool[i].transform.name = "n = " + i;
        }
    }

    public void Spawner(Vector3 pos, Quaternion rot)
    {
        bulletsPool[0].transform.position = pos;
        bulletsPool[0].transform.rotation = rot;
        bulletsPool[0].SetActive(true);
        bulletsPool[0].GetComponent<BulletController>().StartBullet();
        bulletsPool[bulletslength] = bulletsPool[0];

        for (int i = 0; i < bulletslength; i++)
        {
            bulletsPool[i] = bulletsPool[i + 1];
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
}
