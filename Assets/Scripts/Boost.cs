using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostAmount;

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.name == "Player")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        
            rb.velocity = new Vector3(
                rb.velocity.x,
                boostAmount,
                rb.velocity.z);
        }
    }
}
