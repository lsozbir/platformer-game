using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
   
    public void restartGame(){
        Application.LoadLevel(1);
    }
}
