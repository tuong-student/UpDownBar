using System;
using System.Collections;
using System.Collections.Generic;
using NOOD;
using UnityEngine;

namespace Game
{
    public class TableManager : MonoBehaviorInstance<TableManager>
    {
        public Action<Customer> OnCustomComplete;
        public Action<Customer> OnCustomerRequestBeer;
        [SerializeField] private List<Table> _tableList = new List<Table>();
        [SerializeField] private bool _unlockAllSeats;
        private int _currentTableIndex = 0;

        void OnEnable()
        {
            Player.OnPlayerChangePosition += OnPlayerChangePositionHandler;
            foreach(Table table in _tableList)
            {
                table.SetIsUnlockAllSeats(_unlockAllSeats);
            }
        }
        void OnDisable()
        {
            Player.OnPlayerChangePosition -= OnPlayerChangePositionHandler;
        }

        private void OnPlayerChangePositionHandler(int index)
        {
            _currentTableIndex = index;
        }

        public List<Table> GetTableList()
        {
            return _tableList;
        }
        public Vector3 GetPlayerTablePosition()
        {
            return _tableList[_currentTableIndex].transform.position;
        }
        public Table GetTable(int index)
        {
            try
            {
                return _tableList[index];
            }catch
            {
                Debug.LogError("table index out of range");
                return null;
            }
        }
        public Table GetPlayerTable()
        {
            return _tableList[_currentTableIndex];
        }
        public void CustomerComplete(Customer customer)
        {
            OnCustomComplete?.Invoke(customer);
        }
        public void RequestBeer(Customer customer)
        {
            OnCustomerRequestBeer?.Invoke(customer);        
        }
    }

}
