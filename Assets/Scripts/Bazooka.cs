using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 20;
    }

    void Update()
    {
        if (transform.position.y < -5)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {
            float bDamage = 10.0f;
            PlayerBehaviour.Damage(bDamage);
        }
    }
}
