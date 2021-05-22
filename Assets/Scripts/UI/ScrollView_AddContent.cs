using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Костылявый скрипт на увиличение размера контента Скрол Вью
/// </summary>
public class ScrollView_AddContent : MonoBehaviour
{
    //Костыль
    [SerializeField] private bool asList = true;
    [SerializeField] private RectTransform Content = null;
    
    private ScrollView thisScrollView;
    private List<GameObject> ContentItems;
    private int ContentHeight;
    public void OnEnable()
    {
        thisScrollView = this.GetComponent<ScrollView>();
    }

    public void AddContent(GameObject ContentObj)
    {
        RectTransform ObjTransform = new RectTransform();

        //Костыль
        if(!asList) return;

        try
        {
            ObjTransform = ContentObj.GetComponent<RectTransform>();
        }
        catch (ArgumentException)
        {
            Debug.Log($"Gabella, где нормальные RectTransform контен в скролвью {this.gameObject.name}");
            return;
        }

        Vector2 AddcontentSize = ObjTransform.rect.size;
        Vector2 scrollConentHeight = thisScrollView.contentRect.size;
        //int 
        
        foreach (var content in ContentItems)
        {
            
            scrollConentHeight.y -= content.GetComponent<RectTransform>().rect.size.y;
        }

        
        scrollConentHeight.y -= AddcontentSize.y;
        scrollConentHeight.y = Mathf.Abs(scrollConentHeight.y); 
        scrollConentHeight.x = thisScrollView.contentRect.x;
        
        Content.sizeDelta += scrollConentHeight;
        
        
    }

    public void RemoveContent(GameObject ContentObj)
    {
        RectTransform ObjTransform = new RectTransform();
        try
        {
            ObjTransform = ContentObj.GetComponent<RectTransform>();
        }
        catch (ArgumentException)
        {
            Debug.Log($"Gabella, где нормальные RectTransform контен в скролвью {this.gameObject.name}");
            return;
        }

        foreach (var content in ContentItems)
        {
            if (content == ContentObj)
            {
                //Костыль
                if(!asList) return;
                
                
            }
        }
        
    }
    public void AddContentSpace()
    {
        //Костыль
        if(!asList) return;
        
       // thisScrollView
    }
}
