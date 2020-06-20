using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoss : MonoBehaviour
{
    public GameObject circle;
    public GameObject cameras;
    public GameObject aimCam;
    public GameObject blackSpace;

    void Start()
    {
        StartCoroutine(CircleCreator());
    }

    IEnumerator CircleCreator()
    {
        while (true)
        {
            cameras.SetActive(false);
            aimCam.SetActive(true);
            GameManager.instance.player.GetComponent<Player>().unleashed = true;
            blackSpace.SetActive(true);
            CreateCircle();
            yield return new WaitForSeconds(26);
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
