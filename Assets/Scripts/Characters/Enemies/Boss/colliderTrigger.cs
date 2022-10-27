using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTrigger : MonoBehaviour
{
    Enemies enemy;
    CatJumpCollider jumpScript;
    [SerializeField] bool isRight;
    private void Awake()
    {
        enemy = FindObjectOfType<Enemies>();
        jumpScript = GetComponentInParent<CatJumpCollider>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Boss_Enemy")
        {
            //lompat kanan
            if(isRight && collision.gameObject.transform.localScale.x > 0f ||
                !isRight && collision.gameObject.transform.localScale.x < 0f)
            {
                jumpScript.ActivateJump();
            }

        }
    }
}
