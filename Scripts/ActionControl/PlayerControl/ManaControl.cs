using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaControl : MonoBehaviour
{
    public GameObject ballOne;
    public GameObject ballTwo;
    public GameObject ballThree;
    public GameObject ballFour;

    GameObject[] manaBalls = new GameObject[4];

    private void Start()
    {
        manaBalls[0] = ballOne;
        manaBalls[1] = ballTwo;
        manaBalls[2] = ballThree;
        manaBalls[3] = ballFour;
    }

    public void ShowMana(int numMana)
    {
        for(int curBall = 0; curBall < manaBalls.Length; curBall++)
        {
            if(curBall < numMana)
            {
                manaBalls[curBall].SetActive(true);
            }
            else
            {
                manaBalls[curBall].SetActive(false);
            }
        }
    }
}
