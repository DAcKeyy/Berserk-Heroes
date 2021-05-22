using System.Collections.Generic;
using Game.Card;
using UnityEngine;

public class Deck
{
    private Card _hero;
    private List<Card> _cards;

    public Card Hero
    {
        get => _hero;
    }
    public List<Card> Cards
    {
        get => _cards;
    }
    public bool isComplete
    {
        get
        {
            if (Cards.Count < 40) return false;
            else return true;
        }
    }
    public Deck(Card hero, List<Card> cards)
    {
        if (hero.type != CardType.Hero) { Debug.LogWarning("Ошибка, у колоды должен быть герой"); return; }

        _hero = hero;
        
        if (cards.Count < 40)  { Debug.LogWarning("Ошибка, у колоды должно быть по меншей мере 40 карт"); return; }
        if (Check_For_Element(cards)) { Debug.LogWarning("Ошибка, стихии карт колоды должны совподать со стихией карты Героя"); return; }
        if (Check_For_Repeating(cards)) return; 

        _cards = cards;
    }
    public Deck(Card hero)
    {
        if (hero.type != CardType.Hero) { Debug.LogWarning("Ошибка, у колоды должен быть герой"); return; }
        _hero = hero;
        _cards = new List<Card>();
    }

    public bool Check_For_Element(List<Card> cards)
    {
        foreach (var card in cards)
            if (card.element != Hero.element) return false;
        
        return true;
    }
    public bool Check_For_Repeating(List<Card> cards)
    {
        //провереные карты
        List<uint> checkedCards = new List<uint>();
        //карты с ключевым словом УНИКАЛЬНОСТЬ
        List<Card> uniqueCards = new List<Card>();

        foreach (var card in cards)
        {
            if (checkedCards.Contains(card.id))
            {
                if (card.description.Contains("Уникальность"))
                {
                    if (uniqueCards.Contains(card))
                    {
                        Debug.LogWarning($"Ошибка, у колоды не может быть несколько Уникальных карты, [{card.name}]"); 
                        return false; 
                    }
                }

                List<uint> repeatingCards = checkedCards.FindAll(id => id == card.id);
                if (repeatingCards.Count > 3)
                {
                    if (card.description.Contains("Орда"))
                    {
                        if (repeatingCards.Count > 5)
                        {
                            Debug.LogWarning($"Ошибка, у колоды не может быть карт карт с типом \"Орда\" больше 5, [{card.name}]"); 
                            return false;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Ошибка, у колоды не может быть повторяющихся карт больше чем 3, [{card.name}]");
                        return false;
                    }
                }
            }

            if (card.description.Contains("Уникальность")) uniqueCards.Add(card);

            checkedCards.Add(card.id);
        }
        return true;
    }
    public bool Check_For_Repeating(Card card)
    {
        int repeatingCounter = 0;
        foreach (var arrCard in _cards)
        {
            if (card.id == arrCard.id)
            {
                if (card.description.Contains("Уникальность"))
                {
                    Debug.LogWarning($"Ошибка, у колоды не может быть несколько Уникальных карты, [{card.name}]"); 
                    return false; 
                }

                repeatingCounter++;
            }
        }

        if (repeatingCounter > 3)
        {
            if (card.description.Contains("Орда"))
            {
                if (repeatingCounter > 5)
                {
                    Debug.LogWarning($"Ошибка, у колоды не может быть карт карт с типом \"Орда\" больше 5, [{card.name}]"); 
                    return false;
                }
            }
            else
            {
                Debug.LogWarning($"Ошибка, у колоды не может быть повторяющихся карт больше чем 3, [{card.name}]");
                return false;
            }
        }

        return true;
    }
    public bool AddCard(Card card)
    {
        if (card.element != _hero.element) return false;
        if (Check_For_Repeating(card) is false) return false;
        
        _cards.Add(card);
        return true;
    }
}
