using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaHARD : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 30;
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
            float bDamage = 20f;
            PlayerBehaviour.Damage(bDamage);
        }
    }
}
