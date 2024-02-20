using System;
using NOOD;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using NOOD.Sound;

namespace Game
{
    public class UIManager : MonoBehaviorInstance<UIManager>
    {
        #region Events
        public Action OnStorePhase;
        public Action OnNextDayPressed;
        #endregion

        [Header("In game menu")]
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private Image _timeBG;
        [Header("End day menu")]
        [SerializeField] private GameObject _endDayPanel;
        [SerializeField] private TextMeshProUGUI _money;
        [SerializeField] private CustomButton _shopBtn, _nextDayBtn;
        [SerializeField] private float _timeIncreaseSpeed = 30;
        [Header("Store")]
        [SerializeField] private CustomButton _confirmButton;

        #region Unity functions
        void Awake()
        {
            _endDayPanel.SetActive(false);
            _shopBtn.OnClick += ActiveStorePhrase;
            _nextDayBtn.OnClick += () => OnNextDayPressed?.Invoke();
            _confirmButton.OnClick += () => 
            {
                OpenEndDayPanel();
                _confirmButton.gameObject.SetActive(false);
            };
            _confirmButton.gameObject.SetActive(false);
            OnNextDayPressed += HideEndDayPanel;
        }
        void OnEnable()
        {
            GameplayManager.Instance.OnEndDay += OnEndDayHandler;
        }
        private void Start()
        {
            UpdateMoney();
            _timeBG.color = Color.white;
            TimeManager.Instance.OnTimeWarning += () => 
            { 
                _timeBG.color = Color.red; 
            };
            GameplayManager.Instance.OnNextDay += () =>
            {
                _timeBG.color = Color.white;
            };
        }
        private void Update()
        {
            if (GameplayManager.Instance.IsEndDay) return;
            UpdateTime();
        }
        void OnDisable()
        {
            NoodyCustomCode.UnSubscribeAllEvent<GameplayManager>(this);
        }
        private void OnDestroy()
        {
            OnNextDayPressed -= HideEndDayPanel;
            _shopBtn.OnClick -= ActiveStorePhrase;
            _endDayPanel.transform.DOKill();
        }
        #endregion

        #region In game
        public void UpdateMoney()
        {
            _moneyText.text = MoneyManager.Instance.GetMoney().ToString();
        }
        public void UpdateTime()
        {
            float hour = TimeManager.Instance.GetHour();
            float minute = TimeManager.Instance.GetMinute();

            _timeText.text = $"{hour.ToString("00")}:{minute.ToString("00")}";
        }
        #endregion

        #region End game
        private void OnEndDayHandler()
        {
            OpenEndDayPanel();
            PlayMoneyAnimation(MoneyManager.Instance.GetMoney());
            SoundManager.PlaySound(SoundEnum.MoneySound);
        }
        private void OpenEndDayPanel()
        {
            _endDayPanel.SetActive(true);
            _endDayPanel.transform.DOScale(Vector3.one, 0.7f);
            EventSystem.current.SetSelectedGameObject(null);
        }
        private void HideEndDayPanel()
        {
            _endDayPanel.transform.DOScale(Vector3.zero, 0.7f).OnComplete(() => _endDayPanel.SetActive(false));
        }
        private void PlayMoneyAnimation(int money)
        {
            float time = 0;
            float temp = 0;
            NoodyCustomCode.StartUpdater(this, () =>
            {
                time += Time.unscaledDeltaTime;
                temp = Mathf.Lerp(0, money, time/SoundManager.GetSoundLength(SoundEnum.MoneySound));
                _money.text = temp.ToString("0");
                return temp > money;
            });
        }
        private void ActiveStorePhrase()
        {
            HideEndDayPanel();
            _confirmButton.gameObject.SetActive(true);
            OnStorePhase?.Invoke();
        }
        #endregion
    }

}
