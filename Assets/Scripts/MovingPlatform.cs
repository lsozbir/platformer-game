using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float amtToMoveX, amtToMoveY, amtToMoveZ, moveSpeed;
    Vector3 position;
    Vector3 playerPos;
    Rigidbody rb;

    void Start()
    {
        position = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

         transform.position = Vector3.Lerp (transform.position, new Vector3(position.x + Mathf.Sin(Time.time * moveSpeed) * amtToMoveX,
                                                                            position.y + Mathf.Sin(Time.time * moveSpeed) * amtToMoveY,
                                                                            position.z + Mathf.Sin(Time.time * moveSpeed) * amtToMoveZ), 5.0f);


    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
            other.transform.parent = transform;
    }

    private void OnCollisionExit(Collision other)
    {

        if (other.gameObject.name == "Player")
            other.transform.parent = null;

    }


}