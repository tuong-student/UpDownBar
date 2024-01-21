using System.Collections;
using System.Collections.Generic;
using NOOD;
using UnityEngine;

namespace Game
{
    public class MoneyManager : MonoBehaviorInstance<MoneyManager>
    {
        private int _money;

        public int GetMoney() => _money;
        public void AddMoney(int amount)
        {
            _money += amount;
            UIManager.Instance.UpdateMoney();
        }
        public bool PayMoney(int amount)
        {
            if (_money >= amount)
            {
                _money -= amount;
                UIManager.Instance.UpdateMoney();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// If buying stuff, use PayMoney(int) instead
        /// </summary>
        /// <param name="amount"></param>
        public void RemoveMoney(int amount)
        {
            _money -= amount;
            UIManager.Instance.UpdateMoney();
        }
    }

}
