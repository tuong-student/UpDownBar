using System;
using NOOD;
using UnityEngine;

public class GameplayManager : MonoBehaviorInstance<GameplayManager>
{
    public Action OnEndDay;
    public Action OnNextDay;
    public bool IsEndDay;

    void OnEnable()
    {
        TimeManager.Instance.OnTimeUp += OnTimeUpHandler;
        UIManager.Instance.OnNextDayPressed += OnNextDayPressHandler;
    }
    void OnDisable()
    {
        TimeManager.Instance.OnTimeUp -= OnTimeUpHandler;
        UIManager.Instance.OnNextDayPressed -= OnNextDayPressHandler;
    }

    #region Event functions
    private void OnTimeUpHandler()
    {
        IsEndDay = true;
        OnEndDay?.Invoke();
    }
    private void OnNextDayPressHandler()
    {
        IsEndDay = false;
        OnNextDay?.Invoke();
    }
    #endregion
}
