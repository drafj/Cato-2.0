using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BossPush : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (!collision.gameObject.GetComponent<Player>().invencible)
            {
                collision.gameObject.GetComponent<Player>().life--;
                GameManager.instance.StartDealDamage();
            }
            if (collision.gameObject.GetComponent<Player>().life == 0)
            {
                Analytics.CustomEvent("Death", new Dictionary<string, object>
                        {
                            {"death", "Pushed by boss 01"}
                        });
            }
            collision.gameObject.GetComponent<Player>().rgbd.AddForce(transform.up * -5000);
        }
    }
}
