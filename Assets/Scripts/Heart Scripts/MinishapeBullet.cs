using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinishapeBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            gameObject.SetActive(false);
        }
    }
}
