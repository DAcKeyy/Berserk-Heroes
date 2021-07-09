using System;
using System.Collections.Generic;
using Berserk.Messaging;
using Berserk.Messaging.Messages;
using Berserk.Networking.Managers;
using Game.Card;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class CollectionOrganizer : MonoBehaviour
    {
        [SerializeField] private Cards_Collection_ClassField collectionFieldClassScript;
        [SerializeField] private Cards_Collection_TypeField collectionFieldTypeScript;
        [SerializeField] private Cards_Collection_ElementField collectionFieldElementScript;
        [SerializeField] private Cards_Collection_SearchField collectionFieldSearchScript;
        [SerializeField] private Cards_Collection_View collectionFieldViewScript;

        public List<Card.Card> cardCollection;
        public List<GameDeck> decks;
        private List<int> downloadedPages = new List<int>();
        private int currentRequestID =0;
        private CardType? _type;
        private CardClass? _class;
        private CardElement? _element;
        private int? _cost;
        private string _cardName;
        public void Start()
        {
            collectionFieldClassScript.ValueChanged += cardClass =>
            {
                _class = cardClass;
                GetCards(_type, _class, _element, _cost, _cardName);
            };
            collectionFieldTypeScript.ValueChanged += cardType =>
            {
                _type = cardType;
                GetCards(_type, _class, _element, _cost, _cardName);
            };
            collectionFieldElementScript.ValueChanged += cardElement =>
            {
                _element = cardElement;
                GetCards(_type, _class, _element, _cost, _cardName);
            };
            collectionFieldSearchScript.ValueChanged += cardSearch =>
            {
                _cardName = cardSearch;
            };
            collectionFieldSearchScript.SearchRequest += s =>
            {
                GetCards(_type, _class, _element, _cost, _cardName);
            };

            ServerManager.ServerManagerInstance.ServerResponseObjectEvent += (OBJ, TYPE) =>
            {
                if (TYPE == typeof(Message))
                {
                    Message mes = OBJ.ToObject<Message>();
                    if (Type.GetType(mes.MessageBody.Type) == typeof(Cards_Data))
                    {
                        Cards_Data DATA = mes.MessageBody.Value.ToObject<Cards_Data>();

                        if (DATA.RequestID == currentRequestID)
                        {
                            if (mes.Text.Contains("cards not founded"))
                            {
                                //logik
                                SetCards(new List<Card.Card>());
                            }
                            if (DATA.max_page == 1) SetCards(DATA.cards);
                        
                            if (DATA.max_page > 1)
                            {
                                if(downloadedPages.Contains(DATA.page)) return;
                                else downloadedPages.Add(DATA.page);
                                if (DATA.page == 1) SetCards(DATA.cards);
                                else AddCards(DATA.cards);
                            }
                        }
                    }
                }
            };

            GetAllCards();
        }

        public void GetCards(CardType? type, CardClass? @class, CardElement? element, int? cost, string cardName)
        {
            //класс
            //елемент
            //тип
            //мана
            //название

            if (type == CardType.Default) type = null;
            if (@class == CardClass.Default) @class = null;
            if (element == CardElement.Default) element = null;
            if (cost == 0) cost = null;
            if (string.IsNullOrEmpty(cardName)) cardName = null;
            
            downloadedPages.Clear();
            currentRequestID = Random.Range(1, 100000);
            ServerManager.ServerManagerInstance.Send_Object_ToServer(
                new Message("Command GetCards", false,(uint)StaticPrefs.UserID, "", 
                JsonMessage.FromValue(
                    new CardsRequest(type, @class, element, cost, cardName, currentRequestID))));
        }

        public void GetAllCards()
        {
            downloadedPages.Clear();
            currentRequestID = Random.Range(1, 100000);
            ServerManager.ServerManagerInstance.Send_Object_ToServer(
                new Message("Command GetCards", false,(uint)StaticPrefs.UserID, "", 
                    JsonMessage.FromValue(
                        new CardsRequest(null, null, null, null, "/all",currentRequestID))));
        }
        public void SetCards(List<Card.Card> cards)
        {
            collectionFieldViewScript.Cards = cards;
        }

        public void AddCards(List<Card.Card> cards)
        {
            collectionFieldViewScript.AddCardListToCurrent(cards);
        }

        public void GetDecks(uint ownerId, string accessToken)
        {
        
        }

        public void SetDeck(uint ownerId, string accessToken, GameDeck gameDeck)
        {
        
        }
    }
}
