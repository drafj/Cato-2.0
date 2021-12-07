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
    public Queue<GameObject> bulletsPool1 = new Queue<GameObject>();
    public Queue<GameObject> bulletsPool2 = new Queue<GameObject>();
    private Queue<GameObject> eBulletsPool = new Queue<GameObject>();
    private Queue<GameObject> monster127Pool = new Queue<GameObject>();
    private Queue<GameObject> pursuerMonsterPool = new Queue<GameObject>();
    private Queue<GameObject> shooterMonsterPool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < eBulletslength; i++)
        {
            GameObject temp = Instantiate(BulletsPref[0], transform.position, Quaternion.identity);
            temp.SetActive(false);
            eBulletsPool.Enqueue(temp);
        }

        for (int i = 0; i < length127; i++)
        {
            GameObject temp = Instantiate(monster127, transform.position, Quaternion.identity);
            temp.SetActive(false);
            monster127Pool.Enqueue(temp);
        }

        for (int i = 0; i < pursuerlength; i++)
        {
            GameObject temp = Instantiate(pursuerEnemy, transform.position, Quaternion.identity);
            temp.SetActive(false);
            pursuerMonsterPool.Enqueue(temp);
        }

        for (int i = 0; i < shooterlength; i++)
        {
            GameObject temp = Instantiate(shooterMonster, transform.position, Quaternion.identity);
            temp.SetActive(false);
            shooterMonsterPool.Enqueue(temp);
        }
    }

    public void PrimaryShoot(Vector3 pos, Quaternion rot)
    {
        GameObject temp = bulletsPool1.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        bulletsPool1.Dequeue();
        bulletsPool1.Enqueue(temp);
    }

    public void SecondaryShoot(Vector3 pos, Quaternion rot)
    {
        GameObject temp = bulletsPool2.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        bulletsPool2.Dequeue();
        bulletsPool2.Enqueue(temp);
    }

    public void EBSpawner(Vector3 pos, Quaternion rot)
    {
        GameObject temp = eBulletsPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        eBulletsPool.Dequeue();
        eBulletsPool.Enqueue(temp);
    }

    public void Spawner127(Vector3 pos, Quaternion rot)
    {
        GameObject temp = monster127Pool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        monster127Pool.Dequeue();
        monster127Pool.Enqueue(temp);
    }

    public void PursuerSpawner(Vector3 pos, Quaternion rot)
    {
        GameObject temp = pursuerMonsterPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        pursuerMonsterPool.Dequeue();
        pursuerMonsterPool.Enqueue(temp);
    }

    public void ShooterSpawner(Vector3 pos, Quaternion rot)
    {
        GameObject temp = shooterMonsterPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        shooterMonsterPool.Dequeue();
        shooterMonsterPool.Enqueue(temp);
    }
}
