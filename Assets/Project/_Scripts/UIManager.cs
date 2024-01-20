using System;
using NOOD;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Button _shopBtn, _nextDayBtn;
    [SerializeField] private float _timeIncreaseSpeed = 30;
    [Header("Store")]
    [SerializeField] private Button _confirmButton;

    #region Unity functions
    void Awake()
    {
        _endDayPanel.SetActive(false);
        _shopBtn.onClick.AddListener(ActiveStorePhrase);
        _confirmButton.onClick.AddListener(() => 
        {
            OpenEndDayPanel();
            _confirmButton.gameObject.SetActive(false);
        });
        _nextDayBtn.onClick.AddListener(() => OnNextDayPressed?.Invoke());
        _confirmButton.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        GameplayManager.Instance.OnEndDay += OnEndDayHandler;
        OnNextDayPressed += HideEndDayPanel;
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
        GameplayManager.Instance.OnEndDay -= OnEndDayHandler;
        OnNextDayPressed -= HideEndDayPanel;
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
    }
    private void OpenEndDayPanel()
    {
        _endDayPanel.SetActive(true);
    }
    private void HideEndDayPanel()
    {
        _endDayPanel.SetActive(false);
    }
    private void PlayMoneyAnimation(int money)
    {
        float temp = 0;
        NoodyCustomCode.StartUpdater(() =>
        {
            temp += Time.unscaledDeltaTime * _timeIncreaseSpeed;
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
