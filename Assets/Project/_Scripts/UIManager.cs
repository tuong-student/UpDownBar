using NOOD;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviorInstance<UIManager>
{
    [Header("In game menu")]
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _timeBG;
    [Header("End day menu")]
    [SerializeField] private GameObject _endDayPanel;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private Button _shopBtn, _nextDayBtn;
    [SerializeField] private float _timeIncreaseSpeed = 5;

    #region Unity functions
    void Awake()
    {
        _endDayPanel.SetActive(false);
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
    }
    private void Update()
    {
        if (GameplayManager.Instance.IsEndDay) return;
        UpdateTime();
    }
    void OnDisable()
    {
        GameplayManager.Instance.OnEndDay -= OnEndDayHandler;
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
    }
    private void OpenEndDayPanel()
    {
        _endDayPanel.SetActive(true);
        PlayMoneyAnimation(MoneyManager.Instance.GetMoney());
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
    #endregion
}
