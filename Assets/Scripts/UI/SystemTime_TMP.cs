using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemTime_TMP : MonoBehaviour
{
    private TMP_Text thisText;

    public void OnEnable()
    {
        thisText = this.GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        thisText.text = DateTime.Now.ToString("HH:mm");
    }
}
