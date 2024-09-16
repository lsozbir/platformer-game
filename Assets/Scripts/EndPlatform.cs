using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    public int levelID;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player"){
            StartCoroutine(loadLevel());
        }
    }

    IEnumerator loadLevel(){
        yield return new WaitForSeconds(1);

        if(levelID == 6)
        Cursor.visible = true;

        Application.LoadLevel(levelID);
    }
}
