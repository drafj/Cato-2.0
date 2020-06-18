using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
    private bool finalRoll = false;
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
        Invoke("FinalRollActivator", 4);
    }

    void FinalRollActivator()
    {
        finalRoll = true;
    }

    void Disable()
    {
        finalRoll = false;
        if (transform.GetChild(0) != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).SetParent(null);
        }
        gameObject.SetActive(false);
        transform.position = new Vector2(1000, 1000);
    }

    void Update()
    {
        if (finalRoll)
            transform.rotation = Quaternion.Lerp(transform.rotation, finalQuaternion, 5 * Time.deltaTime);
        else
            transform.Rotate(new Vector3(0, 0, -360), 250 * Time.deltaTime);

        if (transform.eulerAngles.z < (finalQuaternion.eulerAngles.z + 0.01f) && transform.eulerAngles.z > (finalQuaternion.eulerAngles.z + -0.01f) && finalRoll)
        {
            Disable();
        }

        //Debug.Log(transform.eulerAngles + " vs " + finalQuaternion.eulerAngles);
    }
}
