using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NOOD;
using UnityEngine;

public class BeerServeManager : MonoBehaviorInstance<BeerServeManager>
{
    [SerializeField] private Transform _beerCupPref;
    private Player _player;

    void Start()
    {
        _player = Player.Instance;        
    }

    public void ServeBeer()
    {
        Table playerTable = TableManager.Instance.GetPlayerTable();
        if(_player == null)
        {
            _player = Player.Instance;
        }
        Vector3 startPosition = _player.transform.position;
        if (!playerTable.GetServePosition(out Vector3 servePosition)) return;

        startPosition.y = servePosition.y;
        Transform beerCup = Instantiate(_beerCupPref, startPosition, Quaternion.identity);
        beerCup.DOMove(servePosition, 1.5f);
    }
}
