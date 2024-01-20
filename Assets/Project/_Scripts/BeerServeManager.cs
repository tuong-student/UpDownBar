using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NOOD;
using UnityEngine;

public class BeerServeManager : MonoBehaviorInstance<BeerServeManager>
{
    public Action OnServerFail;

    [SerializeField] private int _moneyLostOnFail = 20;
    [SerializeField] private Transform _beerCupPref;
    [SerializeField] private float _tableHeight = 0.8f;
    private Player _player;

    #region Unity functions
    void OnEnable()
    {
        OnServerFail += OnServerFailHandler;
    }
    void Start()
    {
        _player = Player.Instance;
    }
    void OnDisable()
    {
        OnServerFail += OnServerFailHandler;
    }
    #endregion


    private void OnServerFailHandler()
    {
        MoneyManager.Instance.RemoveMoney(_moneyLostOnFail);
    }

    public void ServeBeer()
    {
        if (GameplayManager.Instance.IsEndDay == true) return;
        
        if(_player == null)
        {
            _player = Player.Instance;
        }
        Vector3 startPosition = _player.transform.position;
        startPosition.y = _tableHeight;

        BeerCup beerCup = Instantiate(_beerCupPref, startPosition, Quaternion.identity).GetComponent<BeerCup>();
        float moveSpeed = 5;
        NoodyCustomCode.StartUpdater(() =>
        {
            if (beerCup == null) return true;

            beerCup.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            return false;
        });
    }
}
