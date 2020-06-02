using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetSelector : MonoBehaviour
{
    public EnemyActionController EnemyActionController;
    public PlayerHealthController PlayerHealthController;

    System.Random randGen = new System.Random();

    public void SelectTarget()
    {
        int numActive = PlayerHealthController.activePlayers.Count;
        int targetNum = randGen.Next(numActive);
        string targetName = (string)PlayerHealthController.activePlayers[targetNum];

        EnemyActionController.currentTarget = targetName;
    }
}
