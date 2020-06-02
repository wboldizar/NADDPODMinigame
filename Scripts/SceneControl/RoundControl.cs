using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundControl : MonoBehaviour
{
    //Player Objects & Components
    public GameObject PlayerMenu;
    public Animator menuAnim;
    bool menuEnabled = true;

    public ManaControl ManaControl;
    public int manaCount;
    public bool playerRoundTaken = false;

    public FirewallControl FirewallControl;


    //Enemy Objects & Components
    public EnemyTurnTaker EnemyTurnTaker;
    public bool enemyRoundTaken = false;


    //Endgame Objects & Components
    public GameObject endPanel;
    public GameObject winText;
    public GameObject loseText;

    public void Start()
    {
        Rounds();
    }

    //**********Fucntions**********//

    void Rounds()
    {
        StartCoroutine(RoundTaker());
    }

    public void EndGame(string winOrLose)
    {
        menuEnabled = false;
        PlayerMenu.SetActive(menuEnabled);

        endPanel.SetActive(true);
        if (winOrLose.Equals("win"))
        {
            winText.SetActive(true);
        }
        else if(winOrLose.Equals("lose"))
        {
            loseText.SetActive(true);
        }
    }


    //**********Coroutines*********//
    IEnumerator RoundTaker()
    {
        while(true)
        {
            if(FirewallControl.fireWallActive)
            {
                FirewallControl.RemoveFireWall();
            }
            manaCount = 4;
            ManaControl.ShowMana(manaCount);
            playerRoundTaken = false;
            StartCoroutine(TakePlayerTurn());
            yield return new WaitUntil(() => playerRoundTaken);

            yield return new WaitForSeconds(1);

            if(menuEnabled)//game still active
            {
                enemyRoundTaken = false;
                StartCoroutine(EnemyTurnTaker.TakeOverallTurn());
                yield return new WaitUntil(() => enemyRoundTaken);
            }
        }

    }

    IEnumerator TakePlayerTurn()
    {
        PlayerMenu.SetActive(menuEnabled);
        menuAnim.SetBool("RaiseState", true);       

        yield return new WaitUntil(() => playerRoundTaken);

        menuAnim.SetBool("LowerState", true);
        menuAnim.SetBool("RaiseState", false);
        yield return new WaitForSeconds(1);
        menuAnim.SetBool("LowerState", false);
        PlayerMenu.SetActive(false);
    }

}
