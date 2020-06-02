using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionSelector : MonoBehaviour
{
    public EnemyActionController EnemyActionController;
    public EnemyHealthControl EnemyHealthControl;

    System.Random randGen = new System.Random();

    public void SelectGoblinMove()
    {
        int clawOrThrow = randGen.Next(2);

        if (clawOrThrow == 0)
        {
            EnemyActionController.currentMove = "claw";
        }
        else if (clawOrThrow == 1)
        {
            EnemyActionController.currentMove = "lavaBall";
        }
    }

    public void SelectBevSrMove()
    {
        int moveNumber = randGen.Next(10);

        if (moveNumber < 5)//0 ,1, 2, 3, 4
        {
            EnemyActionController.currentMove = "swordSlash";
        }
        else if(5 <= moveNumber && moveNumber < 8)//5, 6, 7 blodfuel if either are alive, otherwise toss up
        {
            if(EnemyHealthControl.activeEnemies.Contains("frontGoblin") || EnemyHealthControl.activeEnemies.Contains("backGoblin"))
            {
                EnemyActionController.currentMove = "bloodFuel";
            }
            else//no allies alive, do another toss up 
            {
                moveNumber = randGen.Next(3);
                if(moveNumber == 0)
                {
                    EnemyActionController.currentMove = "swordSlash";
                }
                else//1 or 2, therefore more necroticSharding
                {
                    EnemyActionController.currentMove = "necroticShard";
                }
            }
        }
        else//8 or 9
        {
            EnemyActionController.currentMove = "necroticShard";
        }
    }
}
