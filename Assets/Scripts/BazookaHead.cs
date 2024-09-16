using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaHead : MonoBehaviour
{
    public GameObject Ammo;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fire());
    }

    IEnumerator fire(){

        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            Instantiate(Ammo, transform.position + transform.forward * 0.5f, transform.rotation);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
