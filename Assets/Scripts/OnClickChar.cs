using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickChar : MonoBehaviour
{
    public int ID;

    public void setID(){
        Application.targetFrameRate = -1;
        PlayerBehaviour.attributesSet = false;
        PlayerBehaviour.characterID = ID;
        PlayerBehaviour.strength = 0;
        PlayerBehaviour.agility = 0;
        Application.LoadLevel("Level 1");
    }

}
