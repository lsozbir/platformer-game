using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevel : MonoBehaviour
{
    public void LevelID(int ID)
    {
        Application.LoadLevel(ID);
    }
}
