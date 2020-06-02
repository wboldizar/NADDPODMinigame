using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public RoundControl RoundControl;
    public ManaControl ManaControl;

    //******Action Specifiers
    public static string currentPlayer;
    public static string currentMove;
    public static string currentEnemy;

    //******Character Controllers
    public AlanisMoveControl alanisControl;
    public HardwonMoveControl hardwonControl;

    //******Spell Controllers
    public WhirlwindControl WhirlwindControl;
    public ThunderclapControl ThunderclapControl;
    public AuraControl AuraControl;
    public FireboltControl FireboltControl;
    public FireballControl FireballControl;
    public FirewallControl FirewallControl;

    public void CheckRoundEnd()
    {
        if(RoundControl.manaCount == 0)
        {
            RoundControl.playerRoundTaken = true;
        }
    }


    //*********Shared Targeted Actions********//

    public void AttackSpecificEnemy()
    {
        if(currentPlayer.Equals("alanis"))
        {
            if(currentMove.Equals("headbutt") && RoundControl.manaCount >= 1)
            {
                RoundControl.manaCount -= 1;
                ManaControl.ShowMana(RoundControl.manaCount);
                StartCoroutine(alanisControl.Headbutt());
            }
            else if(currentMove.Equals("whirlwind") && RoundControl.manaCount >= 2)
            {
                RoundControl.manaCount -= 2;
                ManaControl.ShowMana(RoundControl.manaCount);
                StartCoroutine(WhirlwindControl.Whirlwind());
            }
        }
        else if(currentPlayer.Equals("hardwon"))
        {
            if(currentMove.Equals("hammerJab") && RoundControl.manaCount >= 1)
            {
                RoundControl.manaCount -= 1;
                ManaControl.ShowMana(RoundControl.manaCount);
                StartCoroutine(hardwonControl.HammerJab());
            }
            else if(currentMove.Equals("thunderclap") && RoundControl.manaCount >= 2)
            {
                RoundControl.manaCount -= 2;
                ManaControl.ShowMana(RoundControl.manaCount);
                StartCoroutine(ThunderclapControl.Thunderclap());
            }
        }
        else if(currentPlayer.Equals("mavrus"))
        {
            if(currentMove.Equals("firebolt") && RoundControl.manaCount >= 1)
            {
                RoundControl.manaCount -= 1;
                ManaControl.ShowMana(RoundControl.manaCount);
                StartCoroutine(FireboltControl.Firebolt());
            }
        }
    }

    //********Alanis General Actions**********//

    public void HealingSmoke()
    {
        if(RoundControl.manaCount >= 3)
        {
            RoundControl.manaCount -= 3;
            ManaControl.ShowMana(RoundControl.manaCount);
            StartCoroutine(alanisControl.HealingSmoke());
        }    
    }

    //*******Hardwon General Actions*********//

    public void SpiritDads()
    {
        if (RoundControl.manaCount >= 3 && !AuraControl.auraActive)
        {
            RoundControl.manaCount -= 3;
            ManaControl.ShowMana(RoundControl.manaCount);
            AuraControl.StartSpiritDads();
        }   
    }

    //*******Mavrus General Actions*********//

    public void Fireball()
    {
        if (RoundControl.manaCount >= 2)
        {
            RoundControl.manaCount -= 2;
            ManaControl.ShowMana(RoundControl.manaCount);
            StartCoroutine(FireballControl.Fireball());
        }
    }

    public void SetFirewall()
    {
        if (RoundControl.manaCount >= 3 && !FirewallControl.fireWallActive)
        {
            RoundControl.manaCount -= 3;
            ManaControl.ShowMana(RoundControl.manaCount);
            FirewallControl.SetFirewall();
        }
    }
}
