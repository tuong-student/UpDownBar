using NOOD;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public abstract class UpgradeBase : MonoBehaviour
    {
        public int Price;
        public float PriceMultipler = 100;
    
        [SerializeField] protected UpgradeUI _upgradeUI;
        [SerializeField] protected UpgradeAction _upgradeAction;

        #region Unity functions
        protected void Awake()
        {
            _upgradeUI.SetParent(this);
            Price = (int)PriceMultipler;
            HideUI();
            ChildAwake();
        }
        protected virtual void ChildAwake(){}
        protected void OnEnable()
        {
            _upgradeUI.OnUpgradeButtonClick += OnUpgradeButtonClickHandler;
            if(GameplayManager.Instance)
            {
                GameplayManager.Instance.OnNextDay += HideUI;
            }
            if(UIManager.Instance)
            {
                UIManager.Instance.OnStorePhrase += ShowUI;
                UIManager.Instance.OnStorePhrase += OnStorePhaseHandler;
            }
            ChildOnEnable();
        }
        protected void OnDisable()
        {
            _upgradeUI.OnUpgradeButtonClick -= OnUpgradeButtonClickHandler;
            NoodyCustomCode.UnSubscribeAllEvent<GameplayManager>(this);
            NoodyCustomCode.UnSubscribeAllEvent<UIManager>(this);
            ChildOnDisable();
        }
        protected virtual void ChildOnDisable(){}
        protected virtual void ChildOnEnable(){}
        #endregion

        private void OnUpgradeButtonClickHandler()
        {
            UpgradeManager.Instance.Upgrade(this);
        }

        protected virtual void OnStorePhaseHandler(){}
        public void Upgrade()
        {
            _upgradeAction.Invoke();
        }

        public virtual void AutoSetNewPrice(int upgradeTime)
        {
            Price += (int) (upgradeTime * PriceMultipler);
        }

        public void ShowUI()
        {
            _upgradeUI.gameObject.SetActive(true);
            _upgradeUI.UpdateMoneyText();
        }
        public void HideUI()
        {
            _upgradeUI.gameObject.SetActive(false);
        }
    }
}
