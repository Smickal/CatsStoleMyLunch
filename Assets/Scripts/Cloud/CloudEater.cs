using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject);
        Destroy(collision.gameObject);
    }
}
