using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider healthSlider;
    [SerializeField] HealthScript healthScript;
    public bool isBosHpBar = false;
    private void Awake()
    {
        if (healthSlider == null) return;
        
        if(!isBosHpBar)
            healthScript = GetComponent<HealthScript>();
        healthSlider.maxValue = healthScript.GetMaxHP();  
    }

    private void Update()
    {
        if (healthSlider == null) return;

        healthSlider.value = healthScript.GetCurrentHP();
    }


}
