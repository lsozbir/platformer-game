using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject mus = GameObject.Find("Music");

        if (mus.activeInHierarchy){
            Destroy(mus);
        }
    }
}
