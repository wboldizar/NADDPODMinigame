using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{
    //******Action Specifiers
    public static string currentEnemy;
    public static string currentMove;
    public static string currentTarget;

    //******Character Controllers
    public GoblinControl backGoblinControl;
    public BevSrControl BevSrControl;
    public GoblinControl frontGoblinControl;

    //******Spell Controllers******//
    public LavaBallControl frontLavaBallControl;
    public LavaBallControl backLavaBallControl;
    public NectroticShardControl NectroticShardControl;
    public BloodFuelControl BloodFuelControl;


    //*********Shared Targeted Actions********//

    public void AttackSpecificTarget()
    {
        if (currentEnemy.Equals("backGoblin"))
        {
            if (currentMove.Equals("claw"))
            {
                StartCoroutine(backGoblinControl.GoblinClaw());
            }
            else if (currentMove.Equals("lavaBall"))
            {
                StartCoroutine(backLavaBallControl.LavaBall());
            }
        }
        else if(currentEnemy.Equals("bevSr"))
        {
            if (currentMove.Equals("swordSlash"))
            {
                StartCoroutine(BevSrControl.SwordSlash());
            }
        }
        else if (currentEnemy.Equals("frontGoblin"))
        {
            if (currentMove.Equals("claw"))
            {
                StartCoroutine(frontGoblinControl.GoblinClaw());
            }
            else if (currentMove.Equals("lavaBall"))
            {
                StartCoroutine(frontLavaBallControl.LavaBall());
            }
        }
    }

    public void BevSrNoTargetAction()
    {
        if(currentMove.Equals("necroticShard"))
        {
            StartCoroutine(NectroticShardControl.NecroticShard());
        }
        else if(currentMove.Equals("bloodFuel"))
        {
            StartCoroutine(BloodFuelControl.BloodFuel());
        }
    }


}
