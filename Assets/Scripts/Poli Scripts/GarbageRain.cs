using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageRain : MonoBehaviour
{
    [SerializeField] private GameObject[] garbage = new GameObject[] { };
    [SerializeField] private Queue<GameObject> gQueue = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < garbage.Length; i++)
        {
            GameObject temporal = Instantiate(garbage[i], transform.position, Quaternion.identity);
            temporal.SetActive(false);
            gQueue.Enqueue(temporal);
        }

        StartCoroutine(GarbageSpawner());
    }

    IEnumerator GarbageSpawner()
    {
        while (true)
        {
            SpawnGarbage(new Vector3(Random.Range(-7f, -2.75f), 14.76f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            SpawnGarbage(new Vector3(Random.Range(-2.75f, 2.75f), 14.76f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            SpawnGarbage(new Vector3(Random.Range(2.75f, 7f), 14.76f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }

    void SpawnGarbage(Vector3 pos, Quaternion rot)
    {
        if (gQueue.Count > 0)
        {
            GameObject temp = gQueue.Peek();
            if (temp.activeSelf)
            {
                gQueue.Dequeue();
                gQueue.Enqueue(temp);
            }
            else
            {
                temp.transform.position = pos;
                temp.transform.rotation = rot;
                temp.SetActive(true);
                gQueue.Dequeue();
                gQueue.Enqueue(temp);
            }
        }
    }
}
