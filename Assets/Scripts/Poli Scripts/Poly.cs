using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poly : Enemies
{
    [SerializeField] private List<Vector2> positions = new List<Vector2>();
    void Start()
    {
        MoveBehaviour();
    }

    void MoveBehaviour()
    {

        StartCoroutine(Move(Vector3.zero, Quaternion.identity));
    }

    IEnumerator Move(Vector3 pos, Quaternion rot)
    {
        while (true)
        {
            if (pos.y < 0 && transform.position.y >= -19)
                transform.position = Vector3.Lerp(transform.position, pos, velocity * Time.fixedDeltaTime);
            else if (pos.y < 0)
            {
                MoveBehaviour();
                break;
            }
            else if (pos.y > 0 && transform.position.y <= 18)
                transform.position = Vector3.Lerp(transform.position, pos, velocity * Time.fixedDeltaTime);
            else
            {
                MoveBehaviour();
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void Update()
    {
        
    }
}
