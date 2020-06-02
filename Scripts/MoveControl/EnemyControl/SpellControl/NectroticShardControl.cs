using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectroticShardControl : MonoBehaviour
{
    public EnemyActionController EnemyActionController;
    public EnemyTurnTaker EnemyTurnTaker;

    //Shard components
    public Animator shardAnim;
    public SpriteRenderer leftShardSprites;
    public SpriteRenderer middleShardSprites;
    public SpriteRenderer rightShardSprites;

    //Outside Components
    public Animator bevSrAnim;
    public PlayerHealthController PlayerHealthController;


    //**********Action Coroutines**************\\

    public IEnumerator NecroticShard()
    {
        bevSrAnim.SetBool("NecroticShardState", true);//Total is 270/60 (4.5 seonds), shard key at 95/60

        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("shardArms");
        yield return new WaitForSeconds(1.5f);//wait for sword point

        leftShardSprites.enabled = true;
        middleShardSprites.enabled = true;
        rightShardSprites.enabled = true;

        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("shard");
        shardAnim.SetBool("NecroticShardState", true);//3 seconds long

        yield return new WaitForSeconds(3f);//end of arms and shard
        shardAnim.SetBool("NecroticShardState", false);
        bevSrAnim.SetBool("NecroticShardState", false);

        ArrayList tempActivePlayers;//need a temporary since we cannot iterate if an enemy is removed from Collection
        tempActivePlayers = (ArrayList)PlayerHealthController.activePlayers.Clone();
        foreach (string playerName in tempActivePlayers)
        {
            PlayerHealthController.TakeDamage(15, playerName);
        }

        leftShardSprites.enabled = false;
        middleShardSprites.enabled = false;
        rightShardSprites.enabled = false;
        EnemyTurnTaker.turnTaken = true;
    }
}
