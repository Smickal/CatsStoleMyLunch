using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChikin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject chikin;
    [SerializeField] float chikinForce = 100f;
    [SerializeField] Canvas playerCanvas;

    bool isSpawned = false;

    HealthScript hp;

    private void Awake()
    {
        hp = GetComponent<HealthScript>();
    }


    // Update is called once per frame
    void Update()
    {
        if(hp.GetCurrentHP() <= 0 && !isSpawned)
        {
            GameObject chikinSpawn = Instantiate(chikin, transform.position, Quaternion.identity);
            chikinSpawn.GetComponent<Rigidbody2D>().AddForce(chikinForce * Vector3.up, ForceMode2D.Impulse);

            playerCanvas.gameObject.SetActive(true);


            isSpawned = true;
        }
    }
}
