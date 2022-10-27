using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemies : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] bool isMenu = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (isMenu)
        {
            anim.SetTrigger("SetToPatrol");
            rb.isKinematic = true;
        }
    }
}
