using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallControl : MonoBehaviour
{
    public ActionController ActionController;

    //Fireball components
    public Animator wallAnim;
    public SpriteRenderer bottomWallSprites;
    public SpriteRenderer bottomMidWallSprites;
    public SpriteRenderer topMidWallSprites;
    public SpriteRenderer topWallSprites;

    //Outside Components
    public Animator mavrusAnim;

    public bool fireWallActive = false;

    public void SetFirewall()
    {
        StartCoroutine(FirewallSetter());
    }

    public void RemoveFireWall()
    {
        fireWallActive = false;

        bottomWallSprites.enabled = false;
        bottomMidWallSprites.enabled = false;
        topMidWallSprites.enabled = false;
        topWallSprites.enabled = false;
    }

    public void ActivateFirewall()
    {
        StartCoroutine(FirewallActivator());
    }


    //**********Action Coroutines**************\\
    IEnumerator FirewallSetter()
    {
        fireWallActive = true;

        mavrusAnim.SetBool("StaffSpellState", true);//StaffSpell is 3 seconds, raised at 45/60
        yield return new WaitForSeconds(0.75f);//wait for staff raised

        bottomWallSprites.enabled = true;
        bottomMidWallSprites.enabled = true;
        topMidWallSprites.enabled = true;
        topWallSprites.enabled = true;

        yield return new WaitForSeconds(2.25f);//wait for staff lower
        mavrusAnim.SetBool("StaffSpellState", false);
        ActionController.CheckRoundEnd();
    }


    IEnumerator FirewallActivator()
    {
        GameObject.Find("BattleAudioManager").GetComponent<AudioControl>().Play("firewall");
        wallAnim.SetBool("FirewallState", true);//2 second long
        yield return new WaitForSeconds(2f);

        wallAnim.SetBool("FirewallState", false); 
    }

}
