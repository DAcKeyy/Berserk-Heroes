using System;
using System.Collections.Generic;
using Game;
using Game.Card;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Cards_Collection_View : MonoBehaviour
{
    [SerializeField] private List<CardDisplay> cardDisplays;
    [SerializeField] private TMP_Text currentPageText;
    [SerializeField] private TMP_Text maxPageText;
    [SerializeField] private Button PrevPageButton;
    [SerializeField] private Button NextPageButton;
    private List<List<Card>> pagesList = new List<List<Card>>();
    [SerializeField]private List<Card> _cards = new List<Card>();
    private int _currentPage;
    public List<Card> Cards
    {
        get => _cards;
        set
        {
            _cards = value;
            pagesList = SplitList(value,8);
            maxPageText.text = pagesList.Count.ToString();
            ShowPage(1);
        }
    }
    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if(value < 1) return;
            _currentPage = value;
            currentPageText.text = value.ToString();
        }
    }

    public void OnEnable()
    {
        CurrentPage = 1;    
        ShowPage(0);
    }

    public void AddCardListToCurrent(List<Card> addCards)
    {
        _cards.AddRange(addCards);
        pagesList = SplitList(_cards,8);
        maxPageText.text = pagesList.Count.ToString();
        ShowPage(_currentPage);
    }
    
    public void ShowPage(int page)
    {
        if (_cards.Count == 0)
        {
            PrevPageButton.gameObject.SetActive(false);
            NextPageButton.gameObject.SetActive(false);
            maxPageText.text = "1";
            foreach (var cardDisplay in cardDisplays)
            {   
                cardDisplay.gameObject.SetActive(false);
            }
            return;
        }
        
        if(page < 1|| page > pagesList.Count) return;

        if(page == 1) PrevPageButton.gameObject.SetActive(false);
        else PrevPageButton.gameObject.SetActive(true);
        
        if(page == pagesList.Count) NextPageButton.gameObject.SetActive(false);
        else NextPageButton.gameObject.SetActive(true);
        
        CurrentPage = page;

        for (int i = 0; i < cardDisplays.Count; i++)
        { 
            //Debug.Log($"cardDisplays.Count{cardDisplays.Count} , pagesList.cOUNT{pagesList.Count} ,pagesList[0].Count {pagesList[0].Count}, pagesList[page-1][{i}] {pagesList[page-1][i]}");
            cardDisplays[i].gameObject.SetActive(true);
            if (i < pagesList[page - 1].Count)
            {
                Debug.Log($"i:{i} , pagesList[page - 1].Count {pagesList[page - 1].Count} , pagesList[page-1][i] {pagesList[page-1][i]}");
                cardDisplays[i].InitCard(pagesList[page-1][i]);
            }
            else cardDisplays[i].gameObject.SetActive(false);
        }
    }
    
    public void ShowNextPage() => ShowPage(CurrentPage + 1);
    public void ShowPrevPage() => ShowPage(CurrentPage - 1);
    
    private List<List<Card>> SplitList(List<Card> locations, int nSize = 8)
    {
        var list = new List<List<Card>>();

        for (int i = 0; i < locations.Count; i += nSize)
        {
            list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
        }

        return list;
    }
}
[CustomEditor(typeof(Cards_Collection_View))]
public class Cards_Collection_ViewEditor : Editor
{
    //скрипт для отображении кнопки "Инициализировать данные" для провекрки работы карты в редакторе
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Cards_Collection_View myScript = (Cards_Collection_View)target;
        
        if(GUILayout.Button("Инициализировать массив")) myScript.Cards = myScript.Cards;
        if(GUILayout.Button("Пред страница")) myScript.ShowPrevPage();
        if(GUILayout.Button("След страница")) myScript.ShowNextPage();

    }
}