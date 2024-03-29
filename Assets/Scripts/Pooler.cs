﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField]
    private GameObject electro = null,
        tankEnemy = null,
        bomb = null,
        missile = null,
        big = null,
        harpy = null;
    public GameObject[] BulletsPref;
    public int bulletslength1;
    public int bulletslength2;
    public int electroLength;
    public int tankLength;
    public int missileLength;
    public int bombLength;
    public int bigLength;
    public int harpyLength;
    public Queue<GameObject> bulletsPool1 = new Queue<GameObject>();
    public Queue<GameObject> bulletsPool2 = new Queue<GameObject>();
    private Queue<GameObject> electroPool = new Queue<GameObject>();
    private Queue<GameObject> tankPool = new Queue<GameObject>();
    private Queue<GameObject> missilePool = new Queue<GameObject>();
    private Queue<GameObject> bombPool = new Queue<GameObject>();
    private Queue<GameObject> bigPool = new Queue<GameObject>();
    private Queue<GameObject> harpyPool = new Queue<GameObject>();

    private void Awake()
    {
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

        for (int i = 0; i < missileLength; i++)
        {
            GameObject temp = Instantiate(missile, transform.position, Quaternion.identity);
            temp.SetActive(false);
            missilePool.Enqueue(temp);
        }

        for (int i = 0; i < bombLength; i++)
        {
            GameObject temp = Instantiate(bomb, transform.position, Quaternion.identity);
            temp.SetActive(false);
            bombPool.Enqueue(temp);
        }

        for (int i = 0; i < bigLength; i++)
        {
            GameObject temp = Instantiate(big, transform.position, Quaternion.identity);
            temp.SetActive(false);
            bigPool.Enqueue(temp);
        }

        for (int i = 0; i < harpyLength; i++)
        {
            GameObject temp = Instantiate(harpy, transform.position, Quaternion.identity);
            temp.SetActive(false);
            harpyPool.Enqueue(temp);
        }
    }

    public void PrimaryShoot(Vector3 pos, Quaternion rot)
    {
        GameObject temp = bulletsPool1.Peek();

        if (temp.GetComponent<BulletController>().gunz == Gunz.LASER)
            PlayerPrefs.SetInt("Laser", PlayerPrefs.GetInt("Laser", 0) + 1);

        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        temp.GetComponent<BulletController>().ShootSound();
        bulletsPool1.Dequeue();
        bulletsPool1.Enqueue(temp);
    }

    public void SecondaryShoot(Vector3 pos, Quaternion rot)
    {
        GameObject temp = bulletsPool2.Peek();

        if (temp.GetComponent<BulletController>().gunz == Gunz.LASER)
            PlayerPrefs.SetInt("Laser", PlayerPrefs.GetInt("Laser", 0) + 1);

        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        temp.GetComponent<BulletController>().ShootSound();
        bulletsPool2.Dequeue();
        bulletsPool2.Enqueue(temp);
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
        GameObject temp = missilePool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        GameManager.instance.player.GetComponent<Player>().panelAnim.SetBool("Warning", true);
        missilePool.Dequeue();
        missilePool.Enqueue(temp);
    }

    public void BigSpawner(Vector3 pos, Quaternion rot)
    {
        GameObject temp = bigPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        bigPool.Dequeue();
        bigPool.Enqueue(temp);
    }

    public void BombSpawner(Vector3 pos, Quaternion rot, int vel = 5)
    {
        if (bombPool.Count > 0)
        {
            GameObject temp = bombPool.Peek();
            temp.GetComponent<Bomb>().velReference = vel;
            temp.transform.position = pos;
            temp.transform.rotation = rot;
            temp.SetActive(true);
            bombPool.Dequeue();
            bombPool.Enqueue(temp);
        }
    }

    public void HarpySpawner(Vector3 pos, Quaternion rot)
    {
        GameObject temp = harpyPool.Peek();
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        harpyPool.Dequeue();
        harpyPool.Enqueue(temp);
    }
}
