using Game.Card;
using System.Collections.Generic;

namespace Berserk.Messaging.Messages
{
    class Cards_Data
    {
        //так как вес сообщения ограничен то массив карт кинету будм предвать страницами
        public int max_page;
        public int page;
        public List<Card> cards;
        public int RequestID;

        public Cards_Data()
        {
            max_page = 0;
            page = 0;
            cards = new List<Card>();
        }

        public Cards_Data(int maxPage, int currentPage, List<Card> listCards, int requestID)
        {
            max_page = maxPage;
            page = currentPage;
            cards = listCards;
            RequestID = requestID;
        }
    }
}