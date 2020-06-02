using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public ActionController ActionController;


    public void SelectAlanis()
    {
        ActionController.currentPlayer = "alanis";
    }

    public void SelectHardwon()
    {
        ActionController.currentPlayer = "hardwon";
    }

    public void SelectMavrus()
    {
        ActionController.currentPlayer = "mavrus";
    }
}
