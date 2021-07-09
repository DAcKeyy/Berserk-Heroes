using System;
using System.Collections.Generic;

namespace Game.Deck
{
    [Serializable]
    public struct Deck
    {
        public string name;
        public Card.Card hero;
        public List<Card.Card> cards;
    }
}