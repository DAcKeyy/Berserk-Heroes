using System;

namespace Game.Card
{
    public enum CardType
    {
        Default,
        Hero,
        Spell,
        Event,
        Creature,
        Necklace,
        Armor,
        Weapon
    }
    public enum CardElement
    {
        Default,
        Neutral,
        Steppes,
        Mountains,
        Forests,
        Swamps,
        Darkness
    }
    public enum CardRareness
    {
        Default,
        Common,
        Uncommon,
        Rare,
        Ultrarare
    }
    public enum CardRelease
    {
        Default,
        Strength_and_Honor
    }
    public enum CardClass
    {
        Default,
        Potion,
        Hero_Warrior,
        Angel,
        Arkhaalit,
        Hero_Mage,
        Coyard,
        Toa_Dan,
        Koven,
        Slua,
        Child_of_Krong,
        Guardian_of_the_forest,
        Akkeniyets,
        Troll,
        Pirate,
        Armor,
        Weapon,
        Linung,
        Gnome,
        Mercenary,
        Yordling,
        Inquisitor,
        Elf,
        Demon,
        Wile,
        Orc,
        Draconid,
        Event,
        Artifact,
        River_Maiden,
        Offspring,
        Undergrounder,
        Devastated,
        Beast,
        Terrain,
        Battle_Machine
    }

    [Serializable]
    public struct Card
    {
        public uint id;
        public string name;
        public CardType type;
        public CardElement element;
        public CardRareness rareness;
        public CardClass card_class;
        public string description;
        public string quotation;
        public CardRelease card_release;
        public int cost;
        public int health;
        public int attack;
        public int release_number;
        public int releaseCardsAmount;
        public string image;

        public Card NewCard()
        {
            Card card = new Card();
            
            card.id = 0;
            card.name = "";
            card.type = CardType.Default;
            card.element = CardElement.Default;
            card.rareness = CardRareness.Default;
            card.card_class = CardClass.Default;
            card.description = "";
            card.quotation = "";
            card.card_release = CardRelease.Default;
            card.cost = -100;
            card.health = -100;
            card.attack = -100;
            card.release_number = 0;
            card.releaseCardsAmount = 0;
            card.image = "";
            
            return card;
        }
    }
}