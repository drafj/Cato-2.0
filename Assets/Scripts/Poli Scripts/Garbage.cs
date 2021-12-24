using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * -100 * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Border")
        {
            transform.position = new Vector3(1000, 1000);
            gameObject.SetActive(false);
        }
    }
}
