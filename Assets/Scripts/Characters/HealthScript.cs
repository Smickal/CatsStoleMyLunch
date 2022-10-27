using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float healthPoint;
    [SerializeField] HPCounter hpCounter;
    [SerializeField] bool isPlayer = false;
    float currHP;


    private void Start()
    {
        currHP = healthPoint;
    }

    public void TakeDamage(float damage)
    {
        currHP -= damage;
        if(currHP <= 0)
        {
            if (isPlayer)
            {
                gameObject.SetActive(false);
                Time.timeScale = 0f;
            }
            else
                Destroy(gameObject, 0.5f);
            
        }
    }

    public float GetCurrentHP()
    {
        return currHP;
    }

    public float GetMaxHP()
    {
        return healthPoint;
    }

    public void SetCurrentHPToDisplay()
    {
        hpCounter.SetCurrentText(currHP.ToString());
    }
}
