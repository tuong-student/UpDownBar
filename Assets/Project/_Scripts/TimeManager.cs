using System;
using NOOD;
using UnityEngine;

public class TimeManager : MonoBehaviorInstance<TimeManager>
{
    public Action OnTimeWarning;
    public Action OnTimeUp;
    [SerializeField] private float _timeMultipler = 1;
    [SerializeField] private float _hourInLevel = 12;
    private float _hour;
    private float _minute;

    #region Unity functions
    void Awake()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        _minute += Time.deltaTime * _timeMultipler;  
        if(_minute >= 60)
        {
            _hour++;
            _minute = 0;
        }
        if(_hour == _hourInLevel)
        {
            // Warning
            OnTimeWarning?.Invoke();
        }
        if(_hour == _hourInLevel + 1 && GameplayManager.Instance.IsEndDay == false)
        {
            // Stop level
            OnTimeUp?.Invoke();
        }
    }
    #endregion

    public float GetHour() => _hour;
    public float GetMinute() => _minute;
}
