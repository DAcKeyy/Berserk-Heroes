using System.Globalization;
using TMPro;
using UnityEngine;

namespace Game.Card
{
    public class CardDisplayHealth : MonoBehaviour
    {
        [SerializeField]private TMP_Text healthText;
        private int _health;
        private int _initValue;

        public int HealthValue
        {
            get => _health;
            set
            {
                _health = value;
                if (value < _initValue)
                {
                    healthText.color = Color.red;
                }
                if (value > _initValue)
                {
                    healthText.color = Color.green;
                }
                if (value == _initValue)
                {
                    healthText.color = Color.white;
                }
                if (_initValue == -1)//Если -1 то это Х (таков костысль) (см карты игры)
                {
                    healthText.color = Color.white;
                    healthText.text = "X";
                    return;
                }
                healthText.text = $"{value}";
            }
        }

        public void InitCostValue(int value, CardType cardType)
        {
            if (cardType == CardType.Hero) this.transform.localScale = new Vector3(1, 1, 1);
            else this.transform.localScale = new Vector3(0.865f, 0.865f, 0.865f);
            _initValue = value;
            HealthValue = value;
        }
    }
}
