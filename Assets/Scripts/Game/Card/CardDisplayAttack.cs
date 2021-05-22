using System.Globalization;
using TMPro;
using UnityEngine;

namespace Game.Card
{
    public class CardDisplayAttack : MonoBehaviour
    {
        [SerializeField]private TMP_Text attackText;
        private int _attack;
        private int _initValue;

        public int AttackValue
        {
            get => _attack;
            set
            {
                _attack = value;
                if (value < _initValue)
                {
                    if (value < 0) _attack = 0;
                    attackText.color = Color.red;
                }
                if (value > _initValue)
                {
                    attackText.color = Color.green;
                }
                if (value == _initValue)
                {
                    attackText.color = Color.white;
                }
                if (_initValue == -1)//Если -1 то это Х (таков костысль) (см карты игры)
                {
                    attackText.color = Color.white;
                    attackText.text = "X";
                    return;
                }
                attackText.text = $"{value}";
            }
        }

        public void InitAttackValue(int value)
        {
            _initValue = value;
            AttackValue = value;
        }
    }
}
