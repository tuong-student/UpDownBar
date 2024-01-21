using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UpgradeUI : MonoBehaviour
    {
        public Action OnUpgradeButtonClick;

        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private Button _upgradeButton;
        private UpgradeBase _upgradeBase;

        void Awake()
        {
            _upgradeButton = GetComponent<Button>();
            _upgradeButton.onClick.AddListener(() => OnUpgradeButtonClick?.Invoke());
        }
        void OnEnable()
        {
            OnUpgradeButtonClick += UpdateMoneyText;
        }
        void OnDisable()
        {
            OnUpgradeButtonClick -= UpdateMoneyText;
        }

        public void SetParent(UpgradeBase upgradeBase)
        {
            _upgradeBase = upgradeBase;
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void UpdateMoneyText()
        {
            _moneyText.text = _upgradeBase.Price.ToString();
        }
    }

}
