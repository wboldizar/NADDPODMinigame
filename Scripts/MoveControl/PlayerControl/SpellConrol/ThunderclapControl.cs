using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThunderclapControl : MonoBehaviour
{
    public ActionController ActionController;
    public SortingControl SortingControl;

    //Thunderclap components
    public Transform thunderTailTrans;
    public Transform thunderOrbTrans;
    public Animator thunderAnim;
    public SpriteRenderer thunderTailSprites;
    public SpriteRenderer thunderOrbSprites;
    SpriteRenderer[] thunderSprites = new SpriteRenderer[2];

    public int thunderDamage = 14;


    //External components
    public EnemyHealthControl EnemyHealthControl;
    public Animator hardwonAnim;

    private void Start()
    {
        thunderSprites[0] = thunderTailSprites;
        thunderSprites[1] = thunderOrbSprites;
    }


    //**********Action Coroutines**************\\

    public IEnumerator Thunderclap()
    {
       
        if (ActionController.currentEnemy.Equals("frontGoblin"))
        {
            thunderTailTrans.localPosition = new Vector3(47, -20, 0);
            thunderTailTrans.localEulerAngles = new Vector3(0, 0, 65);
            thunderTailTrans.localScale = new Vector3(37, 70, 1);

            SortingControl.ChangeGroupLayer("FrontofEnemies", thunderSprites);
            thunderOrbTrans.localPosition = new Vector3(90, -27, 0);
            thunderOrbTrans.localScale = new Vector3(70, 70, 1);
        }
        else if (ActionController.currentEnemy.Equals("bevSr"))
        {         
            thunderTailTrans.localPosition = new Vector3(65, -2, 0);
            thunderTailTrans.localEulerAngles = new Vector3(0, 0, 87);
            thunderTailTrans.localScale = new Vector3(35, 87, 1);

            SortingControl.ChangeGroupLayer("BetweenBevandFront", thunderSprites);
            thunderOrbTrans.localPosition = new Vector3(121, 4, 0);
            thunderOrbTrans.localScale = new Vector3(55, 60, 1);
        }
        else if (ActionController.currentEnemy.Equals("backGoblin"))
        {   
            thunderTailTrans.localPosition = new Vector3(38, -5, 0);
            thunderTailTrans.localEulerAngles = new Vector3(0, 0, 80);
            thunderTailTrans.localScale = new Vector3(27, 52, 1);

            SortingControl.ChangeGroupLayer("BetweenBackandBev", thunderSprites);
            thunderOrbTrans.localPosition = new Vector3(73, -4, 0);
            thunderOrbTrans.localScale = new Vector3(50, 50, 1);
        }

        hardwonAnim.SetBool("ThunderclapState", true);

        yield return new WaitForSeconds(1.2f);//wait for hammer punch

        thunderTailSprites.enabled = true;
        thunderOrbSprites.enabled = true;
        thunderAnim.SetBool("ThunderclapState", true);

        yield return new WaitForSeconds(.5f);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("thunderClap");

        yield return new WaitForSeconds(1);//wait for lightning end

        thunderAnim.SetBool("ThunderclapState", false);
        thunderTailSprites.enabled = false;
        thunderOrbSprites.enabled = false;

        EnemyHealthControl.TakeDamage(thunderDamage, ActionController.currentEnemy);

        yield return new WaitForSeconds(.5f);//wait for arm return 

        hardwonAnim.SetBool("ThunderclapState", false);
        ActionController.CheckRoundEnd();
    }
}

