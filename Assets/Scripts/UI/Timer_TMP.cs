using System;
using TMPro;
using UnityEngine;

public class Timer_TMP : MonoBehaviour
{
    TMP_Text TMP_text = null;
    [SerializeField] private bool TimeCountDown = true;
    [SerializeField] private bool ShowSeconds = false;
    [SerializeField] private bool ShowDays = false;

    public static Action TimeOver;

    public int days;
    public int hours;
    public int mins;
    public int seconds;


    private void OnEnable()
    {
        TMP_text = this.GetComponent<TMP_Text>();
        
        if(TimeCountDown) InvokeRepeating("TimeDown", 1f, 1f);
        if(!TimeCountDown) InvokeRepeating("TimeUp", 1f, 1f);
    }


    void TimeDown()
    {
        seconds--;

        if(seconds == -1)
        {
            if (days == 0 && hours == 0 && mins == 0) TimeOver();
            else
            {
                mins--;
                seconds = 59;
            }

            if (mins == -1)
            {
                hours--;
                mins = 59;

                if (hours == -1)
                {
                    days--;
                    hours = 23;
                }
            }
        }

        if(days == 0)
        {
            if(hours == 0)
            {
                if(mins == 0)
                {
                    SetTimeOnText(seconds.ToString("0"));
                    return;
                }

                SetTimeOnText(mins.ToString("00") + ":" + seconds.ToString("00"));
                return;
            }

            SetTimeOnText(hours.ToString("00") + ":" + mins.ToString("00") + ":" + seconds.ToString("00"));
            return;          
        }

        SetTimeOnText(days.ToString("00") + ":" + hours.ToString("00") + ":" + mins.ToString("00") + ":" + seconds.ToString("00"));
    }
    void TimeUp()
    {
        seconds++;

        if(seconds == 60)
        {
            seconds = 0;
            mins++;

            if (mins == 60)
            {
                mins = 0;
                hours++;

                if (hours == 24)
                {
                    hours = 0;
                    days++;
                }
            }
        }
        if(ShowSeconds && ShowDays) 
            SetTimeOnText(
                days.ToString("00") + ":" + 
                hours.ToString("00") + ":" + 
                mins.ToString("00") + ":" + 
                seconds.ToString("00"));
        
        else if(ShowDays) 
            SetTimeOnText(
                hours.ToString("00") + ":" + 
                mins.ToString("00") + ":" + 
                seconds.ToString("00"));
        
        else if(ShowSeconds) 
            SetTimeOnText(
                days.ToString("00") + ":" + 
                hours.ToString("00") + ":" + 
                mins.ToString("00"));
        
        else SetTimeOnText(
            hours.ToString("00") + ":" + 
            mins.ToString("00"));
    }

    void SetTimeOnText(string time)
    {
        TMP_text.text = time;
    }
}
