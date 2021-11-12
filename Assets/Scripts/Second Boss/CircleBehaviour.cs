using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CircleBehaviour : MonoBehaviour
{
    private bool finalRoll = false;
    private bool justRoll = false;
    private int finalRotation;
    private float scaleVel;
    private Vector3 oficialFR;
    [SerializeField] private Quaternion finalQuaternion;

    private void Awake()
    {

    }

    void Start()
    {
        //finalRotation = Random.Range(30, 331);
    }

    private void OnEnable()
    {
        finalRotation = Random.Range(90, 271);
        finalQuaternion.eulerAngles = /*new Vector3(0, 0, 0) + */new Vector3(0, 0, finalRotation);
        StartCoroutine(StartSecuence());
    }

    IEnumerator StartSecuence()
    {
        if (transform.childCount > 0 && transform.GetChild(0).childCount > 0)
            justRoll = true;
        yield return new WaitForSeconds(0.7f);
        justRoll = true;
        yield return new WaitForSeconds(4);
        justRoll = false;
        finalRoll = true;
    }

    IEnumerator Redux()
    {
        while (transform.localScale.x > 0.15f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 1), scaleVel * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    void UnleashPlayer()
    {
        GameManager.instance.Boss.GetComponent<SecondBoss>().cameras.SetActive(true);
        GameManager.instance.Boss.GetComponent<SecondBoss>().aimCam.SetActive(false);
        //GameManager.instance.player.GetComponent<Player>().unleashed = false;
        GameManager.instance.Boss.GetComponent<SecondBoss>().blackSpaceAnim.SetTrigger("leaveBS");
    }

    void Disable()
    {
        if (transform.childCount > 0 && transform.GetChild(0).childCount > 0)
        {
            scaleVel = 0.7f;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).SetParent(null);
        }
        else if (transform.childCount > 0)
        {
            scaleVel = 0.7f;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).SetParent(null);
        }
        else
        {
            scaleVel = 0.5f;
            Invoke("UnleashPlayer", 1.4f);
        }
        StartCoroutine(Redux());
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (collision.gameObject.GetComponent<Player>().life > 0 && !collision.gameObject.GetComponent<Player>().invencible)
            {
                collision.gameObject.GetComponent<Player>().life -= 5;
                GameManager.instance.StartDealDamage();
            }

            else if (collision.gameObject.GetComponent<Player>().life <= 0)
            {
                Analytics.CustomEvent("Death", new Dictionary<string, object>
                {
                    {"death", "by second boss"}
                });
            }
        }
    } */
    void Update()
    {
        if (finalRoll)
            transform.rotation = Quaternion.Lerp(transform.rotation, finalQuaternion, 7 * Time.deltaTime);
        else if (justRoll)
            transform.Rotate(new Vector3(0, 0, -360), 450 * Time.deltaTime);

        if (transform.eulerAngles.z < (finalQuaternion.eulerAngles.z + 0.01f) && transform.eulerAngles.z > (finalQuaternion.eulerAngles.z + -0.01f) && finalRoll)
        {
            finalRoll = false;
            Disable();
        }

        //Debug.Log(transform.eulerAngles + " vs " + finalQuaternion.eulerAngles);
    }
}
