using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StopWatch : MonoBehaviour
{
    // Start is called before the first frame update

    float minute;
    float second;
    float miliSecond;

    float currentTime;

    bool isStarting = true;

    [SerializeField] TextMeshProUGUI miliSecText;
    [SerializeField] TextMeshProUGUI secText;
    [SerializeField] TextMeshProUGUI minuteText;

    // Update is called once per frame
    void Update()
    {
        if(isStarting)
            currentTime += Time.deltaTime;


        miliSecond = (int)((currentTime - (int)currentTime) * 100);
        second = (int)currentTime % 60;
        minute = (int)(currentTime / 60) % 60;

        miliSecText.text = miliSecond.ToString();
        secText.text = second.ToString();
        minuteText.text = minute.ToString();
    }

    public void RestartStopwatch()
    {
        currentTime = 0f;
    }

    public void StopStopwatch()
    {
        isStarting = false;
    }

    public void StartStopWatch()
    {
        isStarting = true;
    }

}
