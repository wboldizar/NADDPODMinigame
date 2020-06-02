using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireboltControl : MonoBehaviour
{
    public ActionController ActionController;
    public SortingControl SortingControl;

    //Firebolt components
    public Transform boltTrans;
    public Animator boltAnim;
    public SpriteRenderer boltSprites;

    //Outside Components
    public Animator mavrusAnim;

    public EnemyHealthControl EnemyHealthControl;
    public Transform frontGoblinBolt;
    public Transform bevSrBolt;
    public Transform backGoblinBolt;

    Vector3 spawnPosition;
    Vector3 spawnScale;

    void Start()
    {
        spawnPosition = boltTrans.position;
        spawnScale = boltTrans.localScale;
    }

    //**********Action Coroutines**************\\

    public IEnumerator Firebolt()
    {
        boltTrans.position = spawnPosition;
        boltTrans.localScale = spawnScale;

        Vector3 endPosition = new Vector3();
        float endScale = 0;
        if (ActionController.currentEnemy.Equals("frontGoblin"))
        {
            SortingControl.ChangeLayer("FrontofEnemies", boltSprites);
            boltTrans.localEulerAngles = new Vector3(0, 0, 70);
            endPosition = frontGoblinBolt.position;
            endScale = 30;
        }
        else if (ActionController.currentEnemy.Equals("bevSr"))
        {
            SortingControl.ChangeLayer("BetweenBevandFront", boltSprites);
            boltTrans.localEulerAngles = new Vector3(0, 0, 85);
            endPosition = bevSrBolt.position;
            endScale = 25;
        }
        else if (ActionController.currentEnemy.Equals("backGoblin"))
        {
            SortingControl.ChangeLayer("BehindPlayers", boltSprites);
            boltTrans.localEulerAngles = new Vector3(0, 0, 80);
            endPosition = backGoblinBolt.position;
            endScale = 20;
        }

        mavrusAnim.SetBool("FlickOffState", true);//Flick off is 2 seconds, 1 till raised

        yield return new WaitForSeconds(1);//wait for finger raised

        boltSprites.enabled = true;

        boltAnim.SetBool("FireboltState", true);//1 second long
        StartCoroutine(ScaleOverSeconds(boltTrans, new Vector3(endScale, endScale, endScale), 0.75f));
        StartCoroutine(MoveOverSeconds(boltTrans, endPosition, 0.75f));
        yield return new WaitForSeconds(.2f);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("firebolt");
        yield return new WaitForSeconds(.8f);

        boltAnim.SetBool("FireboltState", false);//both animations will finish at same time
        mavrusAnim.SetBool("FlickOffState", false);

        EnemyHealthControl.TakeDamage(7, ActionController.currentEnemy);

        boltSprites.enabled = false;
        boltTrans.position = spawnPosition;
        boltTrans.localScale = spawnScale;
        ActionController.CheckRoundEnd();
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
