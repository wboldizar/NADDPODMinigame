using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AuraControl : MonoBehaviour
{
    public ActionController ActionController;

    //Aura components
    public Animator auraAnim;
    public SpriteRenderer auraSprites;

    public bool auraActive = false;

    //Outside components
    public HardwonMoveControl HardwonMoveControl;
    public ThunderclapControl ThunderclapControl;

    public TextMeshProUGUI hammerDescription;
    public TextMeshProUGUI thunderDescription;


    //**********Action Coroutines**************\\

    public void StartSpiritDads()
    {
        auraActive = true;
        auraSprites.enabled = true;
        auraAnim.SetBool("AuraState", true);
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("auraStart");
        hammerDescription.text = "9 Damage\nOneEnemy";
        thunderDescription.text = "20 Damage\nOneEnemy";
        ThunderclapControl.thunderDamage = 20;
        HardwonMoveControl.meleeDamage = 9;
        ActionController.CheckRoundEnd();
    }

}

