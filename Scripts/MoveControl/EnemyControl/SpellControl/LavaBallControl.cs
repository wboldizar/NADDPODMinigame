using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBallControl : MonoBehaviour
{
    public EnemyActionController EnemyActionController;
    public EnemyTurnTaker EnemyTurnTaker;

    //Lavaball components
    public Transform ballTrans;
    public Animator ballAnim;
    public SpriteRenderer ballSprites;

    //Outside Components
    public PlayerHealthController PlayerHealthController;

    public Animator goblinAnim;
    public Transform mavrusLavaBall;
    public Transform hardwonLavaBall;
    public Transform alanisLavaBall;

    Vector3 spawnPosition;
    Vector3 spawnScale;

    void Start()
    {
        spawnPosition = ballTrans.position;
        spawnScale = ballTrans.localScale;
    }

    //**********Action Coroutines**************\\

    public IEnumerator LavaBall()
    {
        ballTrans.position = spawnPosition;
        ballTrans.localScale = spawnScale;

        Vector3 endPosition = new Vector3();
        float endScale = 0;
        if (EnemyActionController.currentTarget.Equals("mavrus"))
        {
            endPosition = mavrusLavaBall.position;
            endScale = 3;
        }
        else if (EnemyActionController.currentTarget.Equals("hardwon"))
        {
            endPosition = hardwonLavaBall.position;
            endScale = 4;
        }
        else if (EnemyActionController.currentTarget.Equals("alanis"))
        {
            endPosition = alanisLavaBall.position;
            endScale = 5;
        }

        goblinAnim.SetBool("LavaBallState", true);//Goes to pot in 35/60, ends at 90

        yield return new WaitForSeconds(.5f);//wait for hand in pot

        ballSprites.enabled = true;

        ballAnim.SetBool("LavaBallState", true);//1.92 second long
        StartCoroutine(ScaleOverSeconds(ballTrans, new Vector3(endScale, endScale, endScale), 1.5f));
        StartCoroutine(MoveOverSeconds(ballTrans, endPosition, 1.5f));

        yield return new WaitForSeconds(0.5f);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("lavaball");
        yield return new WaitForSeconds(0.5f);//end of arms
        goblinAnim.SetBool("LavaBallState", false);


        yield return new WaitForSeconds(.42f);//end of LavaBall
        ballAnim.SetBool("LavaBallState", false);

        PlayerHealthController.TakeDamage(8, EnemyActionController.currentTarget);

        ballSprites.enabled = false;
        ballTrans.position = spawnPosition;
        ballTrans.localScale = spawnScale;
        EnemyTurnTaker.turnTaken = true;
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
