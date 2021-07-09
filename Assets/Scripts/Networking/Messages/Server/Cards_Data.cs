using Game.Card;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;

namespace Berserk.Messaging.Messages
{
    class Cards_Data
    {
        //так как вес сообщения ограничен то массив карт кинету будм предвать страницами
        public int max_page { get; set; }
        public int page { get; set; }
        public int RequestID { get; set; }
        public List<Card> cards { get; set; }

        public Cards_Data()
        {
            max_page = 0;
            page = 0;
            cards = new List<Card>();
            RequestID = 0;
        }

        public Cards_Data(int maxPage, int currentPage, List<Card> listCards, int requestID)
        {
            max_page = maxPage;
            page = currentPage;
            cards = listCards;
            RequestID = requestID;
        }
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            errorContext.Handled = true;
        }
        
    }
}