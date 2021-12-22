using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPooler : MonoBehaviour
{
    [SerializeField] private GameObject pursuerBulletPref;
    [SerializeField] private int pursuerBulletLenth;
    [SerializeField] private Queue<GameObject> pursuerBullets = new Queue<GameObject>();
    [SerializeField] private GameObject shapeBulletPref;
    [SerializeField] private int shapeBulletLenth;
    [SerializeField] private Queue<GameObject> shapeBullets = new Queue<GameObject>();
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
}
