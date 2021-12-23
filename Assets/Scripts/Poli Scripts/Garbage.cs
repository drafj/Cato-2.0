using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Border")
        {
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }
}
