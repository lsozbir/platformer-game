using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseStats : MonoBehaviour
{
    public TMPro.TextMeshProUGUI strText;
    public TMPro.TextMeshProUGUI agiText;


    public void upgradeStats(int stat){

        Debug.Log("working");

        if(PlayerBehaviour.money >= 2){

            if (stat == 1)
                PlayerBehaviour.agility += 1;

            else if (stat == 2)
                PlayerBehaviour.strength += 1;

            updateStatText();

            PlayerBehaviour.money -= 2;
        }
            

    }

    public void updateStatText(){

        strText.text = "Strength: " + PlayerBehaviour.strength.ToString();
        agiText.text = "Agility: " + PlayerBehaviour.agility.ToString();

        PlayerBehaviour.PB.refreshStats();
        PlayerBehaviour.PB.UpdateStats();

    }
}
