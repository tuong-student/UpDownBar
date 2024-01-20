using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private List<Transform> _seatList = new List<Transform>();
    private Queue<Transform> _lockSeatList = new Queue<Transform>();
    private Dictionary<Customer, Transform> _unAvailableSeat = new Dictionary<Customer, Transform>();

    private void Start()
    {
        TableManager.Instance.OnCustomComplete += OnCustomerCompleteHandler;
        while(_seatList.Count > 1)
        {
            Transform seat = _seatList[1];
            _lockSeatList.Enqueue(seat);
            _seatList.Remove(seat);
        }
    }
    private void OnDestroy()
    {
        TableManager.Instance.OnCustomComplete -= OnCustomerCompleteHandler;
    }
    
    public void UnlockSeat()
    {
        Transform seat = _lockSeatList.Dequeue();
        _seatList.Add(seat);
    }
    public void OnCustomerCompleteHandler(Customer customer)
    {
        if(_unAvailableSeat.ContainsKey(customer))
        {
            _seatList.Add(_unAvailableSeat[customer]);
            _unAvailableSeat.Remove(customer);
        }
    }
    public bool IsAvailable()
    {
        return _seatList.Count > 0;
    }

    public Transform GetSeatForCustomer(Customer customer)
    {
        if (IsAvailable() == false) return null;
        Transform seat = _seatList[Random.Range(0, _seatList.Count - 1)];
        _seatList.Remove(seat);
        _unAvailableSeat.Add(customer, seat);
        return seat;
    }
}
