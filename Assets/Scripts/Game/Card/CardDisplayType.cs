using System.Collections.Generic;
using UnityEngine;

namespace Game.Card
{
    public class CardDisplayType : MonoBehaviour
    {
        [SerializeField]private SpriteRenderer typeSprite;
        [SerializeField]private SpriteRenderer typeBackground;
        [SerializeField]private List<Sprite> typeSprites;

        public void Reset()
        {
            typeBackground = GetComponent<SpriteRenderer>();
        }

        public void SetType(CardType type)
        {
            if(typeSprites.Count < 7) return;
        
            if (type == CardType.Default) return;
            if (type == CardType.Hero) typeSprite.sprite = typeSprites[0];
            if (type == CardType.Spell) typeSprite.sprite = typeSprites[1];
            if (type == CardType.Event) typeSprite.sprite = typeSprites[2];
            if (type == CardType.Creature) typeSprite.sprite = typeSprites[3];
            if (type == CardType.Necklace) typeSprite.sprite = typeSprites[4];
            if (type == CardType.Armor) typeSprite.sprite = typeSprites[5];
            if (type == CardType.Weapon) typeSprite.sprite = typeSprites[6];
        }
    
        public void SetTypeBackground(CardElement element)
        {
            if(element == CardElement.Default) typeBackground.color = Color.white;
        
            if(element == CardElement.Darkness) typeBackground.color = Color.magenta;
            if(element == CardElement.Forests) typeBackground.color = Color.green;
            if (element == CardElement.Mountains) typeBackground.color = new Color(0f,0.6f,1f,1); //Color.blue;
            if(element == CardElement.Neutral) typeBackground.color = Color.red;
            if(element == CardElement.Steppes) typeBackground.color = Color.yellow;
            if(element == CardElement.Swamps) typeBackground.color = Color.cyan;
        }
    }
}
