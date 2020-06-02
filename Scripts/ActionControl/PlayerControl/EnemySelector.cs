using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    public ActionController ActionController;

    public void AttackFrontGoblin()
    {
        ActionController.currentEnemy = "frontGoblin";
        ActionController.AttackSpecificEnemy();
        gameObject.SetActive(false);
    }

    public void AttackBevSr()
    {
        ActionController.currentEnemy = "bevSr";
        ActionController.AttackSpecificEnemy();
        gameObject.SetActive(false);
    }

    public void AttackBackGoblin()
    {
        ActionController.currentEnemy = "backGoblin";
        ActionController.AttackSpecificEnemy();
        gameObject.SetActive(false);
    }
}
