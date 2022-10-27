using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
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
            anim.SetBool("IsWalking", true);
            rb.isKinematic = true;
        }
    }

}
