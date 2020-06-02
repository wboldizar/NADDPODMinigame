using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthControl : MonoBehaviour
{
    public RoundControl RoundControl;

    public ArrayList activeEnemies;

    public GameObject backGoblinMulti;
    public GameObject bevSrMulti;
    public GameObject frontGoblinMulti;

    public GameObject backGoblinButton;
    public GameObject bevSrButton;
    public GameObject frontGoblinButton;

    public Slider backGoblinSlider, bevSrSlider, frontGoblinSlider;
    int backGoblinHealth, bevSrHealth, frontGoblinHealth;
    

    private void Start()
    {
        activeEnemies = new ArrayList();
        activeEnemies.Add("backGoblin");
        activeEnemies.Add("bevSr");
        activeEnemies.Add("frontGoblin");

        backGoblinHealth = (int)backGoblinSlider.maxValue;
        bevSrHealth = (int)bevSrSlider.maxValue;
        frontGoblinHealth = (int)frontGoblinSlider.maxValue;
    }

    public void KillEnemy(string enemyName)
    {
        if (enemyName.Equals("backGoblin"))
        {
            backGoblinHealth = 0;
            activeEnemies.Remove("backGoblin");
            Destroy(backGoblinMulti);
            Destroy(backGoblinButton);
            backGoblinSlider.value = backGoblinHealth;
        }
        else if (enemyName.Equals("bevSr"))
        {
            bevSrHealth = 0;
            activeEnemies.Remove("bevSr");
            Destroy(bevSrMulti);
            Destroy(bevSrButton);
            bevSrSlider.value = bevSrHealth;
        }
        else if (enemyName.Equals("frontGoblin"))
        {
            frontGoblinHealth = 0;
            activeEnemies.Remove("frontGoblin");
            Destroy(frontGoblinMulti);
            Destroy(frontGoblinButton);
            frontGoblinSlider.value = frontGoblinHealth;
        }

        if (activeEnemies.Count == 0)//all enemies are dead, win game
        {
            RoundControl.EndGame("win");
        }
    }

    public void TakeDamage(int damageAmount, string enemyName)
    {
        if (enemyName.Equals("backGoblin"))
        {
            if (damageAmount < backGoblinHealth)//non fatal
            {
                backGoblinHealth -= damageAmount;
                backGoblinSlider.value = backGoblinHealth;
            }
            else
            {
                KillEnemy("backGoblin");
            }   
        }
        else if (enemyName.Equals("bevSr"))
        {
            if (damageAmount < bevSrHealth)//non fatal
            {
                bevSrHealth -= damageAmount;
                bevSrSlider.value = bevSrHealth;
            }
            else
            {
                KillEnemy("bevSr");
            }
        }
        else if (enemyName.Equals("frontGoblin"))
        {
            if (damageAmount < frontGoblinHealth)//non fatal
            {
                frontGoblinHealth -= damageAmount;
                frontGoblinSlider.value = frontGoblinHealth;
            }
            else
            {
                KillEnemy("frontGoblin");
            }
        }
    }

    public void Heal(int healAmount, string enemyName)
    {
        if (enemyName.Equals("backGoblin"))
        {
            if (backGoblinHealth < (int)backGoblinSlider.maxValue - healAmount)
            {
                backGoblinHealth += healAmount;
            }
            else
            {
                backGoblinHealth = (int)backGoblinSlider.maxValue;
            }
            backGoblinSlider.value = backGoblinHealth;
        }
        else if (enemyName.Equals("frontGoblin"))
        {
            if (frontGoblinHealth < (int)frontGoblinSlider.maxValue - healAmount)
            {
                frontGoblinHealth += healAmount;
            }
            else
            {
                frontGoblinHealth = (int)frontGoblinSlider.maxValue;
            }
            frontGoblinSlider.value = frontGoblinHealth;
        }
    }

}
