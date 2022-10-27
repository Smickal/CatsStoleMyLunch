using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    [SerializeField] bool isBossLevel = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);

            collision.gameObject.transform.position = spawnPoint.position;

            if(isBossLevel)
            {
                FindObjectOfType<CineMachineFollowSwitcher>().ChangeToPlayerCamera();
            }
        }

        if(collision.tag == "Sandal")
        {
            Destroy(collision.gameObject);
        }


    }
}
