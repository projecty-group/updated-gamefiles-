using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{

    public static TimeController instance; //implement a singleton to allow other scripts to call functions from this script
    public TextMeshProUGUI timeCounter;  //reference to timecounter text gameobject
    //public GameObject timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;
    
    public void Awake()
    {

        instance = this;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        timeCounter.text = "Time: 00:00";
        timerGoing = false;

    }

    public void BeginTimer(){

        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer(){

        timerGoing = false;
    }

    private IEnumerator UpdateTimer(){

        while(timerGoing){

            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }


}
