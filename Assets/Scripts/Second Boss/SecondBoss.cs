using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoss : MonoBehaviour
{
    public GameObject circle;
    public GameObject cameras;
    public GameObject aimCam;
    public GameObject blackSpace;
    public Animator blackSpaceAnim;

    void Start()
    {
        StartCoroutine(CircleCreator());
    }

    IEnumerator CircleCreator()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            cameras.SetActive(false);
            aimCam.SetActive(true);
            GameManager.instance.player.GetComponent<Player>().unleashed = true;
            blackSpaceAnim.SetTrigger("enterBS");
            CreateCircle();
            yield return new WaitForSeconds(27);
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
