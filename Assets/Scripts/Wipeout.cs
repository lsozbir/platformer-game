using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wipeout : MonoBehaviour
{

    public int rotateSpeed;
    public float damage;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Player")
            PlayerBehaviour.Damage(damage * Time.deltaTime);
    }
}
