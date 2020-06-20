using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
    private bool finalRoll = false;
    private bool justRoll = false;
    private int finalRotation;
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
        finalRotation = Random.Range(30, 331);
        finalQuaternion.eulerAngles = transform.eulerAngles + new Vector3(0, 0, finalRotation);
        StartCoroutine(StartSecuence());
    }

    IEnumerator StartSecuence()
    {
        if (transform.childCount > 0 && transform.GetChild(0).childCount > 0)
            justRoll = true;
        yield return new WaitForSeconds(2);
        justRoll = true;
        yield return new WaitForSeconds(4);
        justRoll = false;
        finalRoll = true;
    }

    void Disable()
    {
        finalRoll = false;
        if (transform.childCount > 0 && transform.GetChild(0).childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).SetParent(null);
        }
        else if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).SetParent(null);
        }
        else
        {
            GameManager.instance.player.GetComponent<Player>().unleashed = false;
            GameManager.instance.Boss.GetComponent<SecondBoss>().cameras.SetActive(true);
            GameManager.instance.Boss.GetComponent<SecondBoss>().aimCam.SetActive(false);
            GameManager.instance.Boss.GetComponent<SecondBoss>().blackSpace.SetActive(false);
        }
        Destroy(gameObject, 1);
    }

    void Update()
    {
        if (finalRoll)
            transform.rotation = Quaternion.Lerp(transform.rotation, finalQuaternion, 7 * Time.deltaTime);
        else if (justRoll)
            transform.Rotate(new Vector3(0, 0, -360), 300 * Time.deltaTime);

        if (transform.eulerAngles.z < (finalQuaternion.eulerAngles.z + 0.01f) && transform.eulerAngles.z > (finalQuaternion.eulerAngles.z + -0.01f) && finalRoll)
        {
            Disable();
        }

        //Debug.Log(transform.eulerAngles + " vs " + finalQuaternion.eulerAngles);
    }
}
