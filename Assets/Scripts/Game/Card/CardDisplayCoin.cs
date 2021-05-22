using System.Globalization;
using TMPro;
using UnityEngine;

namespace Game.Card
{
    public class CardDisplayCoin : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinText;
        private int _cost;
        private int _initValue;

        public int CostValue
        {
            get => _cost;
            set
            {
                _cost = value;
                if (value < _initValue)
                {
                    if (value < 0) _cost = 0;
                    coinText.color = Color.green;
                }
                if (value > _initValue)
                {
                    coinText.color = Color.red;
                }
                if (value == _initValue)
                {
                    coinText.color = Color.white;
                }
                if (_initValue == -1)//Если -1 то это Х (таков костысль) (см карты игры)
                {
                    coinText.color = Color.white;
                    coinText.text = "X";
                    return;
                }
                coinText.text = $"{value}";
            }
        }

        public void InitCostValue(int value)
        {
            _initValue = value;
            CostValue = value;
        }
    }
}