using System;
using System.Collections;
using System.Collections.Generic;
using NOOD;
using UnityEngine;

public class GameplayManager : MonoBehaviorInstance<GameplayManager>
{
    public Action OnEndDay;
    public bool IsEndDay;

    void OnEnable()
    {
        TimeManager.Instance.OnTimeUp += OnTimeUpHandler;
    }
    void OnDisable()
    {
        TimeManager.Instance.OnTimeUp -= OnTimeUpHandler;
    }

    #region Event functions
    private void OnTimeUpHandler()
    {
        IsEndDay = true;
        OnEndDay?.Invoke();
    }
    #endregion
}
