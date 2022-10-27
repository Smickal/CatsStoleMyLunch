using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] Animator anim;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            FindObjectOfType<CineMachineFollowSwitcher>().ChangeToGroupCamera();

            if(FindObjectOfType<Enemies>() != null)
                FindObjectOfType<Enemies>().SetToAttackState();

            anim.SetTrigger("TriggerHPBar");
        }
    }
}
