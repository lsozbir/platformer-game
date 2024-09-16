using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlades : MonoBehaviour
{
    public float rotateSpeed;
    public float damage;
    public bool turnX;
    public bool damageOnHit;

    void Update()
    {
        if(turnX)
        transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0));

        else if (!turnX)
            transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!damageOnHit)
        if (collision.gameObject.name == "Player")
            PlayerBehaviour.Damage(damage * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (damageOnHit)
            if (collision.gameObject.name == "Player")
                PlayerBehaviour.Damage(damage);
    }

}
