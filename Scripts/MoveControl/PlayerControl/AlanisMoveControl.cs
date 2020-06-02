using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlanisMoveControl : MonoBehaviour
{
    public ActionController ActionController;

    //Alanis components
    public Transform alanisTrans;
    public Animator alanisAnim;

    public SortingControl SortingControl;
    public GameObject bodySprites;
    public SpriteRenderer[] bodySpriteRenderers;

    //Outside components
    public PlayerHealthController PlayerHealthController;

    public EnemyHealthControl EnemyHealthControl;
    public Transform frontGoblinMeleeTrans;
    public Transform bevSrMeleeTrans;
    public Transform backGoblinMeleeTrans;

    Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = alanisTrans.position;
        bodySpriteRenderers = bodySprites.GetComponentsInChildren<SpriteRenderer>();
    }


    //**********Action Coroutines*********\\

    public IEnumerator Headbutt()
    {
        if(ActionController.currentEnemy.Equals("frontGoblin"))
        {
            MoveToFrontGoblin();
            yield return new WaitForSeconds(1.5f);
        }
        else if (ActionController.currentEnemy.Equals("bevSr"))
        {
            MoveToBevSr();
            SortingControl.ChangeGroupLayer("BetweenBevandFront", bodySpriteRenderers);
            yield return new WaitForSeconds(1.5f);
        }
        else if (ActionController.currentEnemy.Equals("backGoblin"))
        {
            MoveToBackGoblin();
            yield return new WaitForSeconds(2f);
        }
        

        alanisAnim.SetBool("HeadButtState", true);
        yield return new WaitForSeconds(1f);
        alanisAnim.SetBool("HeadButtState", false);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("headButt");

        EnemyHealthControl.TakeDamage(6, ActionController.currentEnemy);

        MoveToSpawnPosition();
        yield return new WaitForSeconds(2);
        SortingControl.ChangeGroupLayer("Alanis", bodySpriteRenderers);
        ActionController.CheckRoundEnd();
    }

    public IEnumerator HealingSmoke()
    {
        alanisAnim.SetBool("SmokingState", true);
        yield return new WaitForSeconds(.6f);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("healingSmoke");
        yield return new WaitForSeconds(2f);
        alanisAnim.SetBool("SmokingState", false);
        PlayerHealthController.HealAll(12);
        ActionController.CheckRoundEnd();
    }


    //**********Move Functions*********\\

    void MoveToSpawnPosition()
    {
        StartCoroutine(WalkOverTime(spawnPosition, 1.2f, 2f));
    }

    void MoveToFrontGoblin()
    {
        StartCoroutine(WalkOverTime(frontGoblinMeleeTrans.position, 1.2f, 1.5f));
    }

    void MoveToBevSr()
    {
        StartCoroutine(WalkOverTime(bevSrMeleeTrans.position, 1f, 1.5f));
    }

    void MoveToBackGoblin()
    {
        StartCoroutine(WalkOverTime(backGoblinMeleeTrans.position, .9f, 2f));
    }


    //**********Move Coroutines*********\\

    IEnumerator WalkOverTime(Vector3 endPosition, float endScale, float seconds)
    {
        alanisAnim.SetBool("WalkingState", true);

        StartCoroutine(ScaleOverSeconds(alanisTrans, new Vector3(endScale, endScale, endScale), seconds));
        StartCoroutine(MoveOverSeconds(alanisTrans, endPosition, seconds));
        yield return new WaitForSeconds(seconds);

        alanisAnim.SetBool("WalkingState", false);
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
