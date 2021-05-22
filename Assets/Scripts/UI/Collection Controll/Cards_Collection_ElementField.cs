using System;
using System.Collections.Generic;
using Game.Card;
using UnityEngine;
using UnityEngine.UI;

public class Cards_Collection_ElementField : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    public Action<CardElement> ValueChanged = delegate(CardElement cardElement ) {  };
    private int _activeButtton;
    
    public void OnValueChanged(int buttonNumber)
    {

        if (_activeButtton == buttonNumber)
        {
            //buttons[_activeButtton].animator.SetBool("isActive",false);
            _activeButtton = 0;
            
        }
        else
        {
            _activeButtton = buttonNumber;
            for (int iterator = 0; iterator < buttons.Count; iterator++)
            {
                if(iterator == _activeButtton -1) continue;
                
                buttons[iterator].animator.SetBool("isActive",false);
            }
        }

        ValueChanged((CardElement) _activeButtton);
    }
}
