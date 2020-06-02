using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballControl : MonoBehaviour
{
    public ActionController ActionController;

    //Fireball components
    public Animator ballAnim;
    public SpriteRenderer backGoblinBallSprites;
    public SpriteRenderer bevSrBallSprites;
    public SpriteRenderer frontGoblinBallSprites;

    //Outside Components
    public EnemyHealthControl EnemyHealthControl;
    public Animator mavrusAnim;


    //**********Action Coroutines**************\\

    public IEnumerator Fireball()
    {
        mavrusAnim.SetBool("StaffSpellState", true);//StaffSpell is 3 seconds, raised at 45/60

        yield return new WaitForSeconds(0.75f);//wait for staff raised

        backGoblinBallSprites.enabled = true;
        bevSrBallSprites.enabled = true;
        frontGoblinBallSprites.enabled = true;

        ballAnim.SetBool("FireballState", true);//1.5 second long
        yield return new WaitForSeconds(.5f);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("fireball");
        yield return new WaitForSeconds(1f);

        ballAnim.SetBool("FireballState", false);
        backGoblinBallSprites.enabled = false;
        bevSrBallSprites.enabled = false;
        frontGoblinBallSprites.enabled = false;

        ArrayList tempActiveEnemies;//need a temporary since we cannot iterate if an enemy is removed from Collection
        tempActiveEnemies = (ArrayList)EnemyHealthControl.activeEnemies.Clone();

        foreach(string enemyName in tempActiveEnemies)
        {
            EnemyHealthControl.TakeDamage(7, enemyName);
        }

        yield return new WaitForSeconds(.75f);//wait for staff lower
        mavrusAnim.SetBool("StaffSpellState", false);
        ActionController.CheckRoundEnd();
    }
}
