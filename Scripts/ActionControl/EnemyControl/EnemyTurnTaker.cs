using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnTaker : MonoBehaviour
{
    public RoundControl RoundControl;

    public EnemyActionController EnemyActionController;
    public EnemyActionSelector EnemyActionSelector;
    public EnemyTargetSelector EnemyTargetSelector;
    public EnemyHealthControl EnemyHealthControl;

    public GameObject backGoblin;
    public GameObject bevSr;
    public GameObject frontGoblin;

    public bool turnTaken = false;


    public IEnumerator TakeOverallTurn()
    {
        if(EnemyHealthControl.activeEnemies.Contains("backGoblin"))
        {
            EnemyActionController.currentEnemy = "backGoblin";
            TakeGoblinTurn();
            yield return new WaitUntil(() => turnTaken == true);
            turnTaken = false;
            yield return new WaitForSeconds(1);
        }
        if(EnemyHealthControl.activeEnemies.Contains("bevSr"))
        {
            EnemyActionController.currentEnemy = "bevSr";
            TakeBevSrTurn();
            yield return new WaitUntil(() => turnTaken == true);
            turnTaken = false;
            yield return new WaitForSeconds(1);
        }
        if(EnemyHealthControl.activeEnemies.Contains("frontGoblin"))
        {
            EnemyActionController.currentEnemy = "frontGoblin";
            TakeGoblinTurn();
            yield return new WaitUntil(() => turnTaken == true);
            turnTaken = false;
            yield return new WaitForSeconds(1);
        }
        RoundControl.enemyRoundTaken = true;
    }

    void TakeGoblinTurn()
    {
        EnemyActionSelector.SelectGoblinMove();
        EnemyTargetSelector.SelectTarget();
        EnemyActionController.AttackSpecificTarget();
    }

    void TakeBevSrTurn()
    {
        EnemyActionSelector.SelectBevSrMove();

        if(EnemyActionController.currentMove.Equals("swordSlash"))
        {
            EnemyTargetSelector.SelectTarget();
            EnemyActionController.AttackSpecificTarget();
        }
        else//non targeted move (shard or bloodfuel)
        {
            EnemyActionController.BevSrNoTargetAction();
        }
    }
}
