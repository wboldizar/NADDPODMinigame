using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFuelControl : MonoBehaviour
{
    //Blood components
    public Animator bloodAnim;
    public SpriteRenderer spraySprites;
    public SpriteRenderer leftSplatSprites;
    public SpriteRenderer middleSplatSprites;
    public SpriteRenderer rightSplatSprites;

    //Outside components
    public EnemyActionController EnemyActionController;
    public EnemyTurnTaker EnemyTurnTaker;
    public EnemyHealthControl EnemyHealthControl;
    public BevSrControl BevSrControl;


    //**********Action Coroutines**************\\

    public IEnumerator BloodFuel()
    {
        if(EnemyHealthControl.activeEnemies.Contains("frontGoblin"))
        {
            BevSrControl.MoveToFrontGoblin();
            yield return new WaitForSeconds(1.5f);

            EnableSprites();
            bloodAnim.SetBool("BloodFuelState", true);//63/60
            GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("bloodFuel");
            yield return new WaitForSeconds(1.04f);

            DisableSprites();
            bloodAnim.SetBool("BloodFuelState", false);
            EnemyHealthControl.Heal(10, "frontGoblin");
        }
        if (EnemyHealthControl.activeEnemies.Contains("backGoblin"))
        {
            BevSrControl.MoveToBackGoblin();
            yield return new WaitForSeconds(2f);

            EnableSprites();
            bloodAnim.SetBool("BloodFuelState", true);//63/60
            GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("bloodFuel");
            yield return new WaitForSeconds(1.04f);

            DisableSprites();
            bloodAnim.SetBool("BloodFuelState", false);
            EnemyHealthControl.Heal(10, "backGoblin");
        }

        BevSrControl.MoveToSpawnPosition();
        yield return new WaitForSeconds(2);

        EnemyTurnTaker.turnTaken = true;
    }


    void EnableSprites()
    {
        spraySprites.enabled = true;
        leftSplatSprites.enabled = true;
        middleSplatSprites.enabled = true;
        rightSplatSprites.enabled = true;
    }


    void DisableSprites()
    {
        spraySprites.enabled = false;
        leftSplatSprites.enabled = false;
        middleSplatSprites.enabled = false;
        rightSplatSprites.enabled = false;
    }
}
