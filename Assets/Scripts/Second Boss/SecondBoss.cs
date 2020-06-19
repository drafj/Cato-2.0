using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoss : MonoBehaviour
{
    public GameObject circle;

    void Start()
    {
        StartCoroutine(CircleCreator());
    }

    IEnumerator CircleCreator()
    {
        while (true)
        {
            CreateCircle();
            yield return new WaitForSeconds(20);
        }
    }

    public void CreateCircle()
    {
        Instantiate(circle, GameManager.instance.player.transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
