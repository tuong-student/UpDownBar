using System.Collections.Generic;
using UnityEngine;
using Game.Extension;
using NOOD;

namespace Game
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private List<Transform> _availableSeats = new List<Transform>();
        private Queue<Transform> _lockSeatList = new Queue<Transform>();
        private Dictionary<Customer, Transform> _unAvailableSeat = new Dictionary<Customer, Transform>();
        private bool _unlockAllSeats;

        #region Unity Functions
        private void Start()
        {
            while(_availableSeats.Count > 1 && _unlockAllSeats == false)
            {
                // Deactivate all seats but 1
                Transform seat = _availableSeats[1];
                seat.gameObject.SetActive(false);
                _lockSeatList.Enqueue(seat);
                _availableSeats.Remove(seat);
            }
            TableManager.Instance.OnCustomComplete += OnCustomerCompleteHandler;
        }
        private void OnDestroy()
        {
            NoodyCustomCode.UnSubscribeAllEvent<TableManager>(this);
            NoodyCustomCode.UnSubscribeAllEvent<GameplayManager>(this);
            NoodyCustomCode.UnSubscribeAllEvent<UIManager>(this);
        }
        #endregion

        #region Upgrade Functions    
        public Vector3 GetUpgradePosition()
        {
            return _lockSeatList.ToArray()[0].position.ToVector3XZ();
        }
        #endregion

        #region Event functions
        #endregion

        #region In game functions
        public void SetIsUnlockAllSeats(bool value)
        {
            _unlockAllSeats = value;
        }
        #endregion

        public void UnlockSeat()
        {
            Transform seat = _lockSeatList.Dequeue();
            seat.gameObject.SetActive(true);
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

}
