using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string currentLevel;
    TextMeshPro textTMP;
    private void Awake()
    {
        textTMP = GetComponent<TextMeshPro>();
        textTMP.text = "Level " + currentLevel;
    }

}
