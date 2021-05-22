using System;
using Game.Card;
using TMPro;
using UnityEngine;

public class Cards_Collection_ClassField : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    public Action<CardClass> ValueChanged = delegate(CardClass cardClass) {  };

    public void ChangeValueEvent()
    {
        ValueChanged((CardClass) dropdown.value);
    }
}
