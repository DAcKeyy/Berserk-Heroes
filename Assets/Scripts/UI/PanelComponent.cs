using System;
using TMPro;
using UnityEngine;

public class PanelComponent : MonoBehaviour
{
    public enum UserAuthorizationPanels
    {
        InformationPanel,
        ChoseTypeOpAuntificationPanel,
        UsualAuntificationPanel,
        UsualRegistartionPanel,
        UsualRegistartionNicknamePanel,
        LoadingPanel,
        UsualRestorePasswordPanel
    }
    
    [SerializeField] TMP_Text MessageText;
    [SerializeField] private UserAuthorizationPanels panelType;
    
    public UserAuthorizationPanels PanelType
    {
        get => panelType;
        set => panelType = value;
    }

    public void ShowTextInformation(string text) => MessageText.text = text;
}
