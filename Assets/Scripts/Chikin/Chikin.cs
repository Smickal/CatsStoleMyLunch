using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chikin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<SceneManagement>().LoadNextScene();
            Destroy(gameObject);
        }
    }
}
