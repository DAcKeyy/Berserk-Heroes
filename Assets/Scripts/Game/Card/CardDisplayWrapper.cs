using System.Collections.Generic;
using UnityEngine;

namespace Game.Card
{
    public class CardDisplayWrapper : MonoBehaviour
    {
        [SerializeField]private SpriteRenderer wrapperSprite;
        [SerializeField]private List<Sprite> wrapperSprites;
        
        public void SetWrapper(CardElement element)
        {
            if(wrapperSprites.Count < 6) return;
            
            if (element == CardElement.Default) wrapperSprite.sprite = wrapperSprites[0];
            
            if (element == CardElement.Neutral) wrapperSprite.sprite = wrapperSprites[0];
            if (element == CardElement.Steppes) wrapperSprite.sprite = wrapperSprites[1];
            if (element == CardElement.Mountains) wrapperSprite.sprite = wrapperSprites[2];
            if (element == CardElement.Forests) wrapperSprite.sprite = wrapperSprites[3];
            if (element == CardElement.Swamps) wrapperSprite.sprite = wrapperSprites[4];
            if (element == CardElement.Darkness) wrapperSprite.sprite = wrapperSprites[5];
        }
    }
}
