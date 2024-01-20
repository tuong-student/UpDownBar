using System.Collections.Generic;
using UnityEngine;
using Game.Extension;

public class Table : MonoBehaviour
{
    [SerializeField] private List<Transform> _availableSeats = new List<Transform>();
    private Queue<Transform> _lockSeatList = new Queue<Transform>();
    private Dictionary<Customer, Transform> _unAvailableSeat = new Dictionary<Customer, Transform>();

    #region Unity Functions
    private void Start()
    {
        while(_availableSeats.Count > 1)
        {
            Transform seat = _availableSeats[1];
            _lockSeatList.Enqueue(seat);
            _availableSeats.Remove(seat);
        }
        GameStart();
        TableManager.Instance.OnCustomComplete += OnCustomerCompleteHandler;
        UIManager.Instance.OnStorePhase += OnStorePhaseHandler;
        GameplayManager.Instance.OnNextDay += GameStart;
    }
    private void OnDestroy()
    {
        TableManager.Instance.OnCustomComplete -= OnCustomerCompleteHandler;
        GameplayManager.Instance.OnNextDay -= GameStart;
        UIManager.Instance.OnStorePhase -= OnStorePhaseHandler;
    }
    #endregion

    #region Upgrade Functions    
    public Vector3 GetUpgradePosition()
    {
        return _lockSeatList.ToArray()[0].position.ToVector3XZ();
    }
    #endregion

    #region Event functions
    private void OnStorePhaseHandler()
    {
    }
    #endregion

    #region In game functions
    private void GameStart()
    {
    }
    #endregion

    public void UnlockSeat()
    {
        Transform seat = _lockSeatList.Dequeue();
        _availableSeats.Add(seat);
    }
    public void OnCustomerCompleteHandler(Customer customer)
    {
        if(_unAvailableSeat.ContainsKey(customer))
        {
            _availableSeats.Add(_unAvailableSeat[customer]);
            _unAvailableSeat.Remove(customer);
        }
    }
    public bool IsAvailable()
    {
        return _availableSeats.Count > 0;
    }

    public Transform GetSeatForCustomer(Customer customer)
    {
        if (IsAvailable() == false) return null;
        Transform seat = _availableSeats[Random.Range(0, _availableSeats.Count - 1)];
        _availableSeats.Remove(seat);
        _unAvailableSeat.Add(customer, seat);
        return seat;
    }
}
