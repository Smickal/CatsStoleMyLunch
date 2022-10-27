using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeSandal : MonoBehaviour
{
    Sandal[] allSandal;
    SandalPowerUp[] allPowerUp;
    List<GameObject> sandals = new List<GameObject>();

    Hand hand;

    [SerializeField] float rechargeSandalCooldown = 3f;
    float currentTime = 0f;
    bool isSandalEmpty = false;

    int totalCurrentSandal;

    private void Awake()
    {
        hand = GetComponent<Hand>();
    }


    void Start()
    {
        SearchForSandalInGame();     
    }

    // Update is called once per frame
    void Update()
    {
        SearchForSandalInGame();
        IncreaseSandalByCooldown();
    }

    private void IncreaseSandalByCooldown()
    {
        if (isSandalEmpty)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= rechargeSandalCooldown)
            {
                hand.IncreaseSandalInPocket();
                currentTime = 0;
                isSandalEmpty = false;
            }
        }
    }

    private void SearchForSandalInGame()
    {
        allSandal = FindObjectsOfType<Sandal>();

        foreach (Sandal sandal in allSandal)
        {
            sandals.Add(sandal.GetComponent<GameObject>());
        }

        totalCurrentSandal = sandals.Count + hand.GetSandalAmmo();
        sandals.Clear();

        if (totalCurrentSandal <= 0) isSandalEmpty = true; 

    }
}
