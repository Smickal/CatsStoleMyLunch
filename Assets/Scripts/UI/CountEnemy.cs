using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI countText;

    Enemies[] enemy;
    int maxCount;
    int currentCount = 0;


    private void Awake()
    {
        enemy = FindObjectsOfType<Enemies>();
        maxCount = enemy.Length;
        UpdateText();
    }

    // Update is called once per frame
    public void AddEnemyCount()
    {
        currentCount++;
        UpdateText();
    }

    private void UpdateText()
    {
        countText.text = "(" + currentCount.ToString() + "/" + maxCount.ToString() + ")";
    }

    public bool CheckForEnemyCount()
    {
        if (maxCount == currentCount)
            return true;
        else
            return false;
    }
}
