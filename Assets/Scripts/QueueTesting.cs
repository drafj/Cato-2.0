using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTesting : MonoBehaviour
{
    private Queue<GameObject> electroQ = new Queue<GameObject>();
    [SerializeField] private GameObject electroPref;
    [SerializeField] private int electroRange;

    void Start()
    {
        for (int i = 0; i < electroRange; i++)
        {
            GameObject temp = Instantiate(electroPref, new Vector3(1000, 1000, 0), Quaternion.identity);
            temp.name = $"electro {i}";
            temp.SetActive(false);
            electroQ.Enqueue(temp);
        }
    }

    public void SpawnElectro()
    {
        GameObject temp = electroQ.Peek();
        temp.transform.position = Vector3.zero;
        temp.SetActive(true);
        electroQ.Dequeue();
        electroQ.Enqueue(temp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnElectro();
        }
    }
}
