using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelector : MonoBehaviour
{
    public ActionController ActionController;


    //*******Alanis Actions********//
    public void Headbutt()
    {
        ActionController.currentMove = "headbutt";
    }

    public void Whirlwind()
    {
        ActionController.currentMove = "whirlwind";
    }

    public void HealingSmoke()
    {
        ActionController.currentMove = "healingSmoke";
        ActionController.HealingSmoke();
    }


    //*******Hardwon Actions*******//
    public void HammerJab()
    {
        ActionController.currentMove = "hammerJab";
    }

    public void Thunderclap()
    {
        ActionController.currentMove = "thunderclap";
    }

    public void SpiritDads()
    {
        ActionController.currentMove = "spiritDads";
        ActionController.SpiritDads();
    }

    //********Mavrus Actions*******//
    public void Firebolt()
    {
        ActionController.currentMove = "firebolt";
    }

    public void Fireball()
    {
        ActionController.currentMove = "fireball";
        ActionController.Fireball();
    }

    public void ActivateFirewall()
    {
        ActionController.currentMove = "firewall";
        ActionController.SetFirewall();
    }
}
