using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPooler : MonoBehaviour
{
    [SerializeField] private GameObject pursuerBulletPref = null;
    [SerializeField] private int pursuerBulletLenth = 0;
    [SerializeField] private Queue<GameObject> pursuerBullets = new Queue<GameObject>();
    [SerializeField] private GameObject shapeBulletPref = null;
    [SerializeField] private int shapeBulletLenth = 0;
    [SerializeField] private Queue<GameObject> shapeBullets = new Queue<GameObject>();
    [SerializeField] private GameObject minimeBulletPref = null;
    [SerializeField] private int minimeBulletLenth = 0;
    [SerializeField] private Queue<GameObject> minimeBullets = new Queue<GameObject>();

    void Awake()
    {

        for (int i = 0; i < pursuerBulletLenth; i++)
        {
            GameObject temporal = Instantiate(pursuerBulletPref, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            pursuerBullets.Enqueue(temporal);
        }

        for (int i = 0; i < shapeBulletLenth; i++)
        {
            GameObject temporal = Instantiate(shapeBulletPref, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            shapeBullets.Enqueue(temporal);
        }

        for (int i = 0; i < minimeBulletLenth; i++)
        {
            GameObject temporal = Instantiate(minimeBulletPref, transform.position, Quaternion.identity);
            temporal.SetActive(false);
            minimeBullets.Enqueue(temporal);
        }
    }

    public bool QueueFilled()
    {
        if (minimeBullets.Count > 0)
            return true;
        else
            return false;
    }

    public void SpawnPursuerB(Vector3 pos, Quaternion rot)
    {
        if (pursuerBullets.Count > 0)
        {
            GameObject temp = pursuerBullets.Peek();
            if (temp.activeSelf)
                temp.SetActive(false);

            temp.transform.position = pos;
            temp.transform.rotation = rot;
            temp.SetActive(true);
            pursuerBullets.Dequeue();
            pursuerBullets.Enqueue(temp);
        }
    }

    public void SpawnShapeB(Vector3 pos, Quaternion rot)
    {
        GameObject temp = shapeBullets.Peek();
        if (temp.activeSelf)
            temp.SetActive(false);

        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        shapeBullets.Dequeue();
        shapeBullets.Enqueue(temp);
    }

    public void SpawnMinimeB(Vector3 pos, Quaternion rot)
    {
        GameObject temp = minimeBullets.Peek();
        if (temp.activeSelf)
            temp.SetActive(false);

        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        minimeBullets.Dequeue();
        minimeBullets.Enqueue(temp);
    }
}
