using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BevSrControl : MonoBehaviour
{
    public EnemyActionController EnemyActionController;
    public EnemyTurnTaker EnemyTurnTaker;
    public EnemyHealthControl EnemyHealthControl;

    //bevSr components 
    public Transform bevSrTrans;
    public Animator bevSrAnim;

    public SortingControl SortingControl;
    public GameObject bodySprites;
    SpriteRenderer[] bodySpriteRenderers;
    public SpriteRenderer armSprite;
 

    //Outside components
    public PlayerHealthController PlayerHealthController;
    public Transform alanisMeleePosition;
    public Transform hardwonMeleePosition;
    public AuraControl AuraControl;
    public Transform mavrusMeleePosition;
    public FirewallControl FirewallControl;

    public Transform frontGoblinHealPosition;
    public Transform backGoblinHealPosition;

    Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = bevSrTrans.position;
        bodySpriteRenderers = bodySprites.GetComponentsInChildren<SpriteRenderer>();
    }


    //**********Action Coroutines*********\\

    public IEnumerator SwordSlash()
    {
        bool takeAuraDamage = false;
        if (EnemyActionController.currentTarget.Equals("mavrus"))
        {
            MoveToMavrus();
            yield return new WaitForSeconds(0.7f);
            if (FirewallControl.fireWallActive)
            {
                FirewallControl.ActivateFirewall();
                EnemyHealthControl.TakeDamage(20, EnemyActionController.currentEnemy);
            }
            SortingControl.ChangeGroupLayer("BehindPlayers", bodySpriteRenderers);
            SortingControl.ChangeLayer("BetweenMavandHard", armSprite);
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
                EnemyHealthControl.TakeDamage(20, EnemyActionController.currentEnemy);
            }
            SortingControl.ChangeGroupLayer("BetweenMavandHard", bodySpriteRenderers);
            SortingControl.ChangeLayer("BetweenHardandAlan", armSprite);
            yield return new WaitForSeconds(0.8f);
        }
        else if (EnemyActionController.currentTarget.Equals("alanis"))
        {
            MoveToAlanis();
            yield return new WaitForSeconds(0.7f);
            if (FirewallControl.fireWallActive)
            {
                FirewallControl.ActivateFirewall();
                EnemyHealthControl.TakeDamage(18, EnemyActionController.currentEnemy);
            }
            SortingControl.ChangeGroupLayer("BetweenHardandAlan", bodySpriteRenderers);
            SortingControl.ChangeLayer("FrontofPlayers", armSprite);
            yield return new WaitForSeconds(1.3f);
        }

        
        bevSrAnim.SetBool("SwordSlashState", true);//total length of 150/60
        yield return new WaitForSeconds(1.5f);//end of stab at 85/60
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("swordSlash");

        PlayerHealthController.TakeDamage(12, EnemyActionController.currentTarget);
        if (takeAuraDamage)
        {
            EnemyHealthControl.TakeDamage(7, EnemyActionController.currentEnemy);
        }

        if (EnemyHealthControl.activeEnemies.Contains(EnemyActionController.currentEnemy))//wasn't killed by aura
        {
            MoveToSpawnPosition();

            yield return new WaitForSeconds(.7f);
            SortingControl.ChangeGroupLayer("BevSr", bodySpriteRenderers);

            yield return new WaitForSeconds(.3f);//end of slash
            bevSrAnim.SetBool("SwordSlashState", false);

            yield return new WaitForSeconds(1);//end of move to spawn
        }
        
        EnemyTurnTaker.turnTaken = true;
    }


    //**********Move Functions*********\\

    public void MoveToSpawnPosition()
    {
        StartCoroutine(FlyOverTime(spawnPosition, 0.8f, 2f));
    }

    void MoveToAlanis()
    {
        StartCoroutine(FlyOverTime(alanisMeleePosition.position, 0.85f, 2f));
    }

    void MoveToHardwon()
    {
        StartCoroutine(FlyOverTime(hardwonMeleePosition.position, 0.75f, 1.5f));
    }

    void MoveToMavrus()
    {
        StartCoroutine(FlyOverTime(mavrusMeleePosition.position, 0.7f, 2.5f));
    }

    public void MoveToFrontGoblin()
    {
        StartCoroutine(FlyOverTime(frontGoblinHealPosition.position, 0.85f, 1.5f));
    }

    public void MoveToBackGoblin()
    {
        StartCoroutine(FlyOverTime(backGoblinHealPosition.position, 0.75f, 2f));
    }

    //**********Move Coroutines*********\\

    IEnumerator FlyOverTime(Vector3 endPosition, float endScale, float seconds)
    {
        StartCoroutine(ScaleOverSeconds(bevSrTrans, new Vector3(endScale, endScale, endScale), seconds));
        StartCoroutine(MoveOverSeconds(bevSrTrans, endPosition, seconds));
        yield return new WaitForSeconds(seconds);
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
