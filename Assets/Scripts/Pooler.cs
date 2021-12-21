using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject electro,
        tankEnemy,
        bomb,
        shooterMonster;
    public GameObject[] BulletsPref;
    public int bulletslength1;
    public int bulletslength2;
    public int eBulletslength;
    public int electroLength;
    public int tankLength;
    public int shooterLength;
    public int bombLength;
    public Queue<GameObject> bulletsPool1 = new Queue<GameObject>();
    public Queue<GameObject> bulletsPool2 = new Queue<GameObject>();
    private Queue<GameObject> eBulletsPool = new Queue<GameObject>();
    private Queue<GameObject> electroPool = new Queue<GameObject>();
    private Queue<GameObject> tankPool = new Queue<GameObject>();
    private Queue<GameObject> shooterMonsterPool = new Queue<GameObject>();
    private Queue<GameObject> bombPool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < eBulletslength; i++)
        {
            GameObject temp = Instantiate(BulletsPref[0], transform.position, Quaternion.identity);
            temp.SetActive(false);
            eBulletsPool.Enqueue(temp);
        }

        for (int i = 0; i < electroLength; i++)
        {
            GameObject temp = Instantiate(electro, transform.position, Quaternion.identity);
            temp.SetActive(false);
            electroPool.Enqueue(temp);
        }

        for (int i = 0; i < tankLength; i++)
        {
            GameObject temp = Instantiate(tankEnemy, transform.position, Quaternion.identity);
            temp.SetActive(false);
            tankPool.Enqueue(temp);
        }

        for (int i = 0; i < shooterLength; i++)
        {
            GameObject temp = Instantiate(shooterMonster, transform.position, Quaternion.identity);
            temp.SetActive(false);
            shooterMonsterPool.Enqueue(temp);
        }

        for (int i = 0; i < bombLength; i++)
        {
            GameObject temp = Instantiate(bomb, transform.position, Quaternion.identity);
            temp.SetActive(false);
            bombPool.Enqueue(temp);
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
        GameObject temp = electroPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        electroPool.Dequeue();
        electroPool.Enqueue(temp);
    }

    public void PursuerSpawner(Vector3 pos, Quaternion rot)
    {
        GameObject temp = tankPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        tankPool.Dequeue();
        tankPool.Enqueue(temp);
    }

    public void MissileSpawner(Vector3 pos, Quaternion rot)
    {
        GameObject temp = shooterMonsterPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        shooterMonsterPool.Dequeue();
        shooterMonsterPool.Enqueue(temp);
    }

    public void BombSpawner(Vector3 pos, Quaternion rot)
    {
        GameObject temp = bombPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        bombPool.Dequeue();
        bombPool.Enqueue(temp);
    }
}
