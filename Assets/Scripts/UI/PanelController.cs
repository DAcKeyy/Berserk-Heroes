using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject[] Panels;

    public void ShowInformationTextOnPanel(string text, PanelComponent.UserAuthorizationPanels panelType)
    {
        foreach (var panel in Panels)
        {
            PanelComponent panelComponent = panel.GetComponent<PanelComponent>();
            if(panelComponent.PanelType == panelType) panelComponent.ShowTextInformation(text);
        }
    }
    public void DisableAllPanels()
    {
        foreach (var panel in Panels)
        {
            panel.SetActive(false);
        }
    }
    public void DisablePanel(PanelComponent.UserAuthorizationPanels panelType)
    {
        foreach (var panel in Panels)
            if(panel.GetComponent<PanelComponent>().PanelType == panelType) panel.SetActive(false);
        
    }
    public void EnablePanel(PanelComponent.UserAuthorizationPanels panelType)
    {
        DisableAllPanels();
        foreach (var panel in Panels)
            if(panel.GetComponent<PanelComponent>().PanelType == panelType) panel.SetActive(true);
    }
}
