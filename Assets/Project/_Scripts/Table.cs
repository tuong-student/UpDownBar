using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private List<Transform> _seatList = new List<Transform>();
    private Dictionary<Customer, Transform> _unAvailableSeat = new Dictionary<Customer, Transform>();
    private Queue<Vector3> _servePositionQueue = new Queue<Vector3>();
    private float _tableHeight = 0.85f;

    private void Start()
    {
        TableManager.Instance.OnCustomComplete += OnCustomerCompleteHandler;
        TableManager.Instance.OnCustomerRequestBeer += OnCustomerRequestBeerHandler;
    }
    private void OnDestroy()
    {
        TableManager.Instance.OnCustomComplete -= OnCustomerCompleteHandler;
        TableManager.Instance.OnCustomerRequestBeer -= OnCustomerRequestBeerHandler;
    }

    public void OnCustomerCompleteHandler(Customer customer)
    {
        if(_unAvailableSeat.ContainsKey(customer))
        {
            _seatList.Add(_unAvailableSeat[customer]);
            _unAvailableSeat.Remove(customer);
        }
    }
    public void OnCustomerRequestBeerHandler(Customer customer)
    {
        if(_unAvailableSeat.ContainsKey(customer))
        {
            Vector3 servePosition = new Vector3(customer.transform.position.x, _tableHeight, this.transform.position.z);
            _servePositionQueue.Enqueue(servePosition);
            Debug.Log("Custom request: " + servePosition);
        }
    }
    public bool GetServePosition(out Vector3 position)
    {
        if(_servePositionQueue.Count > 0)
        {
            position = _servePositionQueue.Dequeue();
            return true;
        }
        else
        {
            position = Vector3.zero;
            return false;
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
