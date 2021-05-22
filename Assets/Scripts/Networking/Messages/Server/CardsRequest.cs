using Game.Card;

namespace Berserk.Messaging.Messages
{

    /*
      string jsonIgnoreNullValues = JsonConvert.SerializeObject(person, Formatting.Indented, new JsonSerializerSettings
       {
           NullValueHandling = NullValueHandling.Ignore
       });
     */
    class CardsRequest
    {
        public CardType? type;
        public CardClass? @class;
        public CardElement? element;
        public int? cost;
        public string cardName;
        public int RequestID;

        public CardsRequest()
        {   
            type = null;
            @class = null;
            element = null;
            cost = null;
            cardName = null;
            RequestID = 0;
        }

        public CardsRequest(CardType? cardType, CardClass? cardClass, CardElement? cardElement, int? cardCost, string cardName, int requestID)
        {
            type = cardType;
            @class = cardClass;
            element = cardElement;
            cost = cardCost;
            this.cardName = cardName;
            RequestID = requestID;
        }
    }
}