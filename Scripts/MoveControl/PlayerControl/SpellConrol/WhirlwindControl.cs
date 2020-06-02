using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindControl : MonoBehaviour
{
    public ActionController ActionController;
    public SortingControl SortingControl;

    //Whirlwind components
    public Transform whirlTrans;
    public Animator whirlAnim;
    public SpriteRenderer whirlSprites;

    //Outside Components
    public EnemyHealthControl EnemyHealthControl;
    public Transform frontGoblinWhirl;
    public Transform bevSrWhirl;
    public Transform backGoblinWhirl;

    Vector3 spawnPosition;
    Vector3 spawnScale;

    void Start()
    {
        spawnPosition = whirlTrans.position;
        spawnScale = whirlTrans.localScale;
    }

    //**********Action Coroutines**************\\

    public IEnumerator Whirlwind()
    {
        whirlTrans.position = spawnPosition;
        whirlTrans.localScale = spawnScale;

        Vector3 endPosition = new Vector3();
        float endScale = 0;
        if (ActionController.currentEnemy.Equals("frontGoblin"))
        {
            SortingControl.ChangeLayer("FrontofEnemies", whirlSprites);
            endPosition = frontGoblinWhirl.position;
            endScale = 25;
        }
        if (ActionController.currentEnemy.Equals("bevSr"))
        {
            SortingControl.ChangeLayer("BetweenBevandFront", whirlSprites);
            endPosition = bevSrWhirl.position;
            endScale = 20;
        }
        if (ActionController.currentEnemy.Equals("backGoblin"))
        {
            SortingControl.ChangeLayer("BetweenBackandBev", whirlSprites);
            endPosition = backGoblinWhirl.position;
            endScale = 15;
        }

        whirlSprites.enabled = true;

        whirlAnim.SetBool("WhirlingState", true);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("whirlwind");
        StartCoroutine(ScaleOverSeconds(whirlTrans, new Vector3(endScale, endScale, endScale), 1.32f));
        StartCoroutine(MoveOverSeconds(whirlTrans, endPosition, 1.32f));
        yield return new WaitForSeconds(1.32f);
        whirlAnim.SetBool("WhirlingState", false);

        EnemyHealthControl.TakeDamage(15, ActionController.currentEnemy);

        whirlSprites.enabled = false;
        whirlTrans.position = spawnPosition;
        whirlTrans.localScale = spawnScale;

        yield return new WaitForSeconds(.5f);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Stop("whirlwind");
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("whirlHit");
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
