using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public RoundControl RoundControl;

    public ArrayList activePlayers;

    public GameObject mavrusMulti;
    public GameObject hardwonMulti;
    public GameObject alanisMulti;

    public GameObject mavrusSelect;
    public GameObject hardwonSelect;
    public GameObject alanisSelect;

    public GameObject mavrusActions;
    public GameObject hardwonActions;
    public GameObject alanisActions;


    public Slider mavrusSlider, alanisSlider, hardwonSlider;
    int mavrusHealth, alanisHealth, hardwonHealth;


    private void Start()
    {
        activePlayers = new ArrayList();
        activePlayers.Add("mavrus");
        activePlayers.Add("hardwon");
        activePlayers.Add("alanis");

        mavrusHealth = (int)mavrusSlider.maxValue;
        hardwonHealth = (int)hardwonSlider.maxValue;
        alanisHealth = (int)alanisSlider.maxValue;
    }

    public void KillPlayer(string playerName)
    {
        if(playerName.Equals("mavrus"))
        {
            mavrusHealth = 0;
            activePlayers.Remove("mavrus");
            mavrusSlider.value = mavrusHealth;

            Destroy(mavrusMulti);
            Destroy(mavrusSelect);
            Destroy(mavrusActions);
        }
        else if (playerName.Equals("hardwon"))
        {
            hardwonHealth = 0;
            activePlayers.Remove("hardwon");            
            hardwonSlider.value = hardwonHealth;

            Destroy(hardwonMulti);
            Destroy(hardwonSelect);
            Destroy(hardwonActions);
        }
        else if (playerName.Equals("alanis"))
        {
            alanisHealth = 0;
            activePlayers.Remove("alanis");           
            alanisSlider.value = alanisHealth;

            Destroy(alanisMulti);
            Destroy(alanisSelect);
            Destroy(alanisActions);
        }

        if(activePlayers.Count == 0)//all PC are dead, lose game
        {
            RoundControl.EndGame("lose");
        }
    }


    public void TakeDamage(int damageAmount, string playerName)
    {
        if (playerName.Equals("mavrus"))
        {
            if (damageAmount < mavrusHealth)//non fatal
            {
                mavrusHealth -= damageAmount;
                mavrusSlider.value = mavrusHealth;
            }
            else
            {
                KillPlayer("mavrus");
            }
        }
        else if (playerName.Equals("hardwon"))
        {
            if (damageAmount < hardwonHealth)//non fatal
            {
                hardwonHealth -= damageAmount;
                hardwonSlider.value = hardwonHealth;
            }
            else
            {
                KillPlayer("hardwon");
            }
        }
        else if(playerName.Equals("alanis"))
        {
            if (damageAmount < alanisHealth)//non fatal
            {
                alanisHealth -= damageAmount;
                alanisSlider.value = alanisHealth;
            }
            else
            {
                KillPlayer("alanis");
            }
        }
    }

    public void HealAll(int healAmount)
    {
        if(activePlayers.Contains("mavrus"))
        {
            if(mavrusHealth < (int)mavrusSlider.maxValue - healAmount)
            {
                mavrusHealth += healAmount;
            }
            else
            {
                mavrusHealth = (int)mavrusSlider.maxValue;
            }
            mavrusSlider.value = mavrusHealth;
        }
        if (activePlayers.Contains("hardwon"))
        {
            if (hardwonHealth < (int)hardwonSlider.maxValue - healAmount)
            {
                hardwonHealth += healAmount;
            }
            else
            {
                hardwonHealth = (int)hardwonSlider.maxValue;
            }
            hardwonSlider.value = hardwonHealth;
        }
        if (activePlayers.Contains("alanis"))
        {
            if (alanisHealth < (int)alanisSlider.maxValue - healAmount)
            {
                alanisHealth += healAmount;
            }
            else
            {
                alanisHealth = (int)alanisSlider.maxValue;
            }
            alanisSlider.value = alanisHealth;
        }
    }
}
