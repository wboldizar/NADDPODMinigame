using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinControl : MonoBehaviour
{
    public EnemyActionController EnemyActionController;
    public EnemyTurnTaker EnemyTurnTaker;
    public EnemyHealthControl EnemyHealthControl;

    //Goblin components
    public Transform goblinTrans;
    public Animator goblinAnim;

    public SortingControl SortingControl;
    public GameObject bodySprites;
    SpriteRenderer[] bodySpriteRenderers;

    //Outside components
    public AuraControl AuraControl;
    public FirewallControl FirewallControl;

    public PlayerHealthController PlayerHealthController;
    public Transform alanisMeleePosition;
    public Transform hardwonMeleePosition;
    public Transform mavrusMeleePosition;

    Vector3 spawnPosition;
    float spawnScale;

    private void Start()
    {
        spawnPosition = goblinTrans.position;
        spawnScale = goblinTrans.localScale.x;
        bodySpriteRenderers = bodySprites.GetComponentsInChildren<SpriteRenderer>();
    }


    //**********Action Coroutines*********\\

    public IEnumerator GoblinClaw()
    {
        bool takeAuraDamage = false;
        if (EnemyActionController.currentTarget.Equals("mavrus"))
        {
            MoveToMavrus();
            if(EnemyActionController.currentEnemy.Equals("backGoblin"))
            {
                SortingControl.ChangeGroupLayer("BetweenMavandHard", bodySpriteRenderers);
            }
            if (EnemyActionController.currentEnemy.Equals("frontGoblin"))
            {
                SortingControl.ChangeGroupLayer("BetweenHardandAlan", bodySpriteRenderers);
            }
            yield return new WaitForSeconds(0.7f);
            if (FirewallControl.fireWallActive)
            { 
                FirewallControl.ActivateFirewall();
                EnemyHealthControl.TakeDamage(20, EnemyActionController.currentEnemy);
            }
            yield return new WaitForSeconds(1.8f);
        }
        else if (EnemyActionController.currentTarget.Equals("hardwon"))
        {
            if(AuraControl.auraActive)
            {
                takeAuraDamage = true;
            }

            MoveToHardwon();
            yield return new WaitForSeconds(0.7f);
            if (FirewallControl.fireWallActive)
            {
                FirewallControl.ActivateFirewall();
                EnemyHealthControl.TakeDamage(18, EnemyActionController.currentEnemy);
            }
            yield return new WaitForSeconds(1.3f);
        }
        else if (EnemyActionController.currentTarget.Equals("alanis"))
        {
            MoveToAlanis();
            yield return new WaitForSeconds(0.7f);
            if (FirewallControl.fireWallActive)
            {
                FirewallControl.ActivateFirewall();
                EnemyHealthControl.TakeDamage(20, EnemyActionController.currentEnemy);
            }
            yield return new WaitForSeconds(1.3f);
        }

        
        goblinAnim.SetBool("GoblinClawState", true);
        yield return new WaitForSeconds(1f);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("goblinClaw");
        yield return new WaitForSeconds(.5f);
        goblinAnim.SetBool("GoblinClawState", false);

        PlayerHealthController.TakeDamage(5, EnemyActionController.currentTarget);
        if(takeAuraDamage)
        {
            EnemyHealthControl.TakeDamage(7, EnemyActionController.currentEnemy);
        }

        if(EnemyHealthControl.activeEnemies.Contains(EnemyActionController.currentEnemy))//weren't killed by aura
        {
            MoveToSpawnPosition();
            yield return new WaitForSeconds(1);
            if (EnemyActionController.currentEnemy.Equals("backGoblin"))
            {
                SortingControl.ChangeGroupLayer("BackGoblin", bodySpriteRenderers);
            }
            if (EnemyActionController.currentEnemy.Equals("frontGoblin"))
            {
                SortingControl.ChangeGroupLayer("FrontGoblin", bodySpriteRenderers);
            }
            yield return new WaitForSeconds(1);
        }
        EnemyTurnTaker.turnTaken = true;
    }


    //**********Move Functions*********\\

    void MoveToSpawnPosition()
    {
        StartCoroutine(WalkOverTime(spawnPosition, spawnScale, 2f));
    }

    void MoveToAlanis()
    {
        StartCoroutine(WalkOverTime(alanisMeleePosition.position, 0.6f, 2f));
    }

    void MoveToHardwon()
    {
        StartCoroutine(WalkOverTime(hardwonMeleePosition.position, 0.4f, 2f));
    }

    void MoveToMavrus()
    {
        StartCoroutine(WalkOverTime(mavrusMeleePosition.position, 0.3f, 2.5f));
    }

    //**********Move Coroutines*********\\

    IEnumerator WalkOverTime(Vector3 endPosition, float endScale, float seconds)
    {
        goblinAnim.SetBool("WalkingState", true);

        StartCoroutine(ScaleOverSeconds(goblinTrans, new Vector3(endScale, endScale, endScale), seconds));
        StartCoroutine(MoveOverSeconds(goblinTrans, endPosition, seconds));
        yield return new WaitForSeconds(seconds);

        goblinAnim.SetBool("WalkingState", false);
    }


    //**********Movement Driving Coroutines*********\\

    IEnumerator MoveOverSeconds(Transform transToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transToMove.position;
        while (elapsedTime < seconds)
        {
            transToMove.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transToMove.position = end;
    }

    IEnumerator ScaleOverSeconds(Transform transToScale, Vector3 scaleEnd, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingScale = transToScale.localScale;
        while (elapsedTime < seconds)
        {
            transToScale.localScale = startingScale * (1 - (elapsedTime / seconds)) + scaleEnd * (elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transToScale.localScale = scaleEnd;
    }
}
