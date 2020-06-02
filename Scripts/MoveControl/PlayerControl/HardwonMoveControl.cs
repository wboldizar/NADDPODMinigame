using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwonMoveControl : MonoBehaviour
{
    public ActionController ActionController;

    //Hardwon objects and components
    public Transform hardwonTrans;
    public Animator hardwonAnim;

    public SortingControl SortingControl;
    public GameObject bodySprites;
    SpriteRenderer[] bodySpriteRenderers;
    public SpriteRenderer torsoSprite;
    

    //Outside components
    public EnemyHealthControl EnemyHealthControl;
    public Transform frontGoblinMeleeTrans;
    public Transform bevSrMeleeTrans;
    public Transform backGoblinMeleeTrans;


    
    Vector3 spawnPosition;

    public int meleeDamage = 6;

    private void Start()
    {
        spawnPosition = hardwonTrans.position;
        bodySpriteRenderers = bodySprites.GetComponentsInChildren<SpriteRenderer>();
    }


    //**********Action Coroutines*********\\

    public IEnumerator HammerJab()
    {
        if (ActionController.currentEnemy.Equals("frontGoblin"))
        {
            SortingControl.ChangeGroupLayer("FrontofEnemies", bodySpriteRenderers);
            SortingControl.ChangeLayer("BetweenBevandFront", torsoSprite);
            MoveToFrontGoblin();
            yield return new WaitForSeconds(2f);
        }
        else if (ActionController.currentEnemy.Equals("bevSr"))
        {
            SortingControl.ChangeGroupLayer("BetweenBevandFront", bodySpriteRenderers);
            SortingControl.ChangeLayer("BetweenBackandBev", torsoSprite);
            MoveToBevSr();
            yield return new WaitForSeconds(2f);
        }
        else if (ActionController.currentEnemy.Equals("backGoblin"))
        {
            MoveToBackGoblin();
            SortingControl.ChangeGroupLayer("BetweenBackandBev", bodySpriteRenderers);
            SortingControl.ChangeLayer("BehindEnemies", torsoSprite);
            yield return new WaitForSeconds(1.5f);
        }

        hardwonAnim.SetBool("JabState", true);
        yield return new WaitForSeconds((float)1.25);
        hardwonAnim.SetBool("JabState", false);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("hammerJab");

        EnemyHealthControl.TakeDamage(meleeDamage, ActionController.currentEnemy);//starts off at 6, boosted by spirit to 9

        MoveToSpawnPosition();
        yield return new WaitForSeconds(2);
        SortingControl.ChangeGroupLayer("Hardwon", bodySpriteRenderers);
        ActionController.CheckRoundEnd();
    }



    //**********Movement Functions & Coroutines*********\\


    void MoveToSpawnPosition()
    {
        StartCoroutine(WalkOverTime(spawnPosition, 0.6f, 2f));
    }

    void MoveToFrontGoblin()
    {
        StartCoroutine(WalkOverTime(frontGoblinMeleeTrans.position, 0.8f, 2f));
    }

    void MoveToBevSr()
    {
        StartCoroutine(WalkOverTime(bevSrMeleeTrans.position, 0.7f, 2f));
    }

    void MoveToBackGoblin()
    {
        StartCoroutine(WalkOverTime(backGoblinMeleeTrans.position, 0.55f, 1.5f));
    }

    IEnumerator WalkOverTime(Vector3 endPosition, float endScale, float seconds)
    {
        hardwonAnim.SetBool("WalkingState", true);

        StartCoroutine(ScaleOverSeconds(hardwonTrans, new Vector3(endScale, endScale, endScale), seconds));
        StartCoroutine(MoveOverSeconds(hardwonTrans, endPosition, seconds));
        yield return new WaitForSeconds(seconds);

        hardwonAnim.SetBool("WalkingState", false);
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
            transToScale.localScale = startingScale * (1- (elapsedTime / seconds)) + scaleEnd * (elapsedTime/seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transToScale.localScale = scaleEnd;
    }
}
