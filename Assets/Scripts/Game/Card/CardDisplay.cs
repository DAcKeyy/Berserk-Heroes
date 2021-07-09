using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Card
{
    public class CardDisplay : MonoBehaviour
    {
        #region Привязки элементов объекта
        
        [Header("Привязки элементов объекта")]
        [SerializeField]private TMP_Text nameText;
        [SerializeField]private TMP_Text descriptionText;
        [SerializeField]private TMP_Text classText;
        [SerializeField]private TMP_Text releaseNumberText;
        [SerializeField]private TMP_Text maxReleaseAmountText;
        [SerializeField]private CardDisplayCoin coinScript;
        [SerializeField]private CardDisplayHealth healthScript;
        [SerializeField]private CardDisplayAttack attackScript;
        [SerializeField]private CardDisplayType typeScript;
        [SerializeField]private CardDisplayRareness rarenessScript;
        [SerializeField]private CardDisplayWrapper wrapperScript;
        [SerializeField]private CardDisplayURLimage imageScript;
        [SerializeField] private CardSelected CardSelectedScript;

        #endregion
        [Header("Данные карты")]
        [SerializeField]private Card card;

        public Action OnCardPressed = delegate {  };
        
        //аксессор данных о карте 
        public Card CardData
        {
            get => card;
            private set
            {
                card = value;
                
                nameText.text = value.name;
                descriptionText.text = value.description;
                classText.text = value.card_class.ToString();
                
                coinScript.CostValue = value.cost;
                healthScript.HealthValue = value.health;
                attackScript.AttackValue = value.attack;
                
                typeScript.SetTypeBackground(card.element);
            }
        }

        private void OnEnable()
        {
            CardSelectedScript.CardMouseUp += OnCardPressed;
        }

        public void InitCard(Card value)
        {
            ////На это не смотрите, это для теста/////
            coinScript.gameObject.SetActive(true);
            healthScript.gameObject.SetActive(true);
            attackScript.gameObject.SetActive(true);
            ////////////////////////////////////////////
                
            card = value;
                
            //Пропихиваем в тексты карты данные о карте
            nameText.text = card.name;
            descriptionText.text = card.description;
            classText.text = card.card_class.ToString();
            releaseNumberText.text = $"{card.release_number}";
            maxReleaseAmountText.text = $"{card.releaseCardsAmount}";
                
            //В скрипты визуализации карты пропихиваем данные, они должны висеть на карте (см CardDisplayCoin, CardDisplayHealth, CardDisplayAttack, CardDisplayType, CardDisplayRareness, CardDisplayWrapper)
                
            if(card.cost >= -1) coinScript.InitCostValue(card.cost);//Если -1 то это Х (таков костысль) (см карты игры)
            else coinScript.gameObject.SetActive(false);
                
            if(card.health >= -1)healthScript.InitCostValue(card.health, card.type);//Если -1 то это Х (таков костысль) (см карты игры)
            else healthScript.gameObject.SetActive(false);
                
            if(card.attack >= -1)attackScript.InitAttackValue(card.attack);//Если -1 то это Х (таков костысль) (см карты игры)
            else attackScript.gameObject.SetActive(false);
                
            typeScript.SetType(card.type);
            typeScript.SetTypeBackground(card.element);
                
            rarenessScript.SetRareness(card.rareness);
            wrapperScript.SetWrapper(card.element);
            
            imageScript.SetURL_Image(value.image);
        }
    }

    [CustomEditor(typeof(CardDisplay))]
    public class CardDisplayBuilderEditor : Editor
    {
        //скрипт для отображении кнопки "Инициализировать данные" для провекрки работы карты в редакторе
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CardDisplay myScript = (CardDisplay)target;
            if(GUILayout.Button("Инициализировать данные"))
            {
                myScript.InitCard(myScript.CardData);
            }
        }
    }
}