using System.Collections.Generic;
using UnityEngine;

namespace Game.Card
{
    public class CardDisplayRareness : MonoBehaviour
    {
        [SerializeField]private SpriteRenderer rarenessSprite;
        [SerializeField]private List<Sprite> rarenessSprites;

        public void SetRareness(CardRareness rareness)
        {
            if(rarenessSprites.Count < 4) return;
            if (rareness == CardRareness.Default) rarenessSprite.sprite = rarenessSprites[0];
            
            if (rareness == CardRareness.Common) rarenessSprite.sprite = rarenessSprites[0];
            if (rareness == CardRareness.Uncommon) rarenessSprite.sprite = rarenessSprites[1];
            if (rareness == CardRareness.Rare) rarenessSprite.sprite = rarenessSprites[2];
            if (rareness == CardRareness.Ultrarare) rarenessSprite.sprite = rarenessSprites[3];
        }
    }
}
