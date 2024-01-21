using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NOOD;
using UnityEngine;

namespace Game
{
    public class CustomerSpawner : MonoBehaviorInstance<CustomerSpawner>
    {
        [SerializeField] private Transform _customerPref;
        [SerializeField] private float _spawnTime;

        private void Start()
        {
            StartCoroutine(SpawnerCustomer());
        }

        IEnumerator SpawnerCustomer()
        {
            while(true)
            {
                yield return new WaitForSeconds(_spawnTime);

                List<Table> availableTable = TableManager.Instance.GetTableList().Where(table => table.IsAvailable() == true).ToList();
                if (availableTable != null && availableTable.Count > 0 && GameplayManager.Instance.IsEndDay == false)
                {
                    Customer customer = Instantiate(_customerPref, this.transform.position, Quaternion.identity).GetComponent<Customer>();

                    // Get random seat
                    int r = Random.Range(0, availableTable.Count - 1);
                    Transform seat = availableTable[r].GetSeatForCustomer(customer);
                    // Check if seat valid
                    customer.SetTargetPosition(seat.position);
                }
            }
        }
    }

}
