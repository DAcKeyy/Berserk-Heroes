using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Cards_Collection_SearchField : MonoBehaviour
{
    public Action<string> ValueChanged = delegate(string SerachValue ) {  };
    public Action<string> SearchRequest = delegate(string SerachValue ) {  };
    //[SerializeField] private TMP_InputField tmpInputField;

    public void Search(TMP_InputField value)
    {
        SearchRequest(value.text);
    }

    public void ChageValue(TMP_InputField value)
    {
        ValueChanged(value.text);
    }
}
