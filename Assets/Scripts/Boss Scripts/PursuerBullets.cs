using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerBullets : MonoBehaviour
{
    Rigidbody2D rb;
    public float velocity,
          rotationVelocity;
    public BulletPState myState;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Invoke("SwitchState", 3);
    }

    public void SwitchState()
    {
        myState = BulletPState.Following;
        //myState = myState == BulletPState.Spinning ? BulletPState.Following : BulletPState.Spinning;
    }

    void JustSpin()
    {
        transform.Rotate(0, 0, 0.2f);
    }

    void FollowTarget()
    {
        Vector3 diff = GameManager.instance.player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), rotationVelocity * Time.deltaTime);
    }

    void Update()
    {
        switch (myState)
        {
            case BulletPState.Spinning:
                JustSpin();
                break;
            case BulletPState.Following:
                if (transform.position.y > GameManager.instance.player.transform.position.y)
                FollowTarget();
                break;
            default:
                break;
        }

        rb.AddForce(transform.up * velocity * Time.deltaTime);
    }
}

public enum BulletPState {Spinning, Following}