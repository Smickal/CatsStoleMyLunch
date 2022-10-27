using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SandalCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Hand hand;
    [SerializeField] TextMeshProUGUI sandalCountText;
    void Start()
    {
        hand = FindObjectOfType<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplaySandalCounter();
    }

    void DisplaySandalCounter()
    {
        sandalCountText.text = hand.GetSandalLeftInPocket().ToString();
    }
}
