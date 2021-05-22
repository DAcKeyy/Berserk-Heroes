using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionText : MonoBehaviour
{
    [SerializeField] private TMP_Text Text = null;

    private void Reset()
    {
        Text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        Text.text = "ver " + Application.version;
    }
}
